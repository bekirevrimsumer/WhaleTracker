@echo off

REM Build and start containers
docker-compose -f docker/docker-compose.yml up --build -d

REM Wait for PostgreSQL to be ready
echo Waiting for PostgreSQL to be ready...
timeout /t 10

REM Apply migrations
docker exec whaletracker-backend dotnet ef database update

echo All services are up and running!
echo Frontend: http://localhost:3000
echo Backend: http://localhost:5036
echo PostgreSQL: localhost:5432 