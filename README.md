# Invoice Management

### Layer Dependencies
- **Core**: No dependencies on other projects
- **Infrastructure**: Depends on Core (for interfaces)
- **Presentation**: Depends on Core (for services and DTOs)

### Setup DB
Install SQL Server

1. Run command to create db (make sure you create **imdb** database already):
`dotnet ef database update --startup-project .\SIM.Infrastructure`

2. When modify tables in database. Run this command to create new db schema:
`dotnet ef migrations add [name] --startup-project .\SIM.Infrastructure`
After create successful, run command to update:
`dotnet ef database update --startup-project .\SIM.Infrastructure`