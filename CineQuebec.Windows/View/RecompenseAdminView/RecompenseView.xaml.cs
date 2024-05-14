using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services.Recompenses;
using System.Windows;
using CineQuebec.Windows.ViewModels;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.BLL.Services.Reservations;

namespace CineQuebec.Windows.View.RecompenseAdminView
{
    public partial class RecompenseView : UserControl
    {
        private readonly RecompenseViewModel _viewModel;
        private readonly IRecompenseService _recompenseService;
        private readonly IPreferenceService _preferenceService;
        private readonly IReservationService _reservationService;
        private readonly IFilmService _filmService;

        public RecompenseView(IReservationService reservationService, IPreferenceService preferenceService, IRecompenseService recompenseService, IFilmService filmService)
        {
            InitializeComponent();
            _viewModel = new RecompenseViewModel(recompenseService, filmService);
            _recompenseService = recompenseService;
            _reservationService = reservationService;
            _preferenceService = preferenceService;
            _filmService = filmService;
            DataContext = _viewModel;
        }

        private void ToggleButton_AddRecompense_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateRecompenseView addUpdateRecompenseView = new AddUpdateRecompenseView(_recompenseService, _filmService);
            this.Content = addUpdateRecompenseView;
        }

        private void ToggleButton_EditRecompense_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridRecompenses.SelectedItem != null)
            {
                Recompense recompense = (Recompense)dataGridRecompenses.SelectedItem;
                OffrirRecompenseView offrirRecompenseView = new OffrirRecompenseView(_filmService, _reservationService, _preferenceService, _recompenseService, recompense);
                this.Content = offrirRecompenseView;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une récompense à éditer.", "Aucune récompense sélectionnée", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
