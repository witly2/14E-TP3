using System.Windows;
using System.Windows.Input;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL.Repositories.Persons;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.View;
using CineQuebec.Windows.View.UsersControls;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AbonneService _abonneService;
    private readonly FilmService _filmService;
    private readonly ProjectionService _projectionService;
    private readonly Film film;
    private readonly ConnexionService _connexionService;
    private AccueilFilmControl accueilFilmControl;

    public MainWindow()
    {
        var db = new DatabaseGestion();
        _filmService = new FilmService(new FilmRepository(db));
        _abonneService = new AbonneService(new AbonneRepsitory(db));
        _projectionService = new ProjectionService(new ProjectionRepository(db));
        _connexionService = new ConnexionService(new PersonRepository(db));


        InitializeComponent();
        var accueilViewModel = new AccueilViewModel(mainContentControl);

        DataContext = accueilViewModel;
        

        mainContentControl.Content = new AccueilFilmControl();
    }


    public void InscriptionControl(Film film = null)
    {
        mainContentControl.Content = new InscriptionControl1(film);
    }

    public void ConnexionControl( Film film = null)
    {
        mainContentControl.Content = new ConnexionControl(_connexionService,  film);
    }


    public void UsersControl()
    {
        mainContentControl.Content = new UsersControl(_abonneService);
    }

    public void FilmsControl()
    {
        mainContentControl.Content = new AdminFilmsControl(_filmService, _projectionService);
    }

    public void AddUpdateFilmControl()
    {
        mainContentControl.Content = new AddUpdateFilmControl(_filmService, _projectionService);
    }

    public void AddProjectionControl()
    {
        mainContentControl.Content = new AddProjectionControl(_projectionService, film);
    }


    private void click_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
    {
        mainContentControl.Content = new AccueilFilmControl();
    }

    private void click_Inscription(object sender, RoutedEventArgs routedEventArgs)
    {
        mainContentControl.Content = new InscriptionControl1();
    }

    private void click_Connexion(object sender, RoutedEventArgs routedEventArgs)
    {
        mainContentControl.Content = new ConnexionControl(_connexionService);
    }
    
    public void DetailFilmControl( Film film)
    {
        mainContentControl.Content = new DetailFilmControl(film);
    }
}