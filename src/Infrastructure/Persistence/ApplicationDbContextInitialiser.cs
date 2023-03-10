using DotnetBoilerplate.Domain.Entities;
using DotnetBoilerplate.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetBoilerplate.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRoles = new IdentityRole[]
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Staff")
        };

        foreach (var role in administratorRoles)
        {
            if (_roleManager.Roles.All(r => r.Name != role.Name))
            {
                await _roleManager.CreateAsync(role);
            }
        }

        // Default users
        var administrator = new ApplicationUser
        {
            UserName = "administrator@localhost",
            Email = "administrator@localhost"
        };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, administratorRoles.Select(a => a.Name ?? string.Empty));
        }

        // Default data
        // Seed, if necessary
        await CreatePersonsDataAsync();
    }

    private async Task CreatePersonsDataAsync()
    {
        if (_context.Persons.Any())
        {
            return;
        }

        var persons = new List<Person>
        {
            new Person{ FirstName = "Adam1", LastName = "Prueba", Email = "adam1@example.com" },
            new Person{ FirstName = "Adam2", LastName = "Prueba", Email = "adam2@example.com" },
            new Person{ FirstName = "Adam3", LastName = "Prueba", Email = "adam3@example.com" },
            new Person{ FirstName = "Adam4", LastName = "Prueba", Email = "adam4@example.com" },
            new Person{ FirstName = "Adam5", LastName = "Prueba", Email = "adam5@example.com" }
        };

        await _context.Persons.AddRangeAsync(persons);
        await _context.SaveChangesAsync(CancellationToken.None);
    }
}
