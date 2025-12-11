namespace EtabInspector.UI.Contracts.Services;

public interface IThemeService
{
    void SetTheme(AppTheme theme);
    AppTheme GetCurrentTheme();
}

public enum AppTheme
{
    Light,
    Dark,
    System
}