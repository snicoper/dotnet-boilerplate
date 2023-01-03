using AutoMapper;
using DotnetBoilerplate.Application.Common.EntityFramework;
using DotnetBoilerplate.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Application.Cqrs.Persons.Queries.GetPersonsPaginate;

public class GetPersonsPaginateHandler : IRequestHandler<GetPersonsPaginateQuery, ResponseData<GetPersonsPaginateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPersonsPaginateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseData<GetPersonsPaginateDto>> Handle(
        GetPersonsPaginateQuery request,
        CancellationToken cancellationToken)
    {
        var persons = _context
            .Persons
            .AsNoTracking();

        return await ResponseData<GetPersonsPaginateDto>.CreateAsync(
            persons,
            request.RequestData,
            _mapper,
            cancellationToken);
    }
}
