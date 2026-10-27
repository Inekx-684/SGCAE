using System;
using System.Collections.Generic;
using System.Text;

using SGCAE.Domain.Exceptions;

namespace SGCAE.Domain.Entities.Employees;

public sealed class Employee
{
    public Guid Id { get; private set; }

    public string Identification { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Position { get; private set; } = null!;

    public string Department { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Employee()
    {
    }

    public Employee(
        string identification,
        string firstName,
        string lastName,
        string email,
        string position,
        string department)
    {
        ApplyRequiredRule(identification, "La identificación");
        ApplyRequiredRule(firstName, "El nombre");
        ApplyRequiredRule(lastName, "El apellido");
        ApplyRequiredRule(email, "El correo");
        ApplyRequiredRule(position, "El cargo");
        ApplyRequiredRule(department, "El área");

        Id = Guid.CreateVersion7();
        Identification = identification.Trim();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        Position = position.Trim();
        Department = department.Trim();

        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    private static void ApplyRequiredRule(
        string value,
        string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BussinesRuleException(
                $"{fieldName} es requerido.");
        }
    }


    public void UpdateIdentification(string identification)
    {
        ApplyRequiredRule(identification, "La identificación");
        Identification = identification.Trim();
    }

    public void UpdateFirstName(string firstName)
    {
        ApplyRequiredRule(firstName, "El nombre");
        FirstName = firstName.Trim();
    }

    public void UpdateLastName(string lastName)
    {
        ApplyRequiredRule(lastName, "El apellido");
        LastName = lastName.Trim();
    }

    public void UpdateEmail(string email)
    {
        ApplyRequiredRule(email, "El correo");
        Email = email.Trim();
    }

    public void UpdatePosition(string position)
    {
        ApplyRequiredRule(position, "El cargo");
        Position = position.Trim();
    }

    public void UpdateDepartment(string department)
    {
        ApplyRequiredRule(department, "El área");
        Department = department.Trim();
    }

}
