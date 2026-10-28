using System;
using System.Collections.Generic;
using System.Text;

using SGCAE.Domain.Entities.Employees;

namespace SGCAE.Application.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task AddAsync(
        Employee employee,
        CancellationToken cancellationToken = default);

    Task<Employee?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdentificationAsync(
        string identification,
        CancellationToken cancellationToken = default);
}

