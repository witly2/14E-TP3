using System.Windows.Controls;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View;

public partial class FilmControl : UserControl
{
    public FilmControl()
    {
        InitializeComponent();
        AccueilVM filmVM = new AccueilVM();
        this.DataContext = filmVM;
    }
}