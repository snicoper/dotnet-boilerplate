using DotnetBoilerplate.Application.Cqrs.Identity.Commands.Login;
using DotnetBolerplate.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.WebApi.Controllers.Identity;

[Route("api/v{version:apiVersion}/identity")]
public class IdentityController : ApiControllerBase
{
    /// <summary>
    /// Identificación de un usuario.
    /// </summary>
    /// <returns>Token en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<LoginDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
