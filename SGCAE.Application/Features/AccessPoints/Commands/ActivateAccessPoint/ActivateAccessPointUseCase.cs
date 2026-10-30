using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Application.Interfaces.Persistence;
namespace SGCAE.Application.Features.AccessPoints.Commands.ActivateAccessPoint;
public sealed class ActivateAccessPointUseCase(IAccessPointsRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var point = await repository.GetByIdAsync(id, ct);
        if (point is null) return false;
        point.Activate();
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}
