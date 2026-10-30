using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGCAE.Application.Features.Employees.Commands.CreateEmployee;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Employees;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Tests.Application.Employees;

[TestClass]
public class CreateEmployeeUseCaseTests
{
    private sealed class FakeEmployeesRepository
        : IEmployeesRepository
    {
        public List<Employee> Employees { get; } = new();

        public Task<(IReadOnlyList<Employee> Items, int Total)> GetPagedAsync(
            int page, int pageSize, string? search, bool? isActive, CancellationToken cancellationToken = default)
        {
            IEnumerable<Employee> filtered = Employees;
            if (isActive.HasValue) filtered = filtered.Where(x => x.IsActive == isActive.Value);
            if (!string.IsNullOrWhiteSpace(search)) filtered = filtered.Where(x => x.Identification.Contains(search) || x.FirstName.Contains(search));
            var list = filtered.ToList();
            return Task.FromResult(((IReadOnlyList<Employee>)list.Skip((page - 1) * pageSize).Take(pageSize).ToList(), list.Count));
        }

        public Task AddAsync(
            Employee employee,
            CancellationToken cancellationToken = default)
        {
            Employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task<Employee?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Employees.FirstOrDefault(x => x.Id == id));
        }

        public Task<bool> ExistsByIdentificationAsync(
            string identification,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                Employees.Any(x =>
                    x.Identification == identification));
        }
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public int SaveCalls { get; private set; }

        public Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            SaveCalls++;
            return Task.FromResult(1);
        }
    }

    private static CreateEmployeeCommand ValidCommand()
    {
        return new CreateEmployeeCommand(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );
    }

    [TestMethod]
    public async Task ExecuteAsync_WithValidData_ShouldSaveEmployee()
    {
        // Arrange
        var repository = new FakeEmployeesRepository();
        var unitOfWork = new FakeUnitOfWork();
        var validator = new CreateEmployeeCommandValidator();

        var useCase = new CreateEmployeeUseCase(
            repository, unitOfWork, validator);

        // Act
        Guid employeeId = await useCase.ExecuteAsync(
            ValidCommand());

        // Assert
        Assert.AreNotEqual(Guid.Empty, employeeId);
        Assert.HasCount(1, repository.Employees);
        Assert.AreEqual(employeeId, repository.Employees[0].Id);
        Assert.AreEqual(1, unitOfWork.SaveCalls);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithDuplicateIdentification_ShouldFail()
    {
        // Arrange
        var repository = new FakeEmployeesRepository();
        var unitOfWork = new FakeUnitOfWork();
        var validator = new CreateEmployeeCommandValidator();

        var useCase = new CreateEmployeeUseCase(
            repository, unitOfWork, validator);

        await useCase.ExecuteAsync(ValidCommand());

        // Act y Assert
        await Assert.ThrowsExactlyAsync<BussinesRuleException>(
            () => useCase.ExecuteAsync(ValidCommand()));

        Assert.HasCount(1, repository.Employees);
        Assert.AreEqual(1, unitOfWork.SaveCalls);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithInvalidEmail_ShouldNotSave()
    {
        // Arrange
        var repository = new FakeEmployeesRepository();
        var unitOfWork = new FakeUnitOfWork();
        var validator = new CreateEmployeeCommandValidator();

        var useCase = new CreateEmployeeUseCase(
            repository, unitOfWork, validator);

        var command = ValidCommand() with
        {
            Email = "correo-invalido"
        };

        // Act y Assert
        await Assert.ThrowsExactlyAsync<ValidationException>(
            () => useCase.ExecuteAsync(command));

        Assert.HasCount(0, repository.Employees);
        Assert.AreEqual(0, unitOfWork.SaveCalls);
    }
}
