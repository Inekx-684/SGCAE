using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Application.Features.AccessRecords.Dtos;
public sealed record AccessRecordDto(Guid Id, Guid? EmployeeId, Guid? CredentialId,
    Guid? AccessPointId, string CredentialCode, AccessMovement Movement,
    AccessDecision Decision, string Reason, DateTime OccurredAt)
{
    public static AccessRecordDto FromEntity(AccessRecord x) => new(x.Id, x.EmployeeId,
        x.CredentialId, x.AccessPointId, x.CredentialCode, x.Movement, x.Decision, x.Reason, x.OccurredAt);
}
public sealed record PagedAccessRecordsDto(IReadOnlyList<AccessRecordDto> Items,
    int TotalCount, int PageNumber, int PageSize);
