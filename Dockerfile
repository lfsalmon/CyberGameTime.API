FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Install Python and pip
RUN apt-get update && apt-get install -y \
    python3 \
    python3-pip \
    && rm -rf /var/lib/apt/lists/*

# Create symbolic link for python command
RUN ln -s /usr/bin/python3 /usr/bin/python

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files for dependency restoration
COPY *.sln ./
COPY CyberGameTime.API/*.csproj ./CyberGameTime.API/
COPY CyberGameTime.Bussiness/*.csproj ./CyberGameTime.Bussiness/
COPY CyberGaneTime.Application/*.csproj ./CyberGaneTime.Application/
COPY CyberGameTime/*.csproj ./CyberGameTime/

# Restore dependencies
RUN dotnet restore CyberGameTime.API.sln

# Copy everything else
COPY . .

# Publish the API
WORKDIR /src/CyberGameTime.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

# Copy published application
COPY --from=build /app/publish .

# Copy Python scripts
COPY --from=build /src/CyberGameTime.Bussiness/Scripts ./Scripts

# Install Python dependencies
COPY CyberGameTime.Bussiness/Scripts/tuyaLan/requirements.txt ./Scripts/tuyaLan/
RUN pip3 install -r ./Scripts/tuyaLan/requirements.txt

ENTRYPOINT ["dotnet", "CyberGameTime.API.dll"]