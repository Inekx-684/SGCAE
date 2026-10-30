using SGCAE.Application.Features.Credentials.Dtos;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.Credentials.Queries.GetCredentialById;
public sealed class GetCredentialByIdUseCase(ICredentialsRepository repository)
{
    public async Task<CredentialDto?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var credential = await repository.GetByIdAsync(id, ct);
        return credential is null ? null : CredentialDto.From(credential);
    }
}
