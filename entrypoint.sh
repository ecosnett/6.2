#!/bin/bash

echo "|Starting the web application...|"
dotnet /app/TestPingApp.dll > /dev/null 2>&1 &

WEB_APP_PID=$!

sleep 5
echo "|Compeleted the web application |"

echo "Running ping test..."
dotnet /PingTest/PingTest.dll  
if [ $? -ne 0 ]; then
    echo "Ping test failed. Exiting."
    kill $WEB_APP_PID
    exit 1
fi
echo "Completed ping test"

echo "Web application is running at http://localhost:8081"

wait $WEB_APP_PID
