using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeLibrary;
using System.Collections.ObjectModel;

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

public partial class MainWindow : Window
{
    public ObservableCollection<Book> MyBooks { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        MyBooks = MyBookCollection.GetMyCollection();
        BooksListBox.ItemsSource = MyBooks;
    }

    private void BookDetails_DeleteRequested(object sender, RoutedEventArgs e)
    {
        var selected = BooksListBox.SelectedItem as Book;

        if (selected != null)
        {
            MyBooks.Remove(selected);
        }
    }
}
