using SGCAE.Application.Features.Employees.Dtos;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.Employees.Queries.GetEmployeesList;
public sealed class GetEmployeesListUseCase(IEmployeesRepository repository)
{
    public async Task<PagedEmployeesDto> ExecuteAsync(GetEmployeesListQuery query, CancellationToken ct = default)
    {
        if (query.Page < 1 || query.PageSize < 1 || query.PageSize > 100)
            throw new ArgumentOutOfRangeException(nameof(query), "Page debe ser >= 1 y PageSize debe estar entre 1 y 100.");
        var (items, total) = await repository.GetPagedAsync(query.Page, query.PageSize, query.Search, query.IsActive, ct);
        return new PagedEmployeesDto(items.Select(EmployeeMapping.ToDto).ToList(), query.Page, query.PageSize, total);
    }
}
