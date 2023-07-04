# Bankstatements
This project showcases the creation of an Entity Framework Code First workflow and an ASP.NET Core Web API in C#. This project creates a database with EF Core, creates an ASP.NET Core web API and documents a web API using Swagger/OpenAPI.
# Install
Download the files by running:
```
Run git clone git@gitlab.com:experis5/moviecharacters.git
```
To get your hostname (needed to connect to SQL server):
```
hostname
```
* Install [SQL Server Managment Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver16)
* Install [SQL Express](https://www.microsoft.com/en-us/Download/details.aspx?id=101064)
* Install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

# Extensions VS
* Install [automapper.extensions.microsoft.dependencyinjection](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection)
* Install [microsoft.entityframeworkcore.design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/)
* Install [microsoft.entityframeworkcore.sqlserver](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)
* Install [microsoft.entityframeworkcore.tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/)
* Install [microsoft.visualstudio.web.codegeneration.design](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/)
* Install [swashbuckle.aspnetcore](https://www.nuget.org/packages/Swashbuckle.AspNetCore)

# Usage
* Open SSMS and connect to YOUR-HOSTNAME/SQLEXPRESS
* Open the solution in Visual Studio and install the above mentioned extensions.
* Open the [appsettings.json]-file in VS and change the Data Source in the DefaultConnection to your hostname.
* Open the Package Manager Console and run the command: Update-Database. To instantiate the database in the SQL server.
* Now you are good to go!

# Project Structure
* Controllers: The controllers for each models to handle the interaction with the database. This directory contains two versions of each controller, of which class [entityName]V2Controller uses a service to interact with the context of the database rather than interacting with the context directly
* Migrations: The database migrations that contains information about the database, tables and seeded data
* Models: Classes that represents the data of an entity within the database as well as DTO operations within the database
* Profiles: Profiles that contain the configurations for how the Automapper interacts with the DTO
* Services: Contains the service classes that interacts with the context and passes information through to the corresponding controller

# Authors
Project created by Jonathan de Kleuver and Brian Huynen.
