# Todo List Code Experiments

## Description
The project consists from:
 - Identity.Service
 - TodoList.Service
 - PostgreSQL
 - PgAdmin
These are running in separate docker containers.

Also there is a Service.Utils with utilities for the minimal api.
It is located in a local nuget source as a nuget package.

## Guide
To run the project use:
```
docker compose -f docker-compose.yml up . --build
```
To run in debug mode with *pgadmin* service and *vsdbg* in containers use:
```
docker compose -f docker-compose.yml -f docker-compose.debug.yml . --build
```

|**URL**|**Resource**|**Purpose**|**Comment**|
|-------|------------|-----------|-----------|
|`https://localhost:8444/`|Identity Service|Used for users management||
|`https://localhost:8443/`|TodoList Service|Used for todos management||
|`https://localhost:8444/swagger`|Identity Service Swagger|Identity Service Playground||
|`https://localhost:8443/swagger`|TodoList Service Swagger|TodoList Service Playground||
|`https://localhost:5432`|PostgreSQL Server|Main Database||
|`https://localhost:5050`|PostgreSQL Management Page|GUI for the database|Runs only in development mode|

To disable browser warning install the `develpment.pfx` with password `1111` to the Trusted Root Certification Authorities. Then the https will work fine while in development mode (`ASPNETCORE_ENVIRONMENT=Development`).

## Screenshots

![image](https://github.com/user-attachments/assets/e75aa1cc-b4b8-4c41-a7a3-085ed14fc1e0)
![image](https://github.com/user-attachments/assets/72f90a7e-35cd-45e7-85d8-612efc688052)
![image](https://github.com/user-attachments/assets/303c0fae-793d-440a-bf86-f4f7477343bb)


