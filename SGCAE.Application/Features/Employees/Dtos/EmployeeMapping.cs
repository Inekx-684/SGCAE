using SGCAE.Domain.Entities.Employees;
namespace SGCAE.Application.Features.Employees.Dtos;
public static class EmployeeMapping
{
    public static EmployeeDto ToDto(Employee x) => new(
        x.Id, x.Identification, x.FirstName, x.LastName,
        x.Email, x.Position, x.Department, x.IsActive, x.CreatedAt);
}
