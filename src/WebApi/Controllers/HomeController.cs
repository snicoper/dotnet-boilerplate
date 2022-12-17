using DotnetBolerplate.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.WebApi.Controllers;

[Route("api/v{version:apiVersion}/home")]
public class HomeController : ApiControllerBase
{
    public ActionResult<string> Hello()
    {
        return "Hello world 23";
    }
}
