
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Features.Employees.Commands.CreateEmployee;
using SGCAE.Application.Features.Employees.Queries.GetEmployeeById;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetEmployeeByIdUseCase _getEmployeeByIdUseCase;

    public EmployeesController(
        CreateEmployeeUseCase createEmployeeUseCase,
        GetEmployeeByIdUseCase getEmployeeByIdUseCase)
    {
        _createEmployeeUseCase = createEmployeeUseCase;
        _getEmployeeByIdUseCase = getEmployeeByIdUseCase;
    }

    // POST: api/employees
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            Guid employeeId = await _createEmployeeUseCase
                .ExecuteAsync(command, cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                new { Id = employeeId });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new
            {
                Message = "Los datos del empleado no son válidos.",
                Errors = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
        catch (BussinesRuleException ex)
        {
            return Conflict(new
            {
                Message = ex.Message
            });
        }
    }

    // GET: api/employees/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeeByIdQuery(id);

        var employee = await _getEmployeeByIdUseCase
            .ExecuteAsync(query, cancellationToken);

        if (employee is null)
        {
            return NotFound(new
            {
                Message = "El empleado no fue encontrado."
            });
        }

        return Ok(employee);
    }
}
