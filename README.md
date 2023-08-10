## Description

Aquifer Server is the back-end server allowing access to data in the Aquifer.

## Setup

- Make sure you have .NET 7 installed

## Installation

```bash
dotnet restore
```

## Running the app

```bash
# with hot reload
dotnet watch run --project src/Aquifer.API

# normal mode
dotnet run --project src/Aquifer.API
```

## CLI REPL

If you want to use the REPL outside of an IDE, csharprepl is a good option.

```bash
# install
dotnet tool install -g csharprepl

# run
csharprepl
```

Then load the project from the csharprepl prompt: `#r "src/Aquifer.API/Aquifer.API.csproj"`

## Lint

```bash
dotnet build --no-incremental /p:WarningsAsErrors=true
```

## Database Migrations

Entity Framework will generate migrations by comparing the C# Entities defined
in the project and the current state of the database.

To create a new migration:
```bash
dotnet ef migrations add --project src/Aquifer.API MigrationNameHere
```

If you run that command and the new migration file is empty, that means there
were no changes detected between the C# Entities and the database. You can use
this to your advantage to create empty migrations and add your own custom code.

To run migrations:
```bash
dotnet ef database update --project src/Aquifer.API
```

## Test

```bash
# unit tests
dotnet test
```
