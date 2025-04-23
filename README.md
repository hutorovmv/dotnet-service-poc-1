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
docker compose -f docker-compose.yml up --build
```
To run in debug mode with *pgadmin* service and *vsdbg* in containers use:
```
docker compose -f docker-compose.yml -f docker-compose.debug.yml up --build
```
Use the `--build` flag only when downloading new version.

|**URL**|**Resource**|**Purpose**|**Comment**|**User**|**Password**|
|-------|------------|-----------|:---------:|:------:|:----------:|
|`https://localhost:8444/`|Identity Service|Used for users management|-|-|-|
|`https://localhost:8443/`|TodoList Service|Used for todos management|-|-|-|
|`https://localhost:8444/swagger`|Identity Service Swagger|Identity Service Playground|-|-|-|
|`https://localhost:8443/swagger`|TodoList Service Swagger|TodoList Service Playground|-|-|-|
|`https://localhost:5432`|PostgreSQL Server|Main Database|-|`postgres`|`postgres`|
|`http://localhost:5050`|PostgreSQL Management Page|GUI for the database|Runs only in development mode|`admin@admin.com`|`admin`|

To disable browser warning install the `develpment.pfx` with password `1111` to the Trusted Root Certification Authorities. Then the https will work fine while in development mode (`ASPNETCORE_ENVIRONMENT=Development`).

## Authorization

For authorization in `TodoList.Service` send the `Authorization` header with `Bearer <token>`.

Request example:
```
var token = "<token>";

fetch('https://localhost:8443/api/todo', {
  method: 'POST',
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    "id": 0,
    "name": "string",
    "isComplete": true
  })
})
.then(res => res.json())
.then(data => console.log(data))
.catch(err => console.error(err));
```

## Screenshots

![image](https://github.com/user-attachments/assets/e75aa1cc-b4b8-4c41-a7a3-085ed14fc1e0)
![image](https://github.com/user-attachments/assets/72f90a7e-35cd-45e7-85d8-612efc688052)
![image](https://github.com/user-attachments/assets/303c0fae-793d-440a-bf86-f4f7477343bb)
