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

# === Only needed for debug in vs code
# RUN apt-get update \
#   && apt-get install -y unzip curl \
#   && mkdir -p /vsdbg \
#   && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
# COPY .vsdbg /vsdbg
# RUN chmod +x /vsdbg/vsdbg

COPY Identity.Service/certs/https/development.pfx /https/development.pfx

EXPOSE 8080
EXPOSE 8443
ENTRYPOINT ["dotnet", "Identity.Service.dll"]