
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL;
using System.Windows;
using System.Windows.Controls;


namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NavWindows.xaml
    /// </summary>
    public partial class NavWindows : Window
    {
        private readonly FilmService _filmService;
        public NavWindows()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));

            InitializeComponent();
            mainContentControl.Content = new AdminHomeControl();

        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl();

        }

        private void ToggleButton_Films_Click(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new FilmsControl(_filmService);
        }


    }
}
