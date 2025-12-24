@echo off
echo Publishing Xero1 for distribution...
echo.

REM Create publish directory
if not exist "publish" mkdir "publish"

REM Publish self-contained executable
echo Creating self-contained executable...
dotnet publish --configuration Release --runtime win-x64 --self-contained true --output "publish\win-x64-standalone"

if %ERRORLEVEL% neq 0 (
    echo Publish failed!
    pause
    exit /b 1
)

REM Publish framework-dependent executable
echo Creating framework-dependent executable...
dotnet publish --configuration Release --runtime win-x64 --self-contained false --output "publish\win-x64-framework"

if %ERRORLEVEL% neq 0 (
    echo Publish failed!
    pause
    exit /b 1
)

echo.
echo Publish completed successfully!
echo.
echo Self-contained version (no .NET required): publish\win-x64-standalone\Xero1.exe
echo Framework-dependent version (.NET 9 required): publish\win-x64-framework\Xero1.exe
echo.
pause