using FluentValidation;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Domain.Entities.AccessPoints;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Application.Features.AccessPoints.Commands.CreateAccessPoint;
public sealed class CreateAccessPointUseCase(IAccessPointsRepository repository, IUnitOfWork unitOfWork, IValidator<CreateAccessPointCommand> validator)
{
    public async Task<Guid> ExecuteAsync(CreateAccessPointCommand command, CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        if (await repository.ExistsByNameAsync(command.Name.Trim(), ct))
            throw new BussinesRuleException("Ya existe un punto de acceso con ese nombre.");
        var point = new AccessPoint(command.Name, command.Location);
        await repository.AddAsync(point, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return point.Id;
    }
}
