﻿using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL.Repositories.Projections;
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
        private readonly ProjectionService _projectionService;
        private readonly Film film;

        public MainWindow()
        {
            DatabaseGestion db = new DatabaseGestion();
            _filmService = new FilmService(new FilmRepository(db));
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _projectionService = new ProjectionService(new ProjectionRepository(db));

            InitializeComponent();

            mainContentControl.Content = new FilmControl();
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

        }
        
        private void click_Inscription(object sender, RoutedEventArgs routedEventArgs)
        {
            mainContentControl.Content = new InscriptionControl1();
        }
        
        private void click_Connexion(object sender, RoutedEventArgs routedEventArgs)
        {
            mainContentControl.Content = new ConnexionControl();
        }
    }
}