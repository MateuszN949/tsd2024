using System.Windows;
using System.Windows.Controls;

namespace WPF;

public partial class BookDetailsControl : UserControl
{
    public BookDetailsControl()
    {
        InitializeComponent();
    }

    public static readonly RoutedEvent DeleteRequestedEvent =
        EventManager.RegisterRoutedEvent(
            "DeleteRequested",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(BookDetailsControl));

    public event RoutedEventHandler DeleteRequested
    {
        add { AddHandler(DeleteRequestedEvent, value); }
        remove { RemoveHandler(DeleteRequestedEvent, value); }
    }

    private void OnDeleteClick(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show(
            "Delete this book?",
            "Confirm",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            RaiseEvent(new RoutedEventArgs(DeleteRequestedEvent));
        }
    }
}