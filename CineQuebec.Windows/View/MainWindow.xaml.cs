using System.Windows;
using System.Windows.Input;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL.Repositories.Persons;
using CineQuebec.Windows.DAL.Repositories.Projections;
using CineQuebec.Windows.DAL.Repositories.Recompenses;
using CineQuebec.Windows.View;
using CineQuebec.Windows.View.RecompenseAdminView;
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

namespace CineQuebec.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmService _filmService;
        private readonly AbonneService _abonneService;
        private readonly ProjectionService _projectionService;
        private readonly RecompenseService _recompenseService;
        private readonly ConnexionService _connexionService;
        private readonly Film film;
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
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _projectionService = new ProjectionService(new ProjectionRepository(db));
            _recompenseService = new RecompenseService(new RecompenseRepository(db));
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

        public void RecompenseView()
        {
            mainContentControl.Content = new RecompenseView(_recompenseService);
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