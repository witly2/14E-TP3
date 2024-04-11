using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.View;
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
        private Film film;

        public MainWindow()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));

            InitializeComponent();
            mainContentControl.Content = new ConnexionControl();
        }

     

        public void InscriptionControl()
        {
            mainContentControl.Content = new InscriptionControl1();
        }

        public void ConnexionControl()
        {
            mainContentControl.Content = new ConnexionControl();
        }

        public void UsersControl()
        {
            mainContentControl.Content = new UsersControl(_abonneService);
        }

        public void FilmsControl()
        {
            mainContentControl.Content = new FilmsControl(_filmService);
        }

        public void AddUpdateFilmControl()
        {
            mainContentControl.Content = new AddUpdateFilmControl(_filmService);
        }
    }
}