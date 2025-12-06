# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY *.sln ./
COPY PullPilot.API/*.csproj ./PullPilot.API/
COPY PullPilot.Domain/*.csproj ./PullPilot.Domain/
COPY PullPilot.Services/*.csproj ./PullPilot.Services/
COPY PullPilot.Interface/*.csproj ./PullPilot.Interface/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/PullPilot.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "PullPilot.API.dll"]