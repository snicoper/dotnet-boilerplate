using AutoMapper;
using DotnetBoilerplate.Application.Common.Mappings;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Application.Cqrs.Persons.Queries.GetPersonsPaginate;

public class GetPersonsPaginateDto : IMapFrom<Person>
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<Person, GetPersonsPaginateDto>()
            .ForMember(
                dest => dest.FullName,
                act => act.MapFrom(src => $"{src.LastName}, {src.FirstName}"));

    }
}
