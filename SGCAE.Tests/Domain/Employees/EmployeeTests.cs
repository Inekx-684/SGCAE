using System;
using System.Collections.Generic;
using System.Text;

using SGCAE.Domain.Entities.Employees;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Tests.Domain.Employees;

[TestClass]
public class EmployeeTests
{
    [TestMethod]
    public void CreateEmployee_ShouldBeActive()
    {
        // Arrange y Act
        var employee = new Employee(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );

        // Assert
        Assert.IsTrue(employee.IsActive);
        Assert.AreEqual("Juan", employee.FirstName);
    }

    [TestMethod]
    public void DeactivateEmployee_ShouldBeInactive()
    {
        var employee = new Employee(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );

        employee.Deactivate();

        Assert.IsFalse(employee.IsActive);
    }

    [TestMethod]
    public void CreateEmployee_WithoutName_ShouldFail()
    {
        Assert.ThrowsExactly<BussinesRuleException>(() =>
            new Employee(
                "123456789",
                "",
                "Perez",
                "juan@empresa.com",
                "Operario",
                "Logistica"
            )
        );
    }


    [TestMethod]
    public void UpdatePosition_ShouldChangePosition()
    {
        // Arrange
        var employee = new Employee(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );

        // Act
        employee.UpdatePosition("Supervisor");

        // Assert
        Assert.AreEqual("Supervisor", employee.Position);
    }

    [TestMethod]
    public void UpdateEmail_WithoutValue_ShouldFail()
    {
        // Arrange
        var employee = new Employee(
            "123456789",
            "Juan",
            "Perez",
            "juan@empresa.com",
            "Operario",
            "Logistica"
        );

        // Act y Assert
        Assert.ThrowsExactly<BussinesRuleException>(() =>
            employee.UpdateEmail("")
        );

        Assert.AreEqual("juan@empresa.com", employee.Email);
    }

}
