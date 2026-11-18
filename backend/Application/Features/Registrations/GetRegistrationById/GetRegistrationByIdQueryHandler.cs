namespace Application.Features.Registrations.GetRegistrationById;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationByIdQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationByIdQuery, Result<RegistrationResponse>>
{
    public async Task<Result<RegistrationResponse>> HandleAsync(
        GetRegistrationByIdQuery query, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Registrations.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

        return entity is null
            ? Result<RegistrationResponse>.Failure(RegistrationErrors.NotFound(query.Id))
            : Result<RegistrationResponse>.Success(RegistrationResponse.From(entity));
    }
}