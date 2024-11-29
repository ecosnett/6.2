# Stage 1: Build and test AddTwoNumbers and Test projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files for TestPingApp and TestPingTest
COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/

# Restore dependencies for both projects
WORKDIR /src/TestPingApp
RUN dotnet restore ../TestPingApp/TestPingApp.csproj

WORKDIR /src/TestPingTest
RUN dotnet restore ../TestPingTest/TestPingTest.csproj

# Copy the rest of the application source code
WORKDIR /src
COPY TestPingApp TestPingApp/
COPY TestPingTest TestPingTest/

# Build TestPingTest project
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore

# Run unit tests for TestPingTest
RUN dotnet test TestPingTest/TestPingTest.csproj --no-build --verbosity normal

# Publish TestPingApp project
RUN dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /out/TestPingApp

# Stage 2: Build PingTest project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-PingTest
WORKDIR /src

# Copy PingTest project and dependencies
COPY PingTest/ PingTest/
WORKDIR /src/PingTest
RUN dotnet restore PingTest.csproj

# Publish PingTest project
RUN dotnet publish PingTest.csproj -c Release -o /out/PingTest

# Stage 3: Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy output from previous stages
COPY --from=build /out/TestPingApp ./TestPingApp
COPY --from=build-PingTest /out/PingTest ./PingTest

# Copy the entrypoint script
COPY entrypoint.sh /entrypoint.sh

# Install necessary packages and set permissions
RUN apt-get update && apt-get install -y curl && chmod +x /entrypoint.sh

# Set environment variables and expose ports
ENV ASPNETCORE_URLS=http://0.0.0.0:8081
EXPOSE 8081

# Set the entrypoint
ENTRYPOINT ["/entrypoint.sh"]

