FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore FileNest.API.slnx
RUN dotnet build FileNest.API.slnx --no-restore -c Release