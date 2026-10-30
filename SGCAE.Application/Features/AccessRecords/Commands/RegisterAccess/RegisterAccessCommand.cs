using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Application.Features.AccessRecords.Commands.RegisterAccess;
public sealed record RegisterAccessCommand(string CredentialCode, Guid AccessPointId, AccessMovement Movement);
