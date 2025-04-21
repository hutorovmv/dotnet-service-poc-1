FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# === Only for debug in vs code
RUN apt-get update \
  && apt-get install -y unzip curl \
  && mkdir -p /vsdbg \
  && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

COPY .vsdbg /vsdbg
RUN chmod +x /vsdbg/vsdbg
# === Only for debug in vs code

COPY certs/https/development.pfx /https/development.pfx

EXPOSE 8081
ENTRYPOINT ["dotnet", "Identity.Service.dll"]