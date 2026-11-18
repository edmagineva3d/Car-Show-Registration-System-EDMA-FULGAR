namespace Application.Features.Registrations.CreateRegistration;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class CreateRegistrationCommandHandler(IAppDbContext dbContext)
    : ICommandHandler<CreateRegistrationCommand, Result<RegistrationResponse>>
{
    public async Task<Result<RegistrationResponse>> HandleAsync(
        CreateRegistrationCommand command, CancellationToken cancellationToken = default)
    {
        var plate = command.PlateNumber.Trim();

        if (await dbContext.Registrations.AnyAsync(r => r.PlateNumber == plate, cancellationToken))
            return Result<RegistrationResponse>.Failure(RegistrationErrors.DuplicatePlate(plate));

        // Sequential number CS27-0001, CS27-0002, ...
        var last = await dbContext.Registrations
            .OrderByDescending(r => r.RegistrationNumber)
            .Select(r => r.RegistrationNumber)
            .FirstOrDefaultAsync(cancellationToken);
        var next = last is null ? 1 : int.Parse(last["CS27-".Length..]) + 1;

        var entity = new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = $"CS27-{next:D4}",
            RegistrationDate = DateTime.UtcNow,                         // server decides
            ParticipantName = command.ParticipantName.Trim(),
            ContactNumber = command.ContactNumber.Trim(),
            Email = command.Email.Trim(),
            Address = command.Address.Trim(),
            CarMakeModel = command.CarMakeModel.Trim(),
            PlateNumber = plate,
            RegistrationType = command.RegistrationType,
            Status = RegistrationStatus.Pending,                        // server decides
            RegistrationFee = RegistrationFees.For(command.RegistrationType), // server decides
            AmountPaid = command.AmountPaid
        };

        dbContext.Registrations.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<RegistrationResponse>.Success(RegistrationResponse.From(entity));
    }
}