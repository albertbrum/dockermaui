:: Execute this batchfile to prepare and run the container
@echo off

echo Cleaning up old container...
docker stop app-maui >nul 2>&1
docker rm app-maui >nul 2>&1

echo Starting docker...

docker build -t app-maui-web .
docker run -d -p 8080:8080 --name app-maui app-maui-web


if %ERRORLEVEL% neq 0 (
    echo Command failed with error code %ERRORLEVEL%.
    pause
    exit /b 0
)

echo Opening browser instance...

start http://localhost:8080
pause