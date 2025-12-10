using CommunityToolkit.Mvvm.ComponentModel;

namespace EtabInspector.UI.ViewModels;

public partial class ModelDocumentViewModel : DocumentViewModel
{
    [ObservableProperty]
    private string _modelData = "3D Model View - This is where your structural model will be displayed";

    [ObservableProperty]
    private int _nodeCount = 0;

    [ObservableProperty]
    private int _elementCount = 0;

    public ModelDocumentViewModel()
    {
        Title = "Model";
    }
}
