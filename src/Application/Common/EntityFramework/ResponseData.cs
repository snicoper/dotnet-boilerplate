using AutoMapper;
using DotnetBoilerplate.Application.Common.EntityFramework.Filter;
using DotnetBoilerplate.Application.Common.EntityFramework.OrderBy;
using DotnetBoilerplate.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Application.Common.EntityFramework;

public class ResponseData<TDto> : RequestData
{
    public ResponseData()
    {
        Items = new HashSet<TDto>();
    }

    public IEnumerable<TDto> Items { get; private init; }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<ResponseData<TDto>> CreateAsync<TEntity>(
        IQueryable<TEntity> source,
        RequestData request,
        IMapper mapper,
        CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        var totalItems = await source.Filter(request).Ordering(request).CountAsync(cancellationToken);

        var items = await source
            .Filter(request)
            .Ordering(request)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var itemsDto = mapper.Map<List<TDto>>(items);

        return CreateResponseDataFromResult(request, itemsDto, totalItems);
    }

    private static ResponseData<TDto> CreateResponseDataFromResult(
        RequestData request,
        IEnumerable<TDto> items,
        int totalItems)
    {
        return new()
        {
            TotalItems = totalItems,
            PageNumber = request.PageNumber,
            TotalPages = CalculateTotalPages(totalItems, request.PageSize),
            Ratio = request.Ratio,
            PageSize = request.PageSize,
            Items = items,
            Orders = request.Orders,
            Filters = request.Filters
        };
    }

    private static int CalculateTotalPages(int totalItems, int pageSize)
    {
        return (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}
