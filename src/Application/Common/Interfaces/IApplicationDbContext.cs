using DotnetBoilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Person> Persons { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

