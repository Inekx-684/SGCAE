using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
namespace SGCAE.Application.Features.Employees.Commands.DeactivateEmployee;
public sealed class DeactivateEmployeeUseCase(IEmployeesRepository repository, IUnitOfWork unitOfWork)
{
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var employee = await repository.GetByIdAsync(id, ct);
        if (employee is null) return false;
        if (employee.IsActive)
        {
            employee.Deactivate();
            await unitOfWork.SaveChangesAsync(ct);
        }
        return true;
    }
}
