using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class DetailFilmControl : UserControl
{
    public DetailFilmControl(Film film)
    {
        InitializeComponent();
        this.DataContext = film;
    }
}