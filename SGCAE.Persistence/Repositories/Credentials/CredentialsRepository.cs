using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Credentials;
using SGCAE.Persistence.Context;
namespace SGCAE.Persistence.Repositories.Credentials;
public sealed class CredentialsRepository(DataContext context) : ICredentialsRepository
{
    public async Task AddAsync(Credential credential, CancellationToken ct = default) => await context.Set<Credential>().AddAsync(credential, ct);
    public Task<Credential?> GetByIdAsync(Guid id, CancellationToken ct = default) => context.Set<Credential>().FirstOrDefaultAsync(x => x.Id == id, ct);
    public Task<Credential?> GetByCodeAsync(string code, CancellationToken ct = default) => context.Set<Credential>().AsNoTracking().FirstOrDefaultAsync(x => x.Code == code, ct);
    public Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default) => context.Set<Credential>().AnyAsync(x => x.Code == code, ct);
    public Task<bool> HasActiveCredentialAsync(Guid employeeId, CancellationToken ct = default) => context.Set<Credential>().AnyAsync(x => x.EmployeeId == employeeId && x.Status == CredentialStatus.Active, ct);
}
