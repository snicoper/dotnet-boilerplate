using FluentValidation;

namespace DotnetBoilerplate.Application.Identity.Login.Commands;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty();

        RuleFor(r => r.Password)
            .NotEmpty();
    }
}
