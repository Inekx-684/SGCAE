using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.AccessPoints;
using SGCAE.Persistence.Context;
namespace SGCAE.Persistence.Repositories.AccessPoints;
public sealed class AccessPointsRepository(DataContext context) : IAccessPointsRepository
{
    public async Task AddAsync(AccessPoint point, CancellationToken ct = default) => await context.Set<AccessPoint>().AddAsync(point, ct);
    public Task<AccessPoint?> GetByIdAsync(Guid id, CancellationToken ct = default) => context.Set<AccessPoint>().FirstOrDefaultAsync(x => x.Id == id, ct);
    public Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default) => context.Set<AccessPoint>().AnyAsync(x => x.Name == name, ct);
    public async Task<IReadOnlyList<AccessPoint>> ListAsync(CancellationToken ct = default) => await context.Set<AccessPoint>().AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);
}
