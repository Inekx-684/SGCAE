using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

namespace SGCAE.Application.Features.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandValidator
    : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Identification)
            .NotEmpty()
            .WithMessage("La identificación es obligatoria.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("El apellido es obligatorio.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("El correo es obligatorio.")
            .EmailAddress()
            .WithMessage("El formato del correo no es válido.");

        RuleFor(x => x.Position)
            .NotEmpty()
            .WithMessage("El cargo es obligatorio.");

        RuleFor(x => x.Department)
            .NotEmpty()
            .WithMessage("El área organizacional es obligatoria.");
    }
}
