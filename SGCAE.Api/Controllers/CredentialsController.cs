using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Features.Credentials.Commands.AssignCredential;
using SGCAE.Application.Features.Credentials.Commands.BlockCredential;
using SGCAE.Application.Features.Credentials.Commands.RevokeCredential;
using SGCAE.Application.Features.Credentials.Queries.GetCredentialById;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Api.Controllers;
[ApiController]
[Route("api/credentials")]
public sealed class CredentialsController(AssignCredentialUseCase assign,
    GetCredentialByIdUseCase getById, BlockCredentialUseCase block,
    RevokeCredentialUseCase revoke) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Assign([FromBody] AssignCredentialCommand command, CancellationToken ct)
    {
        try { var id = await assign.ExecuteAsync(command, ct); return StatusCode(201, new { Id = id }); }
        catch (ValidationException e) { return BadRequest(new { Message = "Datos de credencial inválidos.", Errors = e.Errors.Select(x => x.ErrorMessage) }); }
        catch (KeyNotFoundException e) { return NotFound(new { Message = e.Message }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await getById.ExecuteAsync(id, ct);
        return result is null ? NotFound(new { Message = "Credencial no encontrada." }) : Ok(result);
    }
    [HttpPatch("{id:guid}/block")]
    public async Task<IActionResult> Block(Guid id, CancellationToken ct)
    {
        try { return await block.ExecuteAsync(id, ct) ? NoContent() : NotFound(new { Message = "Credencial no encontrada." }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
    [HttpPatch("{id:guid}/revoke")]
    public async Task<IActionResult> Revoke(Guid id, CancellationToken ct)
    {
        try { return await revoke.ExecuteAsync(id, ct) ? NoContent() : NotFound(new { Message = "Credencial no encontrada." }); }
        catch (BussinesRuleException e) { return Conflict(new { Message = e.Message }); }
    }
}
