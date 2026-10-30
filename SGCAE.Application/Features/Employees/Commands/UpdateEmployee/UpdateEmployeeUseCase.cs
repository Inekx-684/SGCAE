using FluentValidation;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Application.Features.Employees.Commands.UpdateEmployee;
public sealed class UpdateEmployeeUseCase(
    IEmployeesRepository repository, IUnitOfWork unitOfWork, IValidator<UpdateEmployeeCommand> validator)
{
    public async Task<bool> ExecuteAsync(Guid id, UpdateEmployeeCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        var employee = await repository.GetByIdAsync(id, ct);
        if (employee is null) return false;
        var identification = command.Identification.Trim();
        if (employee.Identification != identification && await repository.ExistsByIdentificationAsync(identification, ct))
            throw new BussinesRuleException("Ya existe un empleado con esta identificación.");
        employee.UpdateIdentification(identification);
        employee.UpdateFirstName(command.FirstName);
        employee.UpdateLastName(command.LastName);
        employee.UpdateEmail(command.Email);
        employee.UpdatePosition(command.Position);
        employee.UpdateDepartment(command.Department);
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}
