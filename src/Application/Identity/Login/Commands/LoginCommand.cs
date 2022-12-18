using MediatR;

namespace DotnetBoilerplate.Application.Identity.Login.Commands;

public class LoginCommand : IRequest<LoginDto>
{
    public LoginCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; }

    public string Password { get; }
}
