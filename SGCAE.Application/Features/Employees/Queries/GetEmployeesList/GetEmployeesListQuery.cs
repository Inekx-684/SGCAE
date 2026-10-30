namespace SGCAE.Application.Features.Employees.Queries.GetEmployeesList;
public sealed record GetEmployeesListQuery(int Page = 1, int PageSize = 10, string? Search = null, bool? IsActive = null);
