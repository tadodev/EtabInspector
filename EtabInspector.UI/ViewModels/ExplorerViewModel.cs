using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace EtabInspector.UI.ViewModels;

public partial class ExplorerViewModel : ToolWindowViewModel
{
    [ObservableProperty]
    private ObservableCollection<TreeItemViewModel> items = new();

    public ExplorerViewModel()
    {
        Title = "Explorer";
        ContentId = "explorer";

        // Sample data
        Items.Add(new TreeItemViewModel { Name = "📁 Project", IsExpanded = true });
        Items[0].Children.Add(new TreeItemViewModel { Name = "📄 Model.etabs" });
        Items[0].Children.Add(new TreeItemViewModel { Name = "📄 Drawing.dwg" });
        Items[0].Children.Add(new TreeItemViewModel { Name = "📁 Results", IsExpanded = false });
    }
}

public partial class TreeItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private bool isExpanded;

    [ObservableProperty]
    private ObservableCollection<TreeItemViewModel> children = new();

}