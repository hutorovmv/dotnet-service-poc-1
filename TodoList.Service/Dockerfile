FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY TodoList.Service/*.csproj ./TodoList.Service/

WORKDIR /app/TodoList.Service
RUN dotnet restore

COPY ./TodoList.Service .
RUN dotnet publish TodoList.Service.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/TodoList.Service/out ./

EXPOSE 8080
EXPOSE 8443
ENTRYPOINT ["dotnet", "TodoList.Service.dll"]