using DotnetBoilerplate.Application.Common.EntityFramework;
using DotnetBoilerplate.Application.Cqrs.Persons.Queries.GetPersonsPaginate;
using DotnetBolerplate.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBoilerplate.WebApi.Controllers;

[Route("api/v{version:apiVersion}/persons")]
public class PersonsController : ApiControllerBase
{
    [HttpGet("paginated")]
    public async Task<ActionResult<ResponseData<GetPersonsPaginateDto>>> GetPersonsPaginated(
        [FromQuery] RequestData requestData)
    {
        return await Mediator.Send(new GetPersonsPaginateQuery(requestData));
    }
}
