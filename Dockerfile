# Stage 1: Build and test projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/
RUN dotnet restore TestPingApp/TestPingApp.csproj
RUN dotnet restore TestPingTest/TestPingTest.csproj

# Debug: Check file structure
RUN ls -R /src

# Copy remaining source code
COPY TestPingApp TestPingApp/
COPY TestPingTest TestPingTest/

# Debug: Check after copying files
RUN ls -R /src

# Build TestPingTest project
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore

# Run unit tests
RUN dotnet test TestPingTest/TestPingTest.csproj --no-build --verbosity normal

# Publish TestPingApp
RUN mkdir -p /out/TestPingApp && dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /out/TestPingApp

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy outputs
COPY --from=build /out/TestPingApp ./TestPingApp

# Debug: Verify files
RUN ls -R /app

# Entrypoint
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]

