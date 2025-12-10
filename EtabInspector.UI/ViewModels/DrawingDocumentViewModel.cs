using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class DrawingDocumentViewModel : DocumentViewModel
{
    [ObservableProperty]
    private string drawingContent = "2D Drawing View - This is where your structural drawings will be displayed";

    [ObservableProperty]
    private double scale = 1.0;

    public DrawingDocumentViewModel()
    {
        Title = "Drawing";
    }

    public override void OnClose()
    {
        // Cleanup drawing resources
        base.OnClose();
    }
}
