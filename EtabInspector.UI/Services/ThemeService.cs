using EtabInspector.UI.Contracts.Services;
using iNKORE.UI.WPF.Modern;
using System.Windows;

namespace EtabInspector.UI.Services;

public class ThemeService : IThemeService
{
    public void SetTheme(AppTheme theme)
    {
        ApplicationTheme? inkoreTheme = theme switch
        {
            AppTheme.Light => ApplicationTheme.Light,
            AppTheme.Dark => ApplicationTheme.Dark,
            _ => (ApplicationTheme?)null // System default
        };

        ThemeManager.Current.ApplicationTheme = inkoreTheme;

        // Update all open windows
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Views.ShellWindow shellWindow)
                {
                    shellWindow.ApplyAvalonDockTheme();
                }
            }
        }));
    }

    public AppTheme GetCurrentTheme()
    {
        var currentTheme = ThemeManager.Current.ActualApplicationTheme;

        if (currentTheme == null)
        {
            // Return system theme
            return AppTheme.Light;
        }

        return currentTheme == ApplicationTheme.Dark ? AppTheme.Dark : AppTheme.Light;
    }
}