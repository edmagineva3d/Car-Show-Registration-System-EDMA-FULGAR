namespace Application.Features.Registrations.GetRegistrationSummary;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class GetRegistrationSummaryQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>>
{
    public async Task<Result<GetRegistrationSummaryResponse>> HandleAsync(
        GetRegistrationSummaryQuery query, CancellationToken cancellationToken = default)
    {
        var counts = await dbContext.Registrations.AsNoTracking()
            .GroupBy(r => r.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);
        int C(RegistrationStatus s) => counts.FirstOrDefault(x => x.Status == s)?.Count ?? 0;

        // SQLite can't SUM decimals in SQL, so sum in memory. Cancelled are excluded from money.
        var money = await dbContext.Registrations.AsNoTracking()
            .Where(r => r.Status != RegistrationStatus.Cancelled)
            .Select(r => new { r.RegistrationFee, r.AmountPaid })
            .ToListAsync(cancellationToken);

        var fees = money.Sum(m => m.RegistrationFee);
        var paid = money.Sum(m => m.AmountPaid);

        return Result<GetRegistrationSummaryResponse>.Success(new GetRegistrationSummaryResponse(
            counts.Sum(x => x.Count),
            C(RegistrationStatus.Confirmed), C(RegistrationStatus.CheckedIn),
            C(RegistrationStatus.Pending), C(RegistrationStatus.Cancelled),
            fees, paid, fees - paid));
    }
}