using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Repositories;
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
        public MainWindow()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));

            InitializeComponent();
            mainContentControl.Content = new ConnexionControl();
        }

        public void AdminHomeControl()
        {
            mainContentControl.Content = new AdminHomeControl();
        }

        //public void NavControl()
        //{
        //    mainContentControl.Content = new NavWindows();
        //}


        public void UsersControl()
        {
            mainContentControl.Content = new UsersControl();
        }

        public void FilmsControl()
        {
            mainContentControl.Content = new FilmsControl(_filmService);
        }
    }
}