#!/bin/bash

# Build and start containers
docker-compose -f docker/docker-compose.yml up --build -d

# Wait for PostgreSQL to be ready
echo "Waiting for PostgreSQL to be ready..."
sleep 10

# Apply migrations
docker exec whaletracker-backend dotnet ef database update

echo "All services are up and running!"
echo "Frontend: http://localhost:3000"
echo "Backend: http://localhost:5036"
echo "PostgreSQL: localhost:5432" 