namespace SGCAE.Application.Features.Employees.Dtos;
public sealed record PagedEmployeesDto(
    IReadOnlyList<EmployeeDto> Items, int Page, int PageSize, int Total);
