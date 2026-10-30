using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Application.Features.AccessPoints.Dtos;
namespace SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointById;
public sealed class GetAccessPointByIdUseCase(IAccessPointsRepository repository)
{
    public async Task<AccessPointDto?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var point = await repository.GetByIdAsync(id, ct);
        return point is null ? null : AccessPointDto.From(point);
    }
}
