using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.Credentials.Commands.RevokeCredential;
public sealed class RevokeCredentialUseCase(ICredentialsRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var credential = await repository.GetByIdAsync(id, ct);
        if (credential is null) return false;
        credential.Revoke();
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}
