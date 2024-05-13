using System.Windows;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class ReservationWindows : Window
{
    private readonly Film _film;
    private readonly Abonne _abonne;
    public ReservationWindows(Abonne abonne, Film film)
    {
        InitializeComponent();
        _abonne = abonne;
        _film = film;
    }
 
}