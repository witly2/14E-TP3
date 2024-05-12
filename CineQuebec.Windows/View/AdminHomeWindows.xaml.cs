using System.Windows;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL.Repositories.Projections;

namespace CineQuebec.Windows.View;

public partial class AdminHomeWindows : Window
{
    private readonly AbonneService _abonneService;
    private readonly AdminHomeControl _adminHomeControl;
    private readonly FilmService _filmService;
    private readonly ProjectionService _projectionService;

    public AdminHomeWindows(Admin admin)
    {
        var db = new DatabaseGestion();
        _filmService = new FilmService(new FilmRepository(db));
        _abonneService = new AbonneService(new AbonneRepsitory(db));
        _projectionService = new ProjectionService(new ProjectionRepository(db));
        _adminHomeControl = new AdminHomeControl(admin);
        InitializeComponent();
        DataContext = this;

        AdminName.Text = admin.Username;
        imageUrl = $"https://robohash.org/{admin.Id}";

        mainContentControl.Content = _adminHomeControl;
    }

    public string imageUrl { get; set; }

    private void ToggleButton_Abonnes(object sender, RoutedEventArgs e)
    {
        mainContentControl.Content = new UsersControl(_abonneService);
    }

    private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
    {
        mainContentControl.Content = new AdminFilmsControl(_filmService, _projectionService);
    }

    private void Button_Deconnexion_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void Button_Accueil_Click(object sender, RoutedEventArgs e)
    {
        mainContentControl.Content = _adminHomeControl;
    }
}