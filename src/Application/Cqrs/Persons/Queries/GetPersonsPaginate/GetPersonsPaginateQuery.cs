using DotnetBoilerplate.Application.Common.EntityFramework;
using MediatR;

namespace DotnetBoilerplate.Application.Cqrs.Persons.Queries.GetPersonsPaginate;

public class GetPersonsPaginateQuery : IRequest<ResponseData<GetPersonsPaginateDto>>
{
    public GetPersonsPaginateQuery(RequestData requestData)
    {
        RequestData = requestData;
    }

    public RequestData RequestData { get; }
}
