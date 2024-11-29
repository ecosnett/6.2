# Stage 1: Build and test AddTwoNumbers and Test projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY TestPingApp/TestPingApp.csproj TestPingApp/
COPY TestPingTest/TestPingTest.csproj TestPingTest/
COPY TestPingApp TestPingApp/
COPY TestPingTest TestPingTest/

RUN dotnet restore TestPingTest/TestPingTest.csproj
RUN dotnet build TestPingTest/TestPingTest.csproj -c Release --no-restore

# Run unit tests
RUN dotnet TestPingTest TestPingTest/TestPingTest.csproj --no-build --verbosity normal

RUN dotnet publish TestPingApp/TestPingApp.csproj -c Release -o /TestPingApp_out

# Stage 2: Build ping_test project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-PingTest
WORKDIR /

COPY PingTest/ PingTest/
RUN dotnet restore PingTest/PingTest.csproj
RUN dotnet publish PingTest/PingTest.csproj -c Release -o /PingTest_out

# Stage 3: Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /

COPY --from=build /TestPingApp_out .               
COPY --from=build-PingTest /PingTest_out /PingTest  

COPY entrypoint.sh /entrypoint.sh

RUN apt-get update && apt-get install -y curl
RUN chmod +x /entrypoint.sh

# Set environment variables and expose ports
ENV ASPNETCORE_URLS=http://0.0.0.0:8081
EXPOSE 8081

# Set the entrypoint
ENTRYPOINT ["/entrypoint.sh"]
