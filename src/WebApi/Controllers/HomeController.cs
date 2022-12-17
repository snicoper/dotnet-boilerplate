using DotnetBoilerplate.Application.Common.Interfaces;
using DotnetBolerplate.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.WebApi.Controllers;

[Route("api/v{version:apiVersion}/home")]
public class HomeController : ApiControllerBase
{
    private readonly IValidationFailureService _validationFailureService;

    public HomeController(IValidationFailureService validationFailureService)
    {
        _validationFailureService = validationFailureService;
    }

    public ActionResult<string> Hello()
    {
        _validationFailureService.AddAndRaiseException("Error 1", "Comentario del error");

        return "Hello world";
    }
}
