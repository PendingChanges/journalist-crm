FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY back/Journalist.Crm.sln .
COPY **/*.csproj ./aspnetapp/
RUN dotnet restore

# copy everything else and build app
COPY aspnetapp/. ./aspnetapp/
WORKDIR /source/aspnetapp
RUN dotnet publish -c release -o /app --no-restore