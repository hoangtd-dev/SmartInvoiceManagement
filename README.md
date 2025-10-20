# Invoice Management

### Default Account
DefaultAccount: admin@gmail.com

Password: Admin@123

### Layer Dependencies
- **Core**: No dependencies on other projects
- **Infrastructure**: Depends on Core (for interfaces)
- **Presentation**: Depends on Core (for services and DTOs)

### Using EF for database
Install SQL Server

1. Run command to create db (make sure you create **imdb** database already):

`dotnet ef database update --startup-project .\SIM.Infrastructure`

2. When modify tables in database. Run this command to create new db schema:

`dotnet ef migrations add [name] --startup-project .\SIM.Infrastructure`
After create successful, run command to update:

`dotnet ef database update --startup-project .\SIM.Infrastructure`

# ⚙️ Prerequisites

Before starting, ensure you have the following installed:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later  
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB or full version)  
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) *(optional)*  

---

## 🧩 Database Setup

1. **Create Database**
   - Open **SQL Server Management Studio (SSMS)**  
   - Connect to your SQL Server instance  
   - Run the script `dbscript.sql` located in the project root  

2. **Update Connection String**
   - Locate the connection string in:
     - `appsettings.json` and `appsettings.Development.json` (for dev)

## 🚀 Run the Project

### Using Visual Studio
1. Open the solution `.sln` file in Visual Studio  
2. Set the startup project  
3. Press **F5** or click **Start Debugging**

### Using Command Line
```bash
cd SIM.Presentation
dotnet build
dotnet run