# Base con .NET y Python
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Instalar Python y pip
RUN apt-get update && \
    apt-get install -y python3 python3-pip && \
    pip3 install --no-cache-dir tinytuya && \
    rm -rf /var/lib/apt/lists/*

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage final
FROM base AS final
WORKDIR /app

# Copiar scripts Python
COPY --from=build /src/CyberGameTime.Bussiness/Scripts ./Scripts

# Copiar app .NET
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CyberGameTime.API.dll"]
