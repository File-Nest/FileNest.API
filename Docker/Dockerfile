# Build the application using the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files into the container
COPY . .

# Restore project dependencies
RUN dotnet restore FileNest.API.slnx

# Publish the Web project in Release mode
RUN dotnet publish Src/FileNest.API/FileNest.Web.csproj -c Release -o /app/publish /p:UseAppHost=false


# Use the ASP.NET Core runtime to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install curl for the Docker health check
RUN apt-get update \
    && apt-get install -y curl \
    && rm -rf /var/lib/apt/lists/*

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Set the application port
ENV ASPNETCORE_HTTP_PORTS=5000

# Set the application environment
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port 5000
EXPOSE 5000

# Check the application health every 30 seconds
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl --fail http://localhost:5000/health || exit 1

# Start the application
ENTRYPOINT ["dotnet", "FileNest.Web.dll"]