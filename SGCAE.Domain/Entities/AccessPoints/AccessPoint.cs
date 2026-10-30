using SGCAE.Domain.Exceptions;
namespace SGCAE.Domain.Entities.AccessPoints;
public sealed class AccessPoint
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Location { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private AccessPoint() { }
    public AccessPoint(string name, string location)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new BussinesRuleException("El nombre del punto es obligatorio.");
        if (string.IsNullOrWhiteSpace(location)) throw new BussinesRuleException("La ubicación del punto es obligatoria.");
        Id = Guid.CreateVersion7();
        Name = name.Trim();
        Location = location.Trim();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
    public void Deactivate()
    {
        if (!IsActive) throw new BussinesRuleException("El punto de acceso ya está desactivado.");
        IsActive = false;
    }
    public void Activate()
    {
        if (IsActive) throw new BussinesRuleException("El punto de acceso ya está activo.");
        IsActive = true;
    }
}
