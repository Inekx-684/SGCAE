using SGCAE.Domain.Entities.Employees;
namespace SGCAE.Application.Interfaces.Repositories;
public interface IEmployeesRepository
{
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdentificationAsync(string identification, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Employee> Items, int Total)> GetPagedAsync(
        int page, int pageSize, string? search, bool? isActive, CancellationToken cancellationToken = default);
}
