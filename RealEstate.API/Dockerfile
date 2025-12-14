# Build stage
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

# Copy certificate
COPY ./certs/aspnetapp.pfx /root/certs/aspnetapp.pfx

# Copy all source files
COPY . .
WORKDIR "/src/RealEstate.API"

# Build
RUN dotnet build "./RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "./RealEstate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image (FIXED)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Certs
RUN mkdir -p /root/certs
COPY --from=build /root/certs/aspnetapp.pfx /root/certs/aspnetapp.pfx

# Kestrel HTTPS config
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/certs/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=shehan2002
ENV ASPNETCORE_ENVIRONMENT=Development

# Verify certificate
RUN echo "Certificate path: /root/certs/aspnetapp.pfx" && ls -la /root/certs/aspnetapp.pfx

# Run the app
ENTRYPOINT ["dotnet", "RealEstate.API.dll"]
