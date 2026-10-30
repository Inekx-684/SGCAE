using SGCAE.Domain.Entities.Credentials;
namespace SGCAE.Application.Interfaces.Repositories;
public interface ICredentialsRepository
{
    Task AddAsync(Credential credential, CancellationToken ct = default);
    Task<Credential?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Credential?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default);
    Task<bool> HasActiveCredentialAsync(Guid employeeId, CancellationToken ct = default);
}
