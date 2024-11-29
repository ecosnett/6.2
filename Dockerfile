# Stage 1: Build and test projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/
RUN dotnet restore TestPingApp/TestPingApp.csproj
RUN dotnet restore TestPingTest/TestPingTest.csproj

# Copy the rest of the source code
COPY TestPingApp TestPingApp/
COPY TestPingTest TestPingTest/

# Build and test
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore
RUN dotnet test TestPingTest/TestPingTest.csproj --no-build --verbosity normal

# Publish TestPingApp
RUN mkdir -p /out/TestPingApp && dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /out/TestPingApp

# Stage 2: Build PingTest project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-PingTest
WORKDIR /src

# Copy PingTest project
COPY PingTest/ PingTest/
RUN dotnet restore PingTest/PingTest.csproj

# Publish PingTest
RUN mkdir -p /out/PingTest && dotnet publish PingTest/PingTest.csproj -c Release -o /out/PingTest

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy outputs from build stages
COPY --from=build /out/TestPingApp ./TestPingApp
COPY --from=build-PingTest /out/PingTest ./PingTest

# Add entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN apt-get update && apt-get install -y curl && chmod +x /entrypoint.sh

# Set environment and expose port
ENV ASPNETCORE_URLS=http://0.0.0.0:8081
EXPOSE 8081

# Entrypoint
ENTRYPOINT ["/entrypoint.sh"]
