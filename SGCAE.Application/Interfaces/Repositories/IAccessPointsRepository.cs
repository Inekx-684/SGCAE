using SGCAE.Domain.Entities.AccessPoints;
namespace SGCAE.Application.Interfaces.Repositories;
public interface IAccessPointsRepository
{
    Task AddAsync(AccessPoint point, CancellationToken ct = default);
    Task<AccessPoint?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    Task<IReadOnlyList<AccessPoint>> ListAsync(CancellationToken ct = default);
}
