@echo off
echo Building Xero1 Number Base Converter...
echo.

REM Clean previous builds
if exist "bin" rmdir /s /q "bin"
if exist "obj" rmdir /s /q "obj"

REM Build the application
dotnet build --configuration Release
if %ERRORLEVEL% neq 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo Build completed successfully!
echo.
echo To run the application:
echo   dotnet run
echo.
echo To publish for distribution:
echo   dotnet publish --configuration Release --runtime win-x64 --self-contained true
echo.
pause