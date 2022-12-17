# AgendaOne

`dotnet tool install -g dotnet-ef`

## Migración

```bash
cd src/WebApi

dotnet ef migrations add Initial \
    -p ../Infrastructure/Infrastructure.csproj \
    -c ApplicationDbContext \
    -o ../Infrastructure/Persistence/Migrations/ApplicationMigrations

dotnet ef database update -c ApplicationDbContext
```
