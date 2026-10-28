using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Employees;
using SGCAE.Persistence.Context;

namespace SGCAE.Persistence.Repositories.Employees;

public sealed class EmployeesRepository : IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Employee employee,
        CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(
            employee,
            cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<bool> ExistsByIdentificationAsync(
        string identification,
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AnyAsync(
                x => x.Identification == identification,
                cancellationToken);
    }
}

