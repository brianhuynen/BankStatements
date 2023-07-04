# Bankstatements
This project showcases a REST service which receives a single customer statement JSON as a POST data.

## Install the correct .NET version
* Install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Extensions VS
* Install [automapper.extensions.microsoft.dependencyinjection](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection)
* Install [microsoft.aspnetcore.mvc.testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/6.0.19))
* Install [swashbuckle.aspnetcore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.2.3)

## Project Structure
* Controllers: The controllers for each models to handle the interaction with the database using a service to interact with the context of the database.
* Models: Classes that represents the data of an entity within the database as well as DTO operations within the database
* Profiles: Profiles that contain the configurations for how the Automapper interacts with the DTO
* Services: Contains the service classes that interacts with the context and passes information through to the corresponding controller

# Authors
Project created by Brian Huynen.
