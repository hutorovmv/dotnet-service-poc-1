New-Item -ItemType Directory -Force -Path ./certs/https
dotnet dev-certs https --clean
dotnet dev-certs https -ep ./certs/https/development.pfx -p 1111
dotnet dev-certs https --trust