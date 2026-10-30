using FluentValidation;
using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Application.Features.AccessRecords.Commands.RegisterAccess;
public sealed class RegisterAccessCommandValidator : AbstractValidator<RegisterAccessCommand>
{
    public RegisterAccessCommandValidator()
    {
        RuleFor(x => x.CredentialCode).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AccessPointId).NotEmpty();
        RuleFor(x => x.Movement).IsInEnum();
    }
}
