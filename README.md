# OnlineCourses

Web api initially built with ASP.NET Core 5, currently is migrated to .NET 7, using CQRS pattern with MediatR, with Entity Framework, Dapper, stored procedures, JWT, user authentication, Dto classes and more. SQL Server was used as database engine.

#### Technologies

![](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white) ![](	https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![]()
![]()
![]()

:eight_spoked_asterisk: ASP.NET CORE 5  
:eight_spoked_asterisk: CQRS  
:eight_spoked_asterisk: MEDIATR  
:eight_spoked_asterisk: JWT  
:eight_spoked_asterisk: ENTITY FRAMEWORK  
:eight_spoked_asterisk: DAPPER  

## Deploy in development

1. Clone the project.
2. If you have sql server installed, please change the password and user in the connection string in the ```appsettings.json``` file. Go to step 5.
3. Have docker installed - [Install docker](https://docs.docker.com/engine/install/)
4. Execute the following command
(If you change ``` MSSQL_SA_PASSWORD ``` please change the password in the connection string in the ```appsettings.json``` file.)
  ```
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Secure@Password!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
  ```
5. Restore project packages.
6. Run the project.
7. View api documentation ``` https://localhost:5001/swagger/index.html ```
8. [Get Client App](https://github.com/ralfId/OnlineCourses-Front)
