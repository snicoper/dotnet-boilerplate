using DotnetBoilerplate.Application.Common.Interfaces;
using DotnetBoilerplate.Domain.Entities.Identity;
using DotnetBoilerplate.Infrastructure.Persistence;
using DotnetBoilerplate.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotnetBoilerplate.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(scan =>
               scan.FromCallingAssembly()
                   .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

        // Db configuration.
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assembly)));

        services.AddScoped(provider =>
            (IApplicationDbContext)provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        // Identity.
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7001",
                    ValidAudience = "https://localhost:7001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

        services.AddAuthorization();

        return services;
    }
}
