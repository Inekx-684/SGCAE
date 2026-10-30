using System;
using System.Collections.Generic;
using System.Text;


namespace SGCAE.Application.Features.Employees.Dtos;

public sealed record EmployeeDto(
    Guid Id,
    string Identification,
    string FirstName,
    string LastName,
    string Email,
    string Position,
    string Department,
    bool IsActive,
    DateTime CreatedAt
);
