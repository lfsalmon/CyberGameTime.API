FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 6666/udp
EXPOSE 6667/udp

# Instalar Python y venv
RUN apt-get update && \
    apt-get install -y python3-venv python3-pip && \
    rm -rf /var/lib/apt/lists/*

# Crear entorno virtual para Python
RUN python3 -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"

# Instalar tinytuya en el entorno virtual
RUN pip install --no-cache-dir --upgrade pip && \
    pip install --no-cache-dir tinytuya

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore CyberGameTime.API.sln
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/CyberGameTime.Bussiness/Scripts ./Scripts

# Usar PATH del venv de Python
ENV PATH="/opt/venv/bin:$PATH"

ENTRYPOINT ["dotnet", "CyberGameTime.API.dll"]
