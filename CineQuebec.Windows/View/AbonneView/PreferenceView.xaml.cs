using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.ViewModels.AbonneViewModel;
using CineQuebec.Windows.BLL.Services.Preferences;
using System.Windows;

namespace CineQuebec.Windows.View.AbonneView
{
    public partial class PreferenceView : UserControl
    {
        private readonly PreferenceViewModel _viewModel;
        public PreferenceView(Abonne abonne, IPreferenceService preferenceService)
        {
            InitializeComponent();
            _viewModel = new PreferenceViewModel(abonne, preferenceService);
            DataContext = _viewModel;
        }
        /*
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PreferenceViewModel.Realisateurs))
            {
                txtPreferencesRealisateurs.Text = string.Join(", ", _viewModel.Realisateurs);
            }
            else if (e.PropertyName == nameof(PreferenceViewModel.Acteurs))
            {
                txtPreferencesActeurs.Text = string.Join(", ", _viewModel.Acteurs);
            }
            else if (e.PropertyName == nameof(PreferenceViewModel.Categories))
            {
                txtPreferencesCategories.Text = string.Join(", ", _viewModel.Categories);
            }
        }*/

        private void ModifierPreferencesRealisateurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ModifierPreferencesRealisateurs();
        }

        private void ModifierPreferencesActeurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ModifierPreferencesActeurs();
        }

        private void ModifierPreferencesCategories_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ModifierPreferencesCategories();
        }

        private void AjouterPreferencesRealisateurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AjouterPreferencesRealisateurs();
        }
    }
}
