# === Stage 1 ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# COPY everything needed from root context
COPY TodoList.Service/*.csproj ./TodoList.Service/
COPY TodoList.Service/nuget.config ./TodoList.Service/
COPY packages ./packages

# Restore using custom config (which points to /app/packages)
WORKDIR /app/TodoList.Service
RUN dotnet restore --configfile nuget.config

# Copy everything else inside TodoList.Service and publish
COPY ./TodoList.Service .
RUN dotnet publish TodoList.Service.csproj -c Release -o out

# === Stage 2 ===
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/TodoList.Service/out ./

COPY certs/https/development.pfx /https/development.pfx

EXPOSE 8080
EXPOSE 8443
ENTRYPOINT ["dotnet", "TodoList.Service.dll"]