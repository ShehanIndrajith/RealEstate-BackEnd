# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# HTTP port
EXPOSE 8080

# HTTPS port
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["RealEstate.API/RealEstate.API.csproj", "RealEstate.API/"]
COPY ["RealEstate.Core/RealEstate.Core.csproj", "RealEstate.Core/"]
COPY ["RealEstate.Shared/RealEstate.Shared.csproj", "RealEstate.Shared/"]
COPY ["RealEstate.Infrastructure/RealEstate.Infrastructure.csproj", "RealEstate.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "./RealEstate.API/RealEstate.API.csproj"

# Copy certificate into container
COPY ./certs/aspnetapp.pfx /root/certs/aspnetapp.pfx

# Copy all source files
COPY . .

WORKDIR "/src/RealEstate.API"

# Build the project
RUN dotnet build "./RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "./RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables for Kestrel HTTPS
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/certs/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=shehan2002
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "RealEstate.API.dll"]
