using FluentValidation;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Credentials;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Application.Features.Credentials.Commands.AssignCredential;
public sealed class AssignCredentialUseCase(
    IEmployeesRepository employees, ICredentialsRepository credentials,
    IUnitOfWork unitOfWork, IValidator<AssignCredentialCommand> validator)
{
    public async Task<Guid> ExecuteAsync(AssignCredentialCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var employee = await employees.GetByIdAsync(command.EmployeeId, ct);
        if (employee is null) throw new KeyNotFoundException("El empleado no fue encontrado.");
        if (!employee.IsActive) throw new BussinesRuleException("No se puede asignar una credencial a un empleado inactivo.");
        var code = command.Code.Trim();
        if (await credentials.ExistsByCodeAsync(code, ct)) throw new BussinesRuleException("El código de credencial ya existe.");
        if (await credentials.HasActiveCredentialAsync(employee.Id, ct)) throw new BussinesRuleException("El empleado ya tiene una credencial activa.");
        var credential = new Credential(employee.Id, code);
        await credentials.AddAsync(credential, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return credential.Id;
    }
}
