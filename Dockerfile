# Stage 1: Build and test TestPingApp and TestPingTest projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/

# Restore dependencies
RUN dotnet restore TestPingTest/TestPingTest.csproj

# Copy remaining source files
COPY TestPingApp/ TestPingApp/
COPY TestPingTest/ TestPingTest/

# Build and test
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore
RUN dotnet test TestPingTest/TestPingTest.csproj --no-build --verbosity normal

# Publish TestPingApp
RUN dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /app/TestPingApp_out

# Stage 2: Build PingTest project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-PingTest
WORKDIR /app/PingTest

# Copy source files
COPY PingTest/Program.cs .

# Initialize and publish PingTest project
RUN dotnet new console --force
RUN dotnet publish -c Release -o /app/PingTest_out

# Stage 3: Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy build outputs from previous stages
COPY --from=build /app/TestPingApp_out .               
COPY --from=build-PingTest /app/PingTest_out /PingTest  

# Copy entrypoint script
COPY entrypoint.sh /entrypoint.sh

# Install additional dependencies
RUN apt-get update && apt-get install -y curl
RUN chmod +x /entrypoint.sh

# Set environment variables and expose ports
ENV ASPNETCORE_URLS=http://0.0.0.0:8081
EXPOSE 8081

# Set the entrypoint
ENTRYPOINT ["/entrypoint.sh"]
