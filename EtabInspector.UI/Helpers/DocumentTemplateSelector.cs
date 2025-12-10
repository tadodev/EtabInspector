using EtabInspector.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EtabInspector.UI.Helpers;

public class DocumentTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ModelDocumentTemplate { get; set; }
    public DataTemplate? DrawingDocumentTemplate { get; set; }
    public DataTemplate? DefaultTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            ModelDocumentViewModel => ModelDocumentTemplate,
            DrawingDocumentViewModel => DrawingDocumentTemplate,
            _ => DefaultTemplate ?? base.SelectTemplate(item, container)
        };
    }
}
