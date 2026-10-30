using SGCAE.Domain.Entities.Credentials;
namespace SGCAE.Application.Features.Credentials.Dtos;
public sealed record CredentialDto(Guid Id, Guid EmployeeId, string Code, string Status,
    DateTime AssignedAt, DateTime? BlockedAt, DateTime? RevokedAt)
{
    public static CredentialDto From(Credential c) => new(c.Id, c.EmployeeId, c.Code,
        c.Status.ToString(), c.AssignedAt, c.BlockedAt, c.RevokedAt);
}
