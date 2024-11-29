# Stage 1: Build and test AddTwoNumbers and Test projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files for TestPingApp and TestPingTest
COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/

# Restore dependencies
RUN dotnet restore TestPingApp/TestPingApp.csproj
RUN dotnet restore TestPingTest/TestPingTest.csproj

# Copy the rest of the source code
COPY TestPingApp TestPingApp/
COPY TestPingTest TestPingTest/

# Build and test TestPingTest
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore
RUN dotnet test TestPingTest/TestPingTest.csproj --no-build --verbosity normal

# Publish TestPingApp
RUN dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /out/TestPingApp

# Stage 2: Build PingTest project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-PingTest
WORKDIR /src

# Copy PingTest project
COPY PingTest/ PingTest/

# Restore and publish PingTest
RUN dotnet restore PingTest/PingTest.csproj
RUN dotnet publish PingTest/PingTest.csproj -c Release -o /out/PingTest

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy output from previous stages
COPY --from=build /out/TestPingApp ./TestPingApp
COPY --from=build-PingTest /out/PingTest ./PingTest

# Copy entrypoint script
COPY entrypoint.sh /entrypoint.sh

# Install curl and set permissions
RUN apt-get update && apt-get install -y curl && chmod +x /entrypoint.sh

# Set environment and expose port
ENV ASPNETCORE_URLS=http://0.0.0.0:8081
EXPOSE 8081

# Set entrypoint
ENTRYPOINT ["/entrypoint.sh"]


