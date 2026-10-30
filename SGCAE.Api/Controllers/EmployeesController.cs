using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Features.Employees.Commands.CreateEmployee;
using SGCAE.Application.Features.Employees.Commands.UpdateEmployee;
using SGCAE.Application.Features.Employees.Commands.DeactivateEmployee;
using SGCAE.Application.Features.Employees.Queries.GetEmployeeById;
using SGCAE.Application.Features.Employees.Queries.GetEmployeesList;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Api.Controllers;
[ApiController]
[Route("api/employees")]
public sealed class EmployeesController(
    CreateEmployeeUseCase createEmployee,
    GetEmployeeByIdUseCase getById,
    GetEmployeesListUseCase getList,
    UpdateEmployeeUseCase updateEmployee,
    DeactivateEmployeeUseCase deactivateEmployee) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command, CancellationToken ct)
    {
        try { var id = await createEmployee.ExecuteAsync(command, ct); return StatusCode(201, new { Id = id }); }
        catch (ValidationException ex) { return BadRequest(new { Message = "Los datos del empleado no son válidos.", Errors = ex.Errors.Select(e => e.ErrorMessage) }); }
        catch (BussinesRuleException ex) { return Conflict(new { Message = ex.Message }); }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await getById.ExecuteAsync(new GetEmployeeByIdQuery(id), ct);
        return result is null ? NotFound(new { Message = "El empleado no fue encontrado." }) : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null, [FromQuery] bool? isActive = null, CancellationToken ct = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
            return BadRequest(new { Message = "Page debe ser >= 1 y pageSize debe estar entre 1 y 100." });
        return Ok(await getList.ExecuteAsync(new GetEmployeesListQuery(page, pageSize, search, isActive), ct));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command, CancellationToken ct)
    {
        try
        {
            bool found = await updateEmployee.ExecuteAsync(id, command, ct);
            return found ? NoContent() : NotFound(new { Message = "El empleado no fue encontrado." });
        }
        catch (ValidationException ex) { return BadRequest(new { Message = "Los datos del empleado no son válidos.", Errors = ex.Errors.Select(e => e.ErrorMessage) }); }
        catch (BussinesRuleException ex) { return Conflict(new { Message = ex.Message }); }
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        bool found = await deactivateEmployee.ExecuteAsync(id, ct);
        return found ? NoContent() : NotFound(new { Message = "El empleado no fue encontrado." });
    }
}
