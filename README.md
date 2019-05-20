# IMDB

## Starting up the services

### Running

There is a docker-compose file at the root of the repo. You can spin up the project using docker compose.

docker-compose up

### Development

For local development, I'm currently spinning up everything with docker-compose and then building the single service I'm working on locally using the standard dotent utils.

## Services

### Actor



### Titles


## Databases

Each microservice has it's own database. Right now they're all running on SQL Server.

Each microservice has a migration tool assembly in src/tools/{Service}migration.

docker-compose will execute the migration tools on startup.

### Creating a migration

efcore has a cli tool we can use to generate migrations.

dotnet ef migrations add init --context IMDBDegrees.DAL.Actors.ActorContext --project ../../dal/actors/IMDBDegrees.DAL.Actors.csproj
