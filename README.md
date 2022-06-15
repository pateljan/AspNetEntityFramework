# AspNetEntityFramework

Entity Framework Core tool installation
------------------------------------------
https://docs.microsoft.com/en-us/ef/core/cli/dotnet

- command to install tool ->
   dotnet tool install --global dotnet-ef

- command to update tool ->
  dotnet tool update --global dotnet-ef

- start using ef command -> dotnet ef

# Package Manager Console Entity framework database commands 

1. Add-Migration Initial   
2. Update-Database
3. Remove-Migration

# CLI Commands
1. dotnet ef migrations add initial
2. dotnet ef database update
   
# Database First Command
1. scaffold-DbContext -Connection name=DafultConnection -Provider Microsoft.EntityFrameworkcore.SqlServer -OutputDir Models -Force
