# EarthSentry

EarthSentry is a .NET 8 application designed to manage users, posts, comments, and roles with a clean architecture and dependency injection. It leverages Entity Framework Core with PostgreSQL for data persistence.

## Features

- User management (registration, roles, etc.)
- Post creation, voting, and commenting
- Role-based access control
- Clean separation of concerns using dependency injection
- Easily extensible and testable architecture

## Technologies Used

- **.NET 8 / C# 12**
- **Entity Framework Core** (with PostgreSQL)
- **ASP.NET Core Web API**
- **xUnit** for testing

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- (Optional) [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Configuration

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/EarthSentry.git
    cd EarthSentry
    ```

2. Update the connection string in `EarthSentry.Server/appsettings.json`:
    ```json
    "ConnectionStrings": {
      "EarthDB": "Host=localhost;Database=EarthSentryDb;Username=youruser;Password=yourpassword"
    }
    ```

3. Apply database migrations:
    ```sh
    dotnet ef database update --project EarthSentry.Server
    ```
    If you had any issues with migration, you can manually create the database using the following steps:
      - Go to `/Documentation/Database` folder
      - There you will find the SQL scripts to create the database and tables, just execute by the order of files.

 
4. If you want to run on local with DockerDesktop you can follow the scripts under `/Documentation/Infrastructure`

 
5. For the frontend, navigate to the `EarthSentry.Frontend/earthsentry-app` directory and follow the instructions in the README there.


Enjoy using EarthSentry! 🌍