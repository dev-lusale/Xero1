# Xero1 - Number Base Converter

A powerful and user-friendly Windows application for converting numbers between different base systems (Binary, Octal, Decimal, and Hexadecimal).
![image alt](https://github.com/dev-lusale/Xero1/blob/main/xero%20one.png?raw=true)

## Features

- **Multi-Base Conversion**: Convert between Binary (Base 2), Octal (Base 8), Decimal (Base 10), and Hexadecimal (Base 16)
- **Real-time Conversion**: Automatic conversion as you type in any field
- **Prefix Support**: Optional display of standard prefixes (0b for binary, 0 for octal, 0x for hexadecimal)
- **Bit Size Limits**: Support for 8-bit, 16-bit, 32-bit, and 64-bit number ranges
- **Input Validation**: Real-time validation with helpful error messages
- **Copy to Clipboard**: Easy copying of all conversion results
- **Clean Interface**: Modern, intuitive Windows Forms interface

## System Requirements

- Windows 10 or later
- .NET 9.0 Runtime

## Installation

1. Clone or download this repository
2. Open a command prompt in the project directory
3. Run: `dotnet build --configuration Release`
4. Run: `dotnet run` or execute the built executable

## Usage

1. **Enter a number** in any of the four input fields (Binary, Octal, Decimal, or Hexadecimal)
2. **Watch automatic conversion** to all other bases in real-time
3. **Toggle prefixes** using the checkbox to show/hide standard number prefixes
4. **Select bit size** to enforce appropriate value limits
5. **Copy results** to clipboard using the "Copy Results" button
6. **Clear all fields** using the "Clear All" button

### Input Formats

- **Binary**: Enter digits 0-1 (e.g., `1010`, `0b1010`, `b1010`)
- **Octal**: Enter digits 0-7 (e.g., `755`, `0755`)
- **Decimal**: Enter digits 0-9 (e.g., `255`)
- **Hexadecimal**: Enter digits 0-9, A-F (e.g., `FF`, `0xFF`, `xFF`)

### Bit Size Limits

- **8-bit**: 0 to 255
- **16-bit**: 0 to 65,535
- **32-bit**: 0 to 4,294,967,295
- **64-bit**: 0 to 9,223,372,036,854,775,807

## Building from Source

```bash
# Clone the repository
git clone <repository-url>
cd Xero1

# Build the application
dotnet build --configuration Release

# Run the application
dotnet run
```

## Publishing for Distribution

```bash
# Create a self-contained executable
dotnet publish --configuration Release --runtime win-x64 --self-contained true

# Create a framework-dependent executable (requires .NET 9 installed)
dotnet publish --configuration Release --runtime win-x64 --self-contained false
```

## Architecture

- **NumberConverter.cs**: Core conversion logic and validation
- **MainForm.cs**: Windows Forms UI and event handling
- **Program.cs**: Application entry point

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is open source. Feel free to use, modify, and distribute.

## Version History

- **v1.0.0**: Initial release with full conversion functionality
