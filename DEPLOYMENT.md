# Xero1 Deployment Guide

## Quick Start

The Xero1 Number Base Converter is now fully built and ready for deployment!

## Available Versions

### 1. Self-Contained Version (Recommended)
- **Location**: `publish/win-x64-standalone/Xero1.exe`
- **Requirements**: None - includes .NET runtime
- **Size**: ~100MB
- **Best for**: Distribution to users who may not have .NET installed

### 2. Framework-Dependent Version
- **Location**: `publish/win-x64-framework/Xero1.exe`
- **Requirements**: .NET 9.0 Runtime must be installed
- **Size**: ~500KB
- **Best for**: Environments where .NET 9 is already installed

## Running the Application

### Option 1: Direct Execution
```bash
# Self-contained version
.\publish\win-x64-standalone\Xero1.exe

# Framework-dependent version (requires .NET 9)
.\publish\win-x64-framework\Xero1.exe
```

### Option 2: Using Batch Files
```bash
# Build the application
.\build.bat

# Run the application
.\run.bat

# Create deployment packages
.\publish.bat
```

### Option 3: Using .NET CLI
```bash
# Run in development mode
dotnet run

# Run basic tests
dotnet run -- test
```

## Features Included

✅ **Multi-Base Conversion**: Binary, Octal, Decimal, Hexadecimal
✅ **Real-time Conversion**: Updates as you type
✅ **Input Validation**: Prevents invalid entries
✅ **Prefix Support**: Optional 0b, 0, 0x prefixes
✅ **Bit Size Limits**: 8-bit, 16-bit, 32-bit, 64-bit support
✅ **Copy to Clipboard**: Export all results
✅ **Modern UI**: Clean Windows Forms interface
✅ **Error Handling**: Comprehensive validation and error messages

## System Requirements

- **Operating System**: Windows 10 or later
- **Architecture**: x64 (64-bit)
- **.NET Runtime**: 
  - Self-contained version: None required
  - Framework-dependent version: .NET 9.0 Runtime

## Installation for End Users

### Self-Contained Distribution (Recommended)
1. Copy the entire `publish/win-x64-standalone/` folder to the target machine
2. Run `Xero1.exe` directly - no installation required
3. Optionally create a desktop shortcut to `Xero1.exe`

### Framework-Dependent Distribution
1. Ensure .NET 9.0 Runtime is installed on target machine
2. Copy the `publish/win-x64-framework/` folder to the target machine
3. Run `Xero1.exe`

## Usage Instructions

1. **Enter a number** in any input field (Binary, Octal, Decimal, or Hexadecimal)
2. **Watch automatic conversion** to all other number bases
3. **Toggle prefixes** using the checkbox for standard notation
4. **Select bit size** to enforce value limits
5. **Copy results** to clipboard for use in other applications
6. **Clear all fields** to start fresh

### Supported Input Formats
- **Binary**: `1010`, `0b1010`, `b1010`
- **Octal**: `755`, `0755`
- **Decimal**: `255`
- **Hexadecimal**: `FF`, `0xFF`, `xFF`

## Troubleshooting

### Application Won't Start
- **Self-contained**: Ensure all files in the folder are present
- **Framework-dependent**: Install .NET 9.0 Runtime from Microsoft

### Invalid Number Errors
- Check that input contains only valid digits for the selected base
- Ensure the number doesn't exceed the selected bit size limit

### Copy Function Not Working
- Ensure clipboard access is available
- Try running as administrator if clipboard access is restricted

## Development Information

- **Framework**: .NET 9.0
- **UI Technology**: Windows Forms
- **Architecture**: Clean separation of UI and conversion logic
- **Build System**: .NET CLI / MSBuild

## File Structure

```
Xero1/
├── Xero1.csproj          # Project file
├── Program.cs            # Application entry point
├── MainForm.cs           # Main UI form
├── NumberConverter.cs    # Core conversion logic
├── README.md             # Project documentation
├── DEPLOYMENT.md         # This file
├── .gitignore           # Git ignore rules
├── build.bat            # Build script
├── run.bat              # Run script
├── publish.bat          # Publish script
└── publish/             # Deployment packages
    ├── win-x64-standalone/  # Self-contained version
    └── win-x64-framework/   # Framework-dependent version
```

## Version Information

- **Version**: 1.0.0
- **Build Date**: December 2024
- **Target Framework**: .NET 9.0
- **Platform**: Windows x64

---

**Xero1 Number Base Converter** - Ready for production deployment!