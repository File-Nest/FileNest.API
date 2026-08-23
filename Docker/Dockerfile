# 1. Build Stage: Use the latest SDK that supports .slnx files
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

# Restore and publish the Web project
RUN dotnet restore FileNest.API.slnx
RUN dotnet publish Src/FileNest.API/FileNest.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

# 2. Runtime Stage: Runs your .NET 8 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Configure port 5000
ENV ASPNETCORE_HTTP_PORTS=5000
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 5000

ENTRYPOINT ["dotnet", "FileNest.Web.dll"]
