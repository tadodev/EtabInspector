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

namespace EtabInspector.UI.ViewModels
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
            
            // Apply VS2013 Light Theme via reflection
            ApplyVS2013Theme();
        }

        private void ApplyVS2013Theme()
        {
            try
            {
                // Load the VS2013 theme assembly dynamically
                var themeAssembly = System.Reflection.Assembly.Load("Dirkster.AvalonDock.Themes.VS2013");
                var themeType = themeAssembly.GetType("Dirkster.AvalonDock.Themes.VS2013.VS2013LightTheme");
                if (themeType != null)
                {
                    var themeInstance = Activator.CreateInstance(themeType);
                    dockManager.Theme = themeInstance as dynamic;
                }
            }
            catch (Exception ex)
            {
                // If theme loading fails, continue without it
                System.Diagnostics.Debug.WriteLine($"Failed to load VS2013 theme: {ex.Message}");
            }
        }

        public void ShowWindow() => Show();

        public void ClosedWindow() => Close();

        public object GetDockingManager() => dockManager;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            // Call shutdown on the view model to properly cleanup subscriptions
            _viewModel?.Shutdown();
        }
    }
}
