using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace MultiRun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> apps;

        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
            UpdateAppList();
        }

        private void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            foreach (var path in apps)
            {
                try
                {
                    if (IsUrl(path))
                    {
                        OpenUrlInChrome(path);
                    }
                    else
                    {
                        Process.Start(path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to launch {path}: {ex.Message}");
                }
            }
        }

        private bool IsUrl(string path)
        {
            return path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                   path.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }

        private void OpenUrlInChrome(string url)
        {
            try
            {
                string chromePath = GetChromePath();
                if (!string.IsNullOrEmpty(chromePath))
                {
                    Process.Start(chromePath, $"\"{url}\"");
                }
                else
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open URL in Chrome: {ex.Message}\n\nTrying default browser...", "Chrome Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Failed to open URL in any browser.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string GetChromePath()
        {
            string[] possiblePaths = {
                @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                @"C:\Users\" + Environment.UserName + @"\AppData\Local\Google\Chrome\Application\chrome.exe"
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return null;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(apps);
            if (settingsWindow.ShowDialog() == true)
            {
                apps = settingsWindow.Apps;
                SaveConfig();
                UpdateAppList();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateAppList()
        {
            listApps.Items.Clear();
            foreach (var app in apps)
            {
                listApps.Items.Add(app);
            }
        }

        private void LoadConfig()
        {
            string configPath = GetConfigPath();
            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    var config = JsonConvert.DeserializeObject<Config>(json);
                    apps = config?.Apps ?? new List<string>();
                }
                catch
                {
                    apps = new List<string>();
                }
            }
            else
            {
                apps = new List<string>();
                SaveConfig();
            }
        }

        private void SaveConfig()
        {
            string configPath = GetConfigPath();
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));
            var config = new Config { Apps = apps };
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        private string GetConfigPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "MultiAppLauncher", "config.json");
        }
    }

    public class Config
    {
        public List<string> Apps { get; set; }
    }
}