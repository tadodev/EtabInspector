using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class DocumentViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Untitled";

    [ObservableProperty]
    private string contentId = Guid.NewGuid().ToString();

    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty]
    private bool isActive;

    [ObservableProperty]
    private bool canClose = true;

    public virtual void OnClose()
    {
        // Override in derived classes for cleanup
    }
}
