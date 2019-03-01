# everyday-javascript

## Requires
 - .NET Core SDK (latest)
 - npm (version 6 and 8 tested)

## [Development] deployment
```
$ dotnet build
$ dotnet ef database update
$ dotnet run
```

## Completed
- Database scaffolding with SQLite
  - Code First Migrations + Seed data
  - External API data storage
  - .NET Core Identity integration
 - Periodically deliver data to database [tasks begin at XX:00 every hour]
   - Fetch data from external data source
 - Serve stored data through controller endpoints
 - Secure API endpoints with authentication [cookie]
 - Account registration [email] with .NET Core Identity
 - Service registration / Dependency Injection

## TODO
- Test if `Microsoft.EntityFrameworkCore.Sqlite` works without sqlite3 installed.
- Migrate to SQL Server/MySQL
- External service registration [Microsoft, Facebook, Google]
- External service authentication [Microsoft, Facebook, Google]
- Unit Tests
- AWS/Azure Cloud Deployment