using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Application.Interfaces.Repositories;
public interface IAccessRecordsRepository
{
    Task AddAsync(AccessRecord record, CancellationToken ct = default);
    Task<(IReadOnlyList<AccessRecord> Items, int Total)> SearchAsync(Guid? employeeId,
        Guid? accessPointId, AccessDecision? decision, DateTime? fromUtc, DateTime? toUtc,
        int pageNumber, int pageSize, CancellationToken ct = default);
}
