using System.Windows;
using AvalonDock.Layout;

namespace EtabInspector.UI.Helpers;

public static class LayoutAnchorableBehavior
{
    public static bool GetIsVisibleBinding(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsVisibleBindingProperty);
    }

    public static void SetIsVisibleBinding(DependencyObject obj, bool value)
    {
        obj.SetValue(IsVisibleBindingProperty, value);
    }

    public static readonly DependencyProperty IsVisibleBindingProperty =
        DependencyProperty.RegisterAttached(
            "IsVisibleBinding",
            typeof(bool),
            typeof(LayoutAnchorableBehavior),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsVisibleBindingChanged));

    private static void OnIsVisibleBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LayoutAnchorable anchorable && e.NewValue is bool isVisible)
        {
            anchorable.IsVisible = isVisible;
        }
    }
}
