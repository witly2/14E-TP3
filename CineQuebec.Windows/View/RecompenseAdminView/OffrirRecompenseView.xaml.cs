using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View.AbonneView;
using CineQuebec.Windows.ViewModels;
using MongoDB.Bson;
using static MaterialDesignThemes.Wpf.Theme;

namespace CineQuebec.Windows.View.RecompenseAdminView;

public partial class OffrirRecompenseView : UserControl
{
    private readonly OffrirRecompenseViewModel _viewModel;
    private readonly IReservationService _reservationService;
    private readonly IPreferenceService _preferenceService;
    private readonly IRecompenseService _recompenseService;
    private readonly IFilmService _filmService;
    private readonly Recompense _recompense;

    public OffrirRecompenseView(IFilmService filmService, IReservationService reservationService, IPreferenceService preferenceService, IRecompenseService recompenseService, Recompense recompense)
    {
        InitializeComponent();
        _reservationService = reservationService;
        _preferenceService = preferenceService;
        _recompenseService = recompenseService;
        _filmService = filmService;
        _recompense = recompense;
        _viewModel = new OffrirRecompenseViewModel(reservationService, preferenceService, recompenseService, recompense);
        DataContext = _viewModel;
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dataGridAbonnes.SelectedItem != null)
        {
            var selectedAbonne = (Abonne)dataGridAbonnes.SelectedItem;
        }
    }

    public void AddAbonneListRecompense_Click(object sender, RoutedEventArgs e)
    {
        if (dataGridAbonnes.SelectedItem != null)
        {
            var selectedAbonne = (Abonne)dataGridAbonnes.SelectedItem;
            _recompenseService.AjouterAbonne(_recompense, selectedAbonne);
            RecompenseView recompenseView = new RecompenseView(_reservationService, _preferenceService, _recompenseService, _filmService);
            this.Content = recompenseView;
        }
        else
        {
            MessageBox.Show($"Veuillez sélectionner un abonné pour lui offrir la récompense.");
        }
    }
}