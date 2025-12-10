namespace EtabInspector.UI.Contracts.Views;

public interface IShellWindow
{
    void ShowWindow();
    void ClosedWindow();
    object GetDockingManager();
}
