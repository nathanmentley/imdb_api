# IMDB

## Starting up the services

### Running

There is a docker-compose file at the root of the repo. You can spin up the project using docker compose.

docker-compose up

### Development

For local development, I'm currently spinning up everything with docker-compose and then building the single service I'm working on locally using the standard dotnet utils.

Example, I'd just run dotnet run in the src/web/actors directory.

## Services

### Actor

Imports and exposes data around personal details around actors and cast members.

tools/actormigration will setup and import actor data.
web/actors spins up a rest api around the actor data.

### Titles

Imports and exposes data around shows/movies/media.

tools/titlemigration will setup and import movie/tvshow data.
web/titles spins up a rest api around the movie/tvshow data.

## Databases

Each microservice has it's own database. Right now they're all running on SQL Server.

Each microservice has a migration tool assembly in src/tools/{Service}migration.

docker-compose will execute the migration tools on startup.

## Message Queue.

Active MQ is used to communicate between services. Right now the two web api services will produce messages that a mock background task will consume and print out.

### Creating a migration

efcore has a cli tool we can use to generate migrations.

dotnet ef migrations add init --context IMDBDegrees.DAL.Actors.ActorContext --project ../../dal/actors/IMDBDegrees.DAL.Actors.csproj
