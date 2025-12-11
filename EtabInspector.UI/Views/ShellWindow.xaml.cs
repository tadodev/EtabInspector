using EtabInspector.UI.Contracts.Views;
using EtabInspector.UI.ViewModels;
using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EtabInspector.UI.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window, IShellWindow
    {
        private ShellViewModel? _viewModel;

        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;

            // Apply matching AvalonDock theme based on app theme
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            ApplyAvalonDockTheme();
        }

        /// <summary>
        /// Sync AvalonDock VS2013 brushes to iNKORE app theme
        /// </summary>
        public void ApplyAvalonDockTheme()
        {
            var useDark = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;
            SetVs2013DockBrushes(useDark);
        }

        /// <summary>
        /// Sets the VS2013 AvalonDock theme brushes based on light/dark mode
        /// </summary>
        private static void SetVs2013DockBrushes(bool useDark)
        {
            // Try different possible resource locations
            var possibleUris = useDark
                ? new[]
                {
                    "pack://application:,,,/Dirkster.AvalonDock.Themes.VS2013;component/DarkBrushs.xaml",
                    "pack://application:,,,/Dirkster.AvalonDock.Themes.VS2013;component/Themes/DarkBrushs.xaml",
                    "pack://application:,,,/AvalonDock.Themes.VS2013;component/DarkBrushs.xaml",
                    "pack://application:,,,/AvalonDock.Themes.VS2013;component/Themes/DarkBrushs.xaml"
                }
                : new[]
                {
                    "pack://application:,,,/Dirkster.AvalonDock.Themes.VS2013;component/LightBrushs.xaml",
                    "pack://application:,,,/Dirkster.AvalonDock.Themes.VS2013;component/Themes/LightBrushs.xaml",
                    "pack://application:,,,/AvalonDock.Themes.VS2013;component/LightBrushs.xaml",
                    "pack://application:,,,/AvalonDock.Themes.VS2013;component/Themes/LightBrushs.xaml"
                };

            // Remove any existing VS2013 brush dictionaries
            var md = Application.Current.Resources.MergedDictionaries;
            var existing = md.Where(d => d.Source != null &&
                                         (d.Source.OriginalString.Contains("AvalonDock.Themes.VS2013") ||
                                          d.Source.OriginalString.Contains("Dirkster.AvalonDock.Themes.VS2013")))
                             .ToList();

            foreach (var d in existing)
                md.Remove(d);

            // Try to load the theme with fallback options
            foreach (var uriString in possibleUris)
            {
                try
                {
                    var uri = new Uri(uriString, UriKind.Absolute);
                    var dictionary = new ResourceDictionary { Source = uri };
                    md.Add(dictionary);
                    System.Diagnostics.Debug.WriteLine($"Successfully loaded theme: {uriString}");
                    return;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load theme {uriString}: {ex.Message}");
                }
            }

            System.Diagnostics.Debug.WriteLine("Warning: Could not load any AvalonDock theme resource");
        }

        public void ShowWindow() => Show();

        public void ClosedWindow() => Close();

        public object GetDockingManager() => dockManager;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Cleanup: Unsubscribe from events and dispose resources
            _viewModel?.Shutdown();

            Loaded -= OnWindowLoaded;
        }
    }
}
