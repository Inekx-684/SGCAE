using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Domain.Entities.Employees;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Application.Features.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeUseCase
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateEmployeeCommand> _validator;

    public CreateEmployeeUseCase(
        IEmployeesRepository employeesRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateEmployeeCommand> validator)
    {
        _employeesRepository = employeesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Guid> ExecuteAsync(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken = default)
    {
        // 1. Validar los datos del Command
        await _validator.ValidateAndThrowAsync(
            command,
            cancellationToken);

        // 2. Comprobar si la identificación ya existe
        bool exists = await _employeesRepository
            .ExistsByIdentificationAsync(
                command.Identification.Trim(),
                cancellationToken);

        if (exists)
        {
            throw new BussinesRuleException(
                "Ya existe un empleado con esta identificación.");
        }

        // 3. Construir la entidad del dominio
        var employee = new Employee(
            command.Identification,
            command.FirstName,
            command.LastName,
            command.Email,
            command.Position,
            command.Department
        );

        // 4. Agregar la entidad al repositorio
        await _employeesRepository.AddAsync(
            employee,
            cancellationToken);

        // 5. Confirmar los cambios
        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        // 6. Devolver el identificador creado
        return employee.Id;
    }
}

