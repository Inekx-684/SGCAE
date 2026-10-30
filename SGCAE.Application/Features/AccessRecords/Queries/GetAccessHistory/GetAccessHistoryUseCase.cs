using SGCAE.Application.Features.AccessRecords.Dtos;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.AccessRecords.Queries.GetAccessHistory;
public sealed class GetAccessHistoryUseCase(IAccessRecordsRepository repository)
{
    public async Task<PagedAccessRecordsDto> ExecuteAsync(GetAccessHistoryQuery query, CancellationToken ct = default)
    {
        if (query.PageNumber < 1 || query.PageSize is < 1 or > 100)
            throw new ArgumentException("PageNumber debe ser mayor que cero y PageSize debe estar entre 1 y 100.");
        if (query.FromUtc.HasValue && query.ToUtc.HasValue && query.FromUtc > query.ToUtc)
            throw new ArgumentException("La fecha inicial no puede ser posterior a la final.");
        var (items, total) = await repository.SearchAsync(query.EmployeeId, query.AccessPointId,
            query.Decision, query.FromUtc, query.ToUtc, query.PageNumber, query.PageSize, ct);
        return new PagedAccessRecordsDto(items.Select(AccessRecordDto.FromEntity).ToList(),
            total, query.PageNumber, query.PageSize);
    }
}
