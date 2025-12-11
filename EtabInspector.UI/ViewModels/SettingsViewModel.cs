using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EtabInspector.UI.Contracts.Services;
using System.Windows.Input;

namespace EtabInspector.UI.ViewModels;

public partial class SettingsViewModel : ToolWindowViewModel
{
    private readonly IThemeService _themeService;

    [ObservableProperty]
    private AppTheme selectedTheme;

    [ObservableProperty]
    private bool isLightTheme;

    [ObservableProperty]
    private bool isDarkTheme;

    public ICommand ApplyLightThemeCommand { get; }
    public ICommand ApplyDarkThemeCommand { get; }

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        Title = "Settings";
        ContentId = "settings";

        // Get current theme
        SelectedTheme = _themeService.GetCurrentTheme();
        UpdateThemeFlags();

        ApplyLightThemeCommand = new RelayCommand(ApplyLightTheme);
        ApplyDarkThemeCommand = new RelayCommand(ApplyDarkTheme);
    }

    private void ApplyLightTheme()
    {
        SelectedTheme = AppTheme.Light;
        _themeService.SetTheme(AppTheme.Light);
        UpdateThemeFlags();
    }

    private void ApplyDarkTheme()
    {
        SelectedTheme = AppTheme.Dark;
        _themeService.SetTheme(AppTheme.Dark);
        UpdateThemeFlags();
    }

    private void UpdateThemeFlags()
    {
        IsLightTheme = SelectedTheme == AppTheme.Light;
        IsDarkTheme = SelectedTheme == AppTheme.Dark;
    }
}