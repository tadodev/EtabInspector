using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace EtabInspector.UI.ViewModels;

public partial class PropertiesViewModel : ToolWindowViewModel
{
    [ObservableProperty]
    private ObservableCollection<PropertyItem> properties = new();

    public PropertiesViewModel()
    {
        Title = "Properties";
        ContentId = "properties";

        // Sample properties
        Properties.Add(new PropertyItem { Name = "Name", Value = "Beam-1" });
        Properties.Add(new PropertyItem { Name = "Material", Value = "Concrete" });
        Properties.Add(new PropertyItem { Name = "Section", Value = "W21x44" });
        Properties.Add(new PropertyItem { Name = "Length", Value = "5.0 m" });
    }
}

public partial class PropertyItem : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string value = string.Empty;
}