using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SGCAE.Domain.Exceptions;
using SGCAE.Application.Features.AccessPoints.Commands.CreateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Commands.ActivateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Commands.DeactivateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointById;
using SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointsList;
namespace SGCAE.Api.Controllers;
[ApiController]
[Route("api/access-points")]
public sealed class AccessPointsController(CreateAccessPointUseCase create, GetAccessPointByIdUseCase getById,
    GetAccessPointsListUseCase list, ActivateAccessPointUseCase activate, DeactivateAccessPointUseCase deactivate) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccessPointCommand command, CancellationToken ct)
    {
        try { var id = await create.ExecuteAsync(command, ct); return StatusCode(201, new { Id = id }); }
        catch (ValidationException e) { return BadRequest(new { Message = "Datos del punto de acceso inválidos.", Errors = e.Errors.Select(x => x.ErrorMessage) }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await getById.ExecuteAsync(id, ct);
        return result is null ? NotFound(new { Message = "Punto de acceso no encontrado." }) : Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct) => Ok(await list.ExecuteAsync(ct));
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        try { return await deactivate.ExecuteAsync(id, ct) ? NoContent() : NotFound(new { Message = "Punto de acceso no encontrado." }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken ct)
    {
        try { return await activate.ExecuteAsync(id, ct) ? NoContent() : NotFound(new { Message = "Punto de acceso no encontrado." }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
}
