using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace EtabInspector.UI.ViewModels;

public partial class OutputViewModel : ToolWindowViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> logs = new();

    public OutputViewModel()
    {
        Title = "Output";
        ContentId = "output";

        // Sample logs
        Logs.Add($"[{DateTime.Now:HH:mm:ss}] Application started");
        Logs.Add($"[{DateTime.Now:HH:mm:ss}] Initializing components...");
        Logs.Add($"[{DateTime.Now:HH:mm:ss}] Ready");
    }

    public void AddLog(string message)
    {
        Logs.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}