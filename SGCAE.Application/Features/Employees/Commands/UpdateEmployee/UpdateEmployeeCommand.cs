namespace SGCAE.Application.Features.Employees.Commands.UpdateEmployee;
public sealed record UpdateEmployeeCommand(
    string Identification, string FirstName, string LastName,
    string Email, string Position, string Department);
