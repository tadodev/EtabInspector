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
using EtabInspector.UI.Contracts.Views;
using EtabInspector.UI.ViewModels;
using iNKORE.UI.WPF.Modern.Controls;

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
        /// Applies the AvalonDock theme to match the current iNKORE theme
        /// </summary>
        private void ApplyAvalonDockTheme()
        {
            try
            {
                // Get current theme from iNKORE ThemeManager
                var appTheme = iNKORE.UI.WPF.Modern.ThemeManager.Current.ActualApplicationTheme;

                var themeAssembly = System.Reflection.Assembly.Load("Dirkster.AvalonDock.Themes.VS2013");
                string themeTypeName;

                // Match AvalonDock theme to iNKORE theme
                if (appTheme == iNKORE.UI.WPF.Modern.ApplicationTheme.Dark)
                {
                    themeTypeName = "Dirkster.AvalonDock.Themes.VS2013.Vs2013DarkTheme";
                }
                else
                {
                    themeTypeName = "Dirkster.AvalonDock.Themes.VS2013.Vs2013LightTheme";
                }

                var themeType = themeAssembly.GetType(themeTypeName);
                if (themeType != null)
                {
                    var themeInstance = Activator.CreateInstance(themeType);
                    if (themeInstance != null)
                    {
                        dockManager.Theme = themeInstance as AvalonDock.Themes.Theme;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load AvalonDock theme: {ex.Message}");
                // Continue without theme - AvalonDock will use default theme
            }
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
