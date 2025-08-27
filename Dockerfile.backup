FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
# Restaurar dependencias
RUN dotnet restore CyberGameTime.API.sln

# Copiar todo el código
COPY . .

# Publicar la API
WORKDIR /src/CyberGameTime.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/CyberGameTime.Bussiness/Scripts ./Scripts
ENTRYPOINT ["dotnet", "CyberGameTime.API.dll"]
