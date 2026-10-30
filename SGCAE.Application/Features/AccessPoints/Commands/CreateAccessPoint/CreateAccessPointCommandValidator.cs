using FluentValidation;
namespace SGCAE.Application.Features.AccessPoints.Commands.CreateAccessPoint;
public sealed class CreateAccessPointCommandValidator : AbstractValidator<CreateAccessPointCommand>
{
    public CreateAccessPointCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(150);
    }
}
