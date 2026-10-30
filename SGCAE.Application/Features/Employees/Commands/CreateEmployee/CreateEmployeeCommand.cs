using System;
using System.Collections.Generic;
using System.Text;

namespace SGCAE.Application.Features.Employees.Commands.CreateEmployee
{
    public sealed record CreateEmployeeCommand(
    string Identification,
    string FirstName,
    string LastName,
    string Email,
    string Position,
    string Department
);
}
