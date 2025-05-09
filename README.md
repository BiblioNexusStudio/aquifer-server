## Description

Aquifer Server is the back-end server allowing access to data in the Aquifer.

## Setup

- Make sure you have the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed

## Installation

```bash
dotnet restore
```

Copy `appsettings.example.json` as `appsettings.Development.json` and provide the values needed for your instance.
If you want multiple instances of this file, create them per environment and set your ASPNETCORE_ENVIRONMENT
accordingly. For example: `dotnet run --environment Production`. Environment configurations have no reason
to be checked in.

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

***Important***: Initialize the settings file in the `src/Aquifer.Migrations` directory by copying `appsettings.example.json` as
`appsettings.Development.json` and providing
the values needed for your instance.

Entity Framework will generate migrations by comparing the C# Entities defined
in the project and the current state of the database.

### Add New Entity/Migration

First, create your entity in the `Aquifer.Data/Entities` directory.

Next, add your entity definition to the `src/Aquifer.Data/AquiferDbContext.cs` file.
Entities are listed in an alphabetical order.

### Create a New Migration

To create a new migration, run:

```bash
dotnet ef migrations add --startup-project src/Aquifer.Migrations --project src/Aquifer.Data --context AquiferDbContext <MigrationNameHere>
```

Your new migration will be created in the `src/Aquifer.Data/Migrations` directory along with the `.Designer` file and updated
`AquiferDbContextModelSnapshot.cs` file.

If you run that command and the new migration file is empty, that means there
were no changes detected between the C# Entities and the database. You can use
this to your advantage to create empty migrations and add your own custom code.

To see all migrations (useful before applying them to the DB), run:

```bash
dotnet ef migrations list --startup-project src/Aquifer.Migrations --project src/Aquifer.Data --context AquiferDbContext
```

To run migrations and add your changes to the DB, run:

```bash
dotnet ef database update --startup-project src/Aquifer.Migrations --project src/Aquifer.Data --context AquiferDbContext
```

To remove the last migration (useful when developing locally), run:

```bash
dotnet ef migrations remove --startup-project src/Aquifer.Migrations --project src/Aquifer.Data --context AquiferDbContext
```

## Test

```bash
# unit tests
dotnet test
```

## Jobs/Queues

We use Azure Storage Queues and Azure Functions for queueing and running jobs. To develop locally, you'll need
[Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio,blob-storage)
and [Azure Function Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local).

1. Run Azurite so that you can have local queues. In the `aquifer-server` dir, run `azurite`.
2. Run Aquifer.Jobs using the function core tools. In the `aquifer-server/src/Aquifer.Jobs` run `func start`.

## Client Generation

Aquifer.Well.API supports C# client generation based upon the OpenAPI spec. To generate the client, run:

```bash
dotnet run --project src/Aquifer.Well.API --generateclients true
```

This command assumes that you have a local clone of the bible-well repo that is sitting in a folder next to this repo.
Running the command will update the client code in that repo based upon changes to the OpenAPI spec.