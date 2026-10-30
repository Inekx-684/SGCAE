using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Application.Features.AccessRecords.Queries.GetAccessHistory;
public sealed record GetAccessHistoryQuery(Guid? EmployeeId = null, Guid? AccessPointId = null,
    AccessDecision? Decision = null, DateTime? FromUtc = null, DateTime? ToUtc = null,
    int PageNumber = 1, int PageSize = 10);
