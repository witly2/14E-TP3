using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

/// <summary>
///     Logique d'interaction pour AdminHomeControl.xaml
/// </summary>
public partial class AdminHomeControl : UserControl
{
    private readonly FilmService _filmService;

    public AdminHomeControl(Abonne abonne)
    {
        var db = new DatabaseGestion();
        DataContext = abonne;

        InitializeComponent();
    }
}