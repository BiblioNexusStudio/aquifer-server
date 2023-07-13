## Description

Aquifer Server is the back-end server allowing access to data in the Aquifer.

## Installation

```bash
$ yarn install
```

## Setup

- Make sure you're using node 18.16.0.
- `cp .env.example .env` to setup your ENV
- Setup the database (choose one):
    - Authenticate with Azure using the CLI (.env with `USE_LOCAL_DB=false`)
    - Run the docker MSSQL with `cd dev; docker-compose up` (.env with `USE_LOCAL_DB=true`)

## Running the app

```bash
# development
$ yarn run start:dev

# production mode
$ yarn run start:prod
```

## REPL

The REPL is useful for playing around with data from the command line.

```bash
$ yarn run repl
```

Then an example from inside the REPL:

```
> await get(PassagesService).findAll()
```

## Lint

```bash
# lint
$ yarn run lint
```

## Database Migrations

There are two ways to create migrations. Generated and manual.

* **Generated** - This method is easier and preferred, it will look at the
  defined entities and attempt to create a migration by looking at the diff of
  the current database structure and the expected structure.
```bash
$ yarn migration:generate migrations/name-here
```

* **Manual** - If for some reason the generate command isn't working or you
  just need more fine-grained control, use this to create the file and then
  manually write your own queries.
```bash
$ yarn migration:create migrations/name-here
```

To run migrations:
```bash
$ yarn migration:run
```

## Test

```bash
# unit tests
$ yarn run test

# integration tests
$ yarn run test:integration
```

## License

Aquifer Server is [MIT licensed](LICENSE).
