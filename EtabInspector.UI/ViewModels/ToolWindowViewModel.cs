using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class ToolWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Tool Window";

    [ObservableProperty]
    private string contentId = Guid.NewGuid().ToString();

    [ObservableProperty]
    private bool isVisible = true;

    [ObservableProperty]
    private bool canClose = true;

    [ObservableProperty]
    private bool canHide = true;
}