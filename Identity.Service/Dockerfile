# === Stage 1 ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# COPY everything needed from root context
COPY Identity.Service/*.csproj ./Identity.Service/
COPY Identity.Service/nuget.config ./Identity.Service/
COPY nupkgs ./nupkgs

# Restore using custom config (which points to /app/nupkgs)
WORKDIR /app/Identity.Service
RUN dotnet restore --configfile nuget.config

# Copy everything else inside TodoList.Service and publish
COPY ./Identity.Service .
RUN dotnet publish Identity.Service.csproj -c Release -o out

# === Stage 2 ===
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/Identity.Service/out ./

COPY certs/https/development.pfx /https/development.pfx

EXPOSE 8080
EXPOSE 8443
ENTRYPOINT ["dotnet", "Identity.Service.dll"]