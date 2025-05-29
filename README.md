# Dotnet Microservices Template

[![Build Status](https://github.com/hutorovmv/dotnet-service-poc-1/actions/workflows/docker-compose-run.yml/badge.svg)](https://github.com/hutorovmv/dotnet-service-poc-1/actions/workflows/docker-compose-run.yml)

## Overview

A modern .NET microservices starter template. This solution demonstrates best practices for building, running, and maintaining distributed services with .NET 9, Docker, and GitHub Actions.

### Key Technologies

- **.NET 9** (Minimal API)
- **NuGet Package** (shared utilities)
- **PostgreSQL** (database)
- **Entity Framework** (ORM)
- **Docker & Docker Compose** (containerization)
- **GitHub Actions** (CI/CD)
- **PgAdmin** (database management)

### Services

- **Identity.Service** – User authentication and management
- **TodoList.Service** – Todo management API
- **PostgreSQL** – Main database
- **PgAdmin** – Database GUI (development only)

All services run in isolated Docker containers. Start the full stack with Docker Compose.

The `Service.Utils` package (local NuGet source) provides shared utilities for Minimal APIs.

## Getting Started

### Running the Stack

Start all services:

```sh
docker compose -f docker-compose.yml up --build
```

For debugging (includes *pgadmin* and *vsdbg*):

```sh
docker compose -f docker-compose.yml -f docker-compose.debug.yml up --build
```

> 📝 **Note:** Use `--build` when updating image versions.

### Service Endpoints

| **URL**                        | **Resource**                | **Purpose**                | **Comment**                      | **User**           | **Password**      |
|---------------------------------|-----------------------------|----------------------------|-----------------------------------|--------------------|-------------------|
| `https://localhost:8444/`      | Identity Service            | User management            | -                                 | -                  | -                 |
| `https://localhost:8443/`      | TodoList Service            | Todo management            | -                                 | -                  | -                 |
| `https://localhost:8444/swagger` | Identity Service Swagger   | API playground             | -                                 | -                  | -                 |
| `https://localhost:8443/swagger` | TodoList Service Swagger   | API playground             | -                                 | -                  | -                 |
| `https://localhost:5432`       | PostgreSQL Server           | Main database              | -                                 | `postgres`         | `postgres`        |
| `http://localhost:5050`        | PgAdmin                     | Database GUI               | Dev mode only                     | `admin@admin.com`  | `admin`           |

> 🖊️ **Tip:** To avoid browser HTTPS warnings, install `develpment.pfx` (password: `1111`) to Trusted Root Certification Authorities. This enables trusted HTTPS in development (`ASPNETCORE_ENVIRONMENT=Development`).

## Authorization

To access protected endpoints in `TodoList.Service`, include a Bearer token in the `Authorization` header.

**Example Request:**

```js
const token = "<token>";

fetch('https://localhost:8443/api/todo', {
  method: 'POST',
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    id: 0,
    name: "string",
    isComplete: true
  })
})
.then(res => res.json())
.then(data => console.log(data))
.catch(err => console.error(err));
```

## Commit Quality

- **Commitlint** enforces [Conventional Commits](https://www.conventionalcommits.org/).
- **Husky** runs commit checks automatically.

## Copilot Integration

Advanced GitHub Copilot features are configured:

- `.github/copilot-instructions.md` – Coding conventions for generated code
- `.github/copilot/commit-prompt.md` – Commit message suggestions
- **MCP** for Copilot DB access via `mcp/postgres #query`

<details>
<summary>Copilot Configuration Example</summary>

```json
"github.copilot.chat.commitMessageGeneration.instructions": [
  {
    "file": ".github/copilot/commit-prompt.md"
  }
],
"mcp": {
  "servers": {
    "postgres": {
      "command": "docker",
      "args": [
        "run",
        "-i",
        "--rm",
        "--network",
        "dotnet-microservices-template_backend",
        "mcp/postgres",
        "postgresql://postgres:postgres@db:5432/todolist"
      ]
    }
  }
}
```
</details>

## Screenshots

![Identity Service](https://github.com/user-attachments/assets/e75aa1cc-b4b8-4c41-a7a3-085ed14fc1e0)
![TodoList Service](https://github.com/user-attachments/assets/72f90a7e-35cd-45e7-85d8-612efc688052)
![PgAdmin](https://github.com/user-attachments/assets/303c0fae-793d-440a-bf86-f4f7477343bb)
