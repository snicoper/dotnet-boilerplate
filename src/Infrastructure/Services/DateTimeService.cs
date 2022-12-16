using DotnetBoilerplate.Application.Common.Interfaces;

namespace DotnetBoilerplate.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
