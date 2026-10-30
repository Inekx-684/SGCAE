using FluentValidation;
using SGCAE.Application.Features.AccessRecords.Dtos;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.AccessRecords;
using SGCAE.Domain.Entities.Credentials;
namespace SGCAE.Application.Features.AccessRecords.Commands.RegisterAccess;
public sealed class RegisterAccessUseCase(
    ICredentialsRepository credentials,
    IEmployeesRepository employees,
    IAccessPointsRepository points,
    IAccessRecordsRepository records,
    IUnitOfWork unitOfWork,
    IValidator<RegisterAccessCommand> validator)
{
    public async Task<AccessRecordDto> ExecuteAsync(RegisterAccessCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var credential = await credentials.GetByCodeAsync(command.CredentialCode.Trim(), ct);
        var point = await points.GetByIdAsync(command.AccessPointId, ct);
        var employee = credential is null ? null : await employees.GetByIdAsync(credential.EmployeeId, ct);
        string? denial = null;
        if (credential is null) denial = "Credencial no registrada.";
        else if (credential.Status != CredentialStatus.Active) denial = "Credencial bloqueada o revocada.";
        else if (employee is null || !employee.IsActive) denial = "Empleado inexistente o inactivo.";
        else if (point is null) denial = "Punto de acceso inexistente.";
        else if (!point.IsActive) denial = "Punto de acceso inactivo.";
        var record = new AccessRecord(employee?.Id ?? credential?.EmployeeId, credential?.Id,
            point?.Id, command.CredentialCode, command.Movement,
            denial is null ? AccessDecision.Approved : AccessDecision.Rejected,
            denial ?? "Acceso autorizado.");
        await records.AddAsync(record, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return AccessRecordDto.FromEntity(record);
    }
}
