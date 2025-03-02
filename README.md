# Student Management System
Installation  package
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package xUnit
Install-Package Moq
Install-Package Microsoft.EntityFrameworkCore.InMemory
Install-Package Microsoft.AspNetCore.Mvc.Testin

DB migration
dotnet ef migrations add InitialCreate

dotnet ef database update
