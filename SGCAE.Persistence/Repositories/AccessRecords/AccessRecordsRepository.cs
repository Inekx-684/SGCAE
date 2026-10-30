using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.AccessRecords;
using SGCAE.Persistence.Context;
namespace SGCAE.Persistence.Repositories.AccessRecords;
public sealed class AccessRecordsRepository(DataContext context) : IAccessRecordsRepository
{
    public async Task AddAsync(AccessRecord record, CancellationToken ct = default) =>
        await context.Set<AccessRecord>().AddAsync(record, ct);
    public async Task<(IReadOnlyList<AccessRecord> Items, int Total)> SearchAsync(
        Guid? employeeId, Guid? accessPointId, AccessDecision? decision, DateTime? fromUtc,
        DateTime? toUtc, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        IQueryable<AccessRecord> query = context.Set<AccessRecord>().AsNoTracking();
        if (employeeId.HasValue) query = query.Where(x => x.EmployeeId == employeeId);
        if (accessPointId.HasValue) query = query.Where(x => x.AccessPointId == accessPointId);
        if (decision.HasValue) query = query.Where(x => x.Decision == decision);
        if (fromUtc.HasValue) query = query.Where(x => x.OccurredAt >= fromUtc);
        if (toUtc.HasValue) query = query.Where(x => x.OccurredAt <= toUtc);
        var total = await query.CountAsync(ct);
        var items = await query.OrderByDescending(x => x.OccurredAt).ThenByDescending(x => x.Id)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }
}
