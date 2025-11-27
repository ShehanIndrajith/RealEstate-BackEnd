# ------------------------------
# Stage 1: Base runtime image
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ------------------------------
# Stage 2: Build the application
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["RealEstate.API/RealEstate.API.csproj", "RealEstate.API/"]
RUN dotnet restore "RealEstate.API/RealEstate.API.csproj"

# Copy all source files
COPY . .
WORKDIR "/src/RealEstate.API"

# Build the project
RUN dotnet build "RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ------------------------------
# Stage 3: Publish the application
# ------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ------------------------------
# Stage 4: Final image
# ------------------------------
FROM base AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=publish /app/publish .

# Use default user (do NOT use $APP_UID, it breaks Linux container)
# ENTRYPOINT
ENTRYPOINT ["dotnet", "RealEstate.API.dll"]
