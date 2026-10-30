using SGCAE.Domain.Exceptions;
namespace SGCAE.Domain.Entities.AccessRecords;
public enum AccessMovement { Entry = 1, Exit = 2 }
public enum AccessDecision { Approved = 1, Rejected = 2 }
public sealed class AccessRecord
{
    public Guid Id { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public Guid? CredentialId { get; private set; }
    public Guid? AccessPointId { get; private set; }
    public string CredentialCode { get; private set; } = null!;
    public AccessMovement Movement { get; private set; }
    public AccessDecision Decision { get; private set; }
    public string Reason { get; private set; } = null!;
    public DateTime OccurredAt { get; private set; }
    private AccessRecord() { }
    public AccessRecord(Guid? employeeId, Guid? credentialId, Guid? accessPointId,
        string credentialCode, AccessMovement movement, AccessDecision decision, string reason)
    {
        if (string.IsNullOrWhiteSpace(credentialCode)) throw new BussinesRuleException("El código de credencial es obligatorio.");
        if (!Enum.IsDefined(movement)) throw new BussinesRuleException("Movimiento inválido.");
        if (!Enum.IsDefined(decision)) throw new BussinesRuleException("Resultado inválido.");
        if (string.IsNullOrWhiteSpace(reason)) throw new BussinesRuleException("Debe indicar el motivo del resultado.");
        Id = Guid.CreateVersion7(); EmployeeId = employeeId; CredentialId = credentialId;
        AccessPointId = accessPointId; CredentialCode = credentialCode.Trim();
        Movement = movement; Decision = decision; Reason = reason.Trim(); OccurredAt = DateTime.UtcNow;
    }
}
