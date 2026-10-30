using SGCAE.Domain.Exceptions;
namespace SGCAE.Domain.Entities.Credentials;
public enum CredentialStatus { Active, Blocked, Revoked }
public sealed class Credential
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Code { get; private set; } = null!;
    public CredentialStatus Status { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public DateTime? BlockedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    private Credential() { }
    public Credential(Guid employeeId, string code)
    {
        if (employeeId == Guid.Empty) throw new BussinesRuleException("El empleado es obligatorio.");
        if (string.IsNullOrWhiteSpace(code)) throw new BussinesRuleException("El código de credencial es obligatorio.");
        Id = Guid.CreateVersion7();
        EmployeeId = employeeId;
        Code = code.Trim();
        Status = CredentialStatus.Active;
        AssignedAt = DateTime.UtcNow;
    }
    public void Block()
    {
        if (Status != CredentialStatus.Active) throw new BussinesRuleException("Solamente una credencial activa puede bloquearse.");
        Status = CredentialStatus.Blocked;
        BlockedAt = DateTime.UtcNow;
    }
    public void Revoke()
    {
        if (Status == CredentialStatus.Revoked) throw new BussinesRuleException("La credencial ya está revocada.");
        Status = CredentialStatus.Revoked;
        RevokedAt = DateTime.UtcNow;
    }
}
