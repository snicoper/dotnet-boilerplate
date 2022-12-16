using DotnetBoilerplate.Application.Common.Interfaces;
using DotnetBoilerplate.Infrastructure.Persistence;
using DotnetBoilerplate.WebApi.Filters;
using DotnetBoilerplate.WebApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace DotnetBoilerplate.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddRazorPages();

        // Customize default API behavior.
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "DotnetBoilerplate API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the text box: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        // Routing.
        services.AddRouting(options => { options.LowercaseUrls = true; });

        return services;
    }
}
