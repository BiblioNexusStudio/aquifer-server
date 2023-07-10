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
$ yarn run start

# watch mode
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

## Test

```bash
# unit tests
$ yarn run test

# integration tests
$ yarn run test:integration
```

## License

Aquifer Server is [MIT licensed](LICENSE).
