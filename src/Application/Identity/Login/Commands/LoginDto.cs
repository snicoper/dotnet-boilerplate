namespace DotnetBoilerplate.Application.Identity.Login.Commands;

public class LoginDto
{
    public bool Success { get; set; } = false;

    public string? Token { get; set; }
}
