using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Employees;
using SGCAE.Persistence.Context;
namespace SGCAE.Persistence.Repositories.Employees;
public sealed class EmployeesRepository : IEmployeesRepository
{
    private readonly DataContext _context;
    public EmployeesRepository(DataContext context) => _context = context;
    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
        => await _context.Employees.AddAsync(employee, cancellationToken);
    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    public Task<bool> ExistsByIdentificationAsync(string identification, CancellationToken cancellationToken = default)
        => _context.Employees.AnyAsync(x => x.Identification == identification, cancellationToken);
    public async Task<(IReadOnlyList<Employee> Items, int Total)> GetPagedAsync(
        int page, int pageSize, string? search, bool? isActive, CancellationToken cancellationToken = default)
    {
        IQueryable<Employee> query = _context.Employees.AsNoTracking();
        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);
        if (!string.IsNullOrWhiteSpace(search))
        {
            string value = search.Trim();
            query = query.Where(x => x.Identification.Contains(value)
                || x.FirstName.Contains(value) || x.LastName.Contains(value)
                || x.Email.Contains(value));
        }
        int total = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }
}
