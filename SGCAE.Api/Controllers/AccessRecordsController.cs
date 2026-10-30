using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Features.AccessRecords.Commands.RegisterAccess;
using SGCAE.Application.Features.AccessRecords.Queries.GetAccessHistory;
namespace SGCAE.Api.Controllers;
[ApiController]
[Route("api/access-records")]
public sealed class AccessRecordsController(RegisterAccessUseCase register, GetAccessHistoryUseCase history) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterAccessCommand command, CancellationToken ct)
    {
        try
        {
            var result = await register.ExecuteAsync(command, ct);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Message = "Datos de acceso inválidos.", Errors = ex.Errors.Select(x => x.ErrorMessage) });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetHistory([FromQuery] GetAccessHistoryQuery query, CancellationToken ct)
    {
        try { return Ok(await history.ExecuteAsync(query, ct)); }
        catch (ArgumentException ex) { return BadRequest(new { Message = ex.Message }); }
    }
}
