using System;
using System.Collections.Generic;
using System.Text;
using SGCAE.Application.Features.Employees.Dtos;
using SGCAE.Application.Interfaces.Repositories;

namespace SGCAE.Application.Features.Employees.Queries.GetEmployeeById;

public sealed class GetEmployeeByIdUseCase
{
    private readonly IEmployeesRepository _employeesRepository;

    public GetEmployeeByIdUseCase(
        IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task<EmployeeDto?> ExecuteAsync(
        GetEmployeeByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var employee = await _employeesRepository.GetByIdAsync(
            query.Id,
            cancellationToken);

        if (employee is null)
        {
            return null;
        }

        return new EmployeeDto(
            employee.Id,
            employee.Identification,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.Position,
            employee.Department,
            employee.IsActive,
            employee.CreatedAt
        );
    }
}
