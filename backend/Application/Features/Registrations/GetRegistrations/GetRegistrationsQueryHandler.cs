namespace Application.Features.Registrations.GetRegistrations;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationsQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>>
{
    public async Task<Result<GetRegistrationsResponse>> HandleAsync(
        GetRegistrationsQuery query, CancellationToken cancellationToken = default)
    {
        var q = dbContext.Registrations.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.ParticipantName))
            q = q.Where(r => EF.Functions.Like(r.ParticipantName, $"%{query.ParticipantName.Trim()}%")); // case-insensitive in SQLite
        if (query.Type is { } type) q = q.Where(r => r.RegistrationType == type);
        if (query.Status is { } status) q = q.Where(r => r.Status == status);

        var total = await q.CountAsync(cancellationToken);

        var rows = await q
            .OrderByDescending(r => r.RegistrationDate)
            .ThenByDescending(r => r.RegistrationNumber)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var pages = (int)Math.Ceiling(total / (double)query.PageSize);

        return Result<GetRegistrationsResponse>.Success(new GetRegistrationsResponse(
            rows.Select(RegistrationResponse.From).ToList(), query.Page, query.PageSize, total, pages));
    }
}