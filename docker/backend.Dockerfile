FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Enable Windows targeting
ENV EnableWindowsTargeting=true

# Copy solution and project files
COPY ["WhaleTracker.sln", "./"]
COPY ["src/WhaleTracker.API/WhaleTracker.API.csproj", "src/WhaleTracker.API/"]
COPY ["src/WhaleTracker.Core/WhaleTracker.Core.csproj", "src/WhaleTracker.Core/"]
COPY ["src/WhaleTracker.Infrastructure/WhaleTracker.Infrastructure.csproj", "src/WhaleTracker.Infrastructure/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish "src/WhaleTracker.API/WhaleTracker.API.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "WhaleTracker.API.dll"] 