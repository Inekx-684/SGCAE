using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Application.Features.AccessPoints.Dtos;
namespace SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointsList;
public sealed class GetAccessPointsListUseCase(IAccessPointsRepository repository)
{
    public async Task<IReadOnlyList<AccessPointDto>> ExecuteAsync(CancellationToken ct = default)
    {
        var points = await repository.ListAsync(ct);
        return points.Select(AccessPointDto.From).ToArray();
    }
}
