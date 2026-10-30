using System;
using System.Collections.Generic;
using System.Text;

using SGCAE.Application.Features.Employees.Commands.CreateEmployee;

namespace SGCAE.Tests.Application.Employees;

[TestClass]
public class CreateEmployeeCommandValidatorTests
{
    [TestMethod]
    public void Validate_WithValidData_ShouldPass()
    {
        var command = new CreateEmployeeCommand(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );

        var validator = new CreateEmployeeCommandValidator();

        var result = validator.Validate(command);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WithInvalidEmail_ShouldFail()
    {
        var command = new CreateEmployeeCommand(
            "123456789",
            "Juan",
            "Perez",
            "correo-invalido",
            "Operario",
            "Logistica"
        );

        var validator = new CreateEmployeeCommandValidator();

        var result = validator.Validate(command);

        Assert.IsFalse(result.IsValid);
    }
}
