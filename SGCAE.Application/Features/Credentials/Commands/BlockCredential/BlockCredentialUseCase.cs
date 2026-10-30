using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.Credentials.Commands.BlockCredential;
public sealed class BlockCredentialUseCase(ICredentialsRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var credential = await repository.GetByIdAsync(id, ct);
        if (credential is null) return false;
        credential.Block();
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}
