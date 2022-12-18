using MediatR;

namespace DotnetBoilerplate.Application.Identity.Login.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
{
    public LoginHandler()
    {
    }

    public Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
