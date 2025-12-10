using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class ModelDocumentViewModel : DocumentViewModel
{
    [ObservableProperty]
    private string modelData = "3D Model View - This is where your structural model will be displayed";

    [ObservableProperty]
    private int nodeCount = 0;

    [ObservableProperty]
    private int elementCount = 0;

    public ModelDocumentViewModel()
    {
        Title = "Model";
    }
}
