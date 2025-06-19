FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY Identity.Service/*.csproj ./Identity.Service/

WORKDIR /app/Identity.Service
RUN dotnet restore

COPY ./Identity.Service .
RUN dotnet publish Identity.Service.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/Identity.Service/out ./

EXPOSE 8080
EXPOSE 8443

ENTRYPOINT ["dotnet", "Identity.Service.dll"]