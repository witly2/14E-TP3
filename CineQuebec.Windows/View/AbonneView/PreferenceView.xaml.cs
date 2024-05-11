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

        private void AjouterPreferencesActeurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AjouterPreferencesActeurs();
        }

        private void AjouterPreferencesCategories_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AjouterPreferencesCategories();
        }

        private void EnleverPreferencesRealisateurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EnleverPreferencesRealisateurs();
        }

        private void EnleverPreferencesActeurs_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EnleverPreferencesActeurs();
        }

        private void EnleverPreferencesCategories_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EnleverPreferencesCategories();
        }
    }
}
