# ------------------------------
# Stage 1: Base runtime
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ------------------------------
# Stage 2: Build
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files for all projects
COPY RealEstate.API/RealEstate.API.csproj RealEstate.API/
COPY RealEstate.Core/RealEstate.Core.csproj RealEstate.Core/
COPY RealEstate.Infrastructure/RealEstate.Infrastructure.csproj RealEstate.Infrastructure/
COPY RealEstate.Shared/RealEstate.Shared.csproj RealEstate.Shared/

# Restore dependencies for API project
RUN dotnet restore RealEstate.API/RealEstate.API.csproj

# Copy all source files
COPY . .

# Build API project
RUN dotnet build RealEstate.API/RealEstate.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# ------------------------------
# Stage 3: Publish
# ------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish RealEstate.API/RealEstate.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ------------------------------
# Stage 4: Final image
# ------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstate.API.dll"]
