using DotnetBoilerplate.Application;
using DotnetBoilerplate.Infrastructure;
using DotnetBoilerplate.Infrastructure.Persistence;
using DotnetBoilerplate.WebApi;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder
    .Host
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                theme: AnsiConsoleTheme.Literate);

        if (!hostingContext.HostingEnvironment.IsDevelopment())
        {
            loggerConfiguration.WriteTo.File("web_api_log.txt");
        }
    });

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialize and seed database.
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

// HealCheks.
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true
});

app.UseHttpsRedirection();
app.UseStaticFiles();

// NSwag.
app.UseOpenApi();
app.UseSwaggerUi3(settings => { settings.Path = string.Empty; });
app.UseReDoc(settings => { settings.Path = "/docs"; });

app.UseRouting();
app.UseCors("DefaultCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
