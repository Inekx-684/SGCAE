using SGCAE.Domain.Entities.AccessPoints;
namespace SGCAE.Application.Features.AccessPoints.Dtos;
public sealed record AccessPointDto(Guid Id, string Name, string Location, bool IsActive, DateTime CreatedAt)
{
    public static AccessPointDto From(AccessPoint p) => new(p.Id, p.Name, p.Location, p.IsActive, p.CreatedAt);
}
