using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace MultiRun
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public List<string> Apps { get; private set; }

        public SettingsWindow(List<string> apps)
        {
            Apps = new List<string>(apps);
            InitializeComponent();
            UpdateAppList();
        }

        private void UpdateAppList()
        {
            listViewApps.Items.Clear();
            foreach (var app in Apps)
            {
                listViewApps.Items.Add(app);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("What would you like to add?\n\nYes = Add Application\nNo = Add URL",
                                       "Add Item", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*",
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    if (!Apps.Contains(filePath))
                    {
                        Apps.Add(filePath);
                        UpdateAppList();
                    }
                }
            }
            else if (result == MessageBoxResult.No)
            {
                var inputDialog = new InputDialog("Enter URL (e.g., https://www.google.com):");
                if (inputDialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(inputDialog.InputText))
                {
                    string url = inputDialog.InputText.Trim();
                    if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                        url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!Apps.Contains(url))
                        {
                            Apps.Add(url);
                            UpdateAppList();
                        }
                        else
                        {
                            MessageBox.Show("This URL is already in the list.", "Duplicate URL", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid URL starting with http:// or https://", "Invalid URL", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var listViewItem = FindParent<ListViewItem>(button);
                if (listViewItem != null)
                {
                    string path = listViewItem.Content as string;
                    if (path != null)
                    {
                        Apps.Remove(path);
                        UpdateAppList();
                    }
                }
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                DragMove();
            }
        }

        private void btnCloseSettings_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}