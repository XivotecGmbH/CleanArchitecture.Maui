<!-- Project Shields & URLs -->
[github_build-shield]: https://img.shields.io/badge/
[github_build-url]: https://img.shields.io/github/actions/workflow/status/XivotecGmbH/CleanArchitecture.Maui/.github%2Fworkflows%2Fbuild.yml
[license-shield]: https://img.shields.io/github/license/XivotecGmbH/CleanArchitecture.Maui
[license-url]: https://github.com/XivotecGmbH/CleanArchitecture.Maui/blob/master/LICENSE
[contributors-shield]: https://img.shields.io/github/contributors/XivotecGmbH/CleanArchitecture.Maui.svg
[contributors-url]: https://github.com/XivotecGmbH/CleanArchitecture.Maui/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/XivotecGmbH/CleanArchitecture.Maui
[forks-url]: https://github.com/XivotecGmbH/CleanArchitecture.Maui/network/members
[issues-shield]: https://img.shields.io/github/issues/XivotecGmbH/CleanArchitecture.Maui
[issues-url]: https://github.com/XivotecGmbH/CleanArchitecture.Maui/issues

[nuget-shield]: https://img.shields.io/nuget/v/Xivotec.CleanArchitecture.Maui.Template?label=NuGet
[nuget-url]: https://www.nuget.org/packages/Xivotec.CleanArchitecture.Maui.Template
[nuget-d-shield]: https://img.shields.io/nuget/dt/Xivotec.CleanArchitecture.Maui.Template?label=Downloads
[nuget-d-url]: https://www.nuget.org/packages/Xivotec.CleanArchitecture.Maui.Template

[website-shield]: https://img.shields.io/badge/Xivotec-blue
[website-url]: https://xivotec.com/
[instagram-shield]: https://img.shields.io/badge/Xivotec-blue?logo=instagram&logoColor=white
[instagram-url]: https://www.instagram.com/xivotec
[linkedin-shield]: https://img.shields.io/badge/Xivotec-blue?logo=linkedin&logoColor=white
[linkedin-url]: https://de.linkedin.com/company/xivotec

# A Clean Architecture .NET MAUI Solution Template
![github_build-url]
[![License][license-shield]][license-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Issues][issues-shield]][issues-url]

[![NugetLink][nuget-shield]][nuget-url]
[![NugetDownloads][nuget-d-shield]][nuget-d-url]

[![Website][website-shield]][website-url]
[![Instagram][instagram-shield]][instagram-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

The goal of this template is to provide a straightforward and efficient approach for application development, leveraging the combined power of Clean Architecture and .Net MAUI.
Using this template, you can easily create a multi platform app using .NET MAUI, while adhering to the core principles of Clean Architecture.

## Getting Started
The easiest way to get started with this template is to install the [NuGet package][nuget-d-url]

### Prerequisites
- Install the latest .NET 8.x SDK & Tools
- Install the latest version of Visual Studio IDE
- Install the latest .NET MAUI package
- Install / have access to a PostgreSQL database (optional, see below)
- Enable Developer Mode on your device (required for debugging .NET MAUI applications)

### Installation
[1] Open the command prompt and run:

```bash
dotnet new install Xivotec.CleanArchitecture.Maui.Template
```

[2] Once installed, create a new solution in your project folder or from Visual Studio :

```bash
dotnet new xt-camaui-sln -n <YourProjectName>
```

Because .NET MAUI is packaged by default, `dotnet run` won't work.  
Instead, open your solution in Visual Studio directly and run it from there.

## Database

The template is configured to use PostgreSQL as a database provider by default. If you want to use another provider, you need to exchange `.RegisterPostgreSqlPortServices()`
in the `Presentation` project `MauiProgram.cs` file and the `Infrastructure.PostgreSQLPort` project itself with a corresponding implementation.

The database connection string is set in the `appsettings.json` file in the `Presentation` project.

Once you run the application, the database will be automatically created (if necessary) and the latest migrations will be applied.

## License

This project is licensed with the [MIT license](LICENSE).

## Support

If you have any problem, please let us know by raising a new issue.

If you have suggestions on how to improve or extend the template, let us know via email.  
Our homepage is linked in the banners at the top.

## Technologies Used

Main technologies:
* [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)
* [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui)
* [CommunityToolkit.Mvvm](https://learn.microsoft.com/de-de/dotnet/communitytoolkit/mvvm/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)

Unit Tests:  
* [XUnit](https://xunit.net/)
* [FluentAssertions](https://fluentassertions.com/)
* [NSubstitute](https://nsubstitute.github.io/)

## Learn More

* [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)
* [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)