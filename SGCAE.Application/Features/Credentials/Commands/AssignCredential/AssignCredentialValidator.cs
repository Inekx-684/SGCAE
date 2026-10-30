using FluentValidation;
namespace SGCAE.Application.Features.Credentials.Commands.AssignCredential;
public sealed class AssignCredentialValidator : AbstractValidator<AssignCredentialCommand>
{
    public AssignCredentialValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
    }
}
