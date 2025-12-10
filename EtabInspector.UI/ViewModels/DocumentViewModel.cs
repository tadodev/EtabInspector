using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class DocumentViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Untitled";

    [ObservableProperty]
    private string _contentId = Guid.NewGuid().ToString();

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private bool _isActive;

    [ObservableProperty]
    private bool _canClose = true;

    public virtual void OnClose()
    {
        // Override in derived classes for cleanup
    }
}
