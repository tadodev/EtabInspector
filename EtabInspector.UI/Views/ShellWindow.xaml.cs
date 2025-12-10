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

            // Apply AvalonDock Dark Theme
            ApplyAvalonDockTheme();
        }

        private void ApplyAvalonDockTheme()
        {
            try
            {
                var themeAssembly = System.Reflection.Assembly.Load("Dirkster.AvalonDock.Themes.VS2013");
                var themeType = themeAssembly.GetType("Dirkster.AvalonDock.Themes.VS2013.Vs2013DarkTheme");
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
                System.Diagnostics.Debug.WriteLine($"Failed to load VS2013 Dark theme: {ex.Message}");
            }
        }

        public void ShowWindow() => Show();

        public void ClosedWindow() => Close();

        public object GetDockingManager() => dockManager;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _viewModel?.Shutdown();
        }
    }
}
