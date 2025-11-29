# MultiRun - Multi-App & URL Launcher

A sleek and modern application launcher for Windows that allows you to organize and launch multiple applications and URLs with a single click. Built with WPF and .NET 8, featuring a professional dark theme and customizable interface.

## Features

- **Modern Dark UI**: Professional dark-themed interface with custom window chrome and smooth animations
- **App & URL Management**: Easily add, edit, and organize applications and web URLs
- **Batch Launching**: Launch all configured apps and URLs simultaneously
- **Custom Scrollbars**: Fully themed dark scrollbars for a consistent look
- **Portable Executable**: Self-contained single-file executable with no dependencies
- **Persistent Settings**: Saves configuration to user AppData for portability

## Screenshots

<img width="990" height="904" alt="image" src="https://github.com/user-attachments/assets/ca58567c-0800-44a9-9c6a-142d6aab9637" />

## Installation

### Option 1: Pre-built Executable
1. Download the latest `MultiRun.exe` from the releases page
2. Place the executable anywhere on your system
3. Run `MultiRun.exe` to start the application

### Option 2: Build from Source
1. Ensure you have .NET 8 SDK installed
2. Clone this repository
3. Navigate to the project directory
4. Run the following commands:

```bash
# Restore dependencies
dotnet restore

# Build in debug mode
dotnet build

# Run the application
dotnet run --project MultiRun

# Or publish as self-contained executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

The published executable will be in `MultiRun\bin\Release\net8.0-windows\win-x64\publish\`

## Usage

1. **Launch the Application**: Run `MultiRun.exe`
2. **Add Apps/URLs**: Click "⚙️ Manage Apps & URLs" to open the settings window
3. **Configure Items**: Add executable paths or URLs using the input dialog
4. **Launch All**: Click "Launch All Apps & URLs" to start everything simultaneously
5. **Settings Persistence**: Your configuration is automatically saved and restored

## System Requirements

- Windows 10 or later
- .NET 8.0 Runtime (only for source builds; executable is self-contained)
- 100 MB free disk space

## Configuration

Settings are stored in:
```
%APPDATA%\MultiAppLauncher\config.json
```

You can manually edit this file or use the in-app settings manager.

## Building & Development

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code with C# extension

### Project Structure
```
MultiRun/
├── App.xaml                 # Application entry point
├── MainWindow.xaml          # Main launcher window
├── SettingsWindow.xaml      # Settings management window
├── InputDialog.xaml         # Input dialog for adding items
└── Resources/               # Icons and assets
```

### Key Technologies
- **WPF**: Windows Presentation Foundation for UI
- **C# 12**: Modern C# with nullable reference types
- **Newtonsoft.Json**: JSON serialization for configuration
- **Custom Styling**: Extensive XAML styling for dark theme

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Changelog

### v1.0.0
- Initial release
- Dark theme implementation
- App and URL launching
- Settings management
- Self-contained executable support

## Support

If you encounter any issues or have suggestions:
- Open an issue on GitHub
- Check the configuration file for errors
- Ensure all paths and URLs are valid

## Acknowledgments

- Built with .NET 8 and WPF
- Icons from [source]
- Inspired by modern launcher applications
