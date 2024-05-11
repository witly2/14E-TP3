using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View.AbonneView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.ViewModels.AbonneViewModel
{
    public class PreferenceViewModel : NotifyPropertyChangeBase
    {
        private readonly Abonne _abonne;
        //private readonly Preference? _preferences;
        private readonly IPreferenceService _preferenceService;
        private ObservableCollection<Realisateur> _realisateurs;
        private ObservableCollection<Acteur> _acteurs;
        private ObservableCollection<Categorie> _categories;
        private List<Realisateur> _realisateursBDList;
        private Realisateur _realisateurSelectionne;

        public Realisateur RealisateurSelectionne
        {
            get { return _realisateurSelectionne; }
            set
            {
                _realisateurSelectionne = value; OnPropertyChanged();
            }
        }

        public List<Realisateur> RealisateursBDList
        {
            get { return _realisateursBDList; }
            set { _realisateursBDList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Realisateur> Realisateurs
        {
            get { return _realisateurs; }
            set { _realisateurs = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Acteur> Acteurs
        {
            get { return _acteurs; }
            set { _acteurs = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }

        private Visibility _realisateursPreferencesVisibility = Visibility.Collapsed;
        public Visibility RealisateursPreferencesVisibility
        {
            get { return _realisateursPreferencesVisibility; }
            set { _realisateursPreferencesVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _realisateursBDVisibility = Visibility.Collapsed;
        public Visibility RealisateursBDVisibility
        {
            get { return _realisateursBDVisibility; }
            set { _realisateursBDVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _acteursPreferencesVisibility = Visibility.Collapsed;
        public Visibility ActeursPreferencesVisibility
        {
            get { return _acteursPreferencesVisibility; }
            set { _acteursPreferencesVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _acteursBDVisibility = Visibility.Collapsed;
        public Visibility ActeursBDVisibility
        {
            get { return _acteursBDVisibility; }
            set { _acteursBDVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _categoriesPreferencesVisibility = Visibility.Collapsed;
        public Visibility CategoriesPreferencesVisibility
        {
            get { return _categoriesPreferencesVisibility; }
            set { _categoriesPreferencesVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _categoriesBDVisibility = Visibility.Collapsed;
        public Visibility CategoriesBDVisibility
        {
            get { return _categoriesBDVisibility; }
            set { _categoriesBDVisibility = value; OnPropertyChanged(); }
        }

        public PreferenceViewModel(Abonne abonne, IPreferenceService preferenceService){
            _abonne = abonne;
            _preferenceService = preferenceService;

            LoadPreferences();
        }

        private async Task LoadPreferences()
        {
            var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
            if (preferences != null)
            {
                Realisateurs = new ObservableCollection<Realisateur>(preferences.ListPreferenceRealisateur);
                Acteurs = new ObservableCollection<Acteur>(preferences.ListPreferenceActeur);
                Categories = new ObservableCollection<Categorie>(preferences.ListPreferenceCategorie);
            } 
        }

        public async void ModifierPreferencesRealisateurs()
        {
            var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
            RealisateursBDList = await _preferenceService.GetAllRealisateurs(preferences);
            RealisateursPreferencesVisibility = Visibility.Visible;
        }

        public void ModifierPreferencesActeurs() 
        {
            ActeursPreferencesVisibility = Visibility.Visible;
        }

        public void ModifierPreferencesCategories() 
        {
            CategoriesPreferencesVisibility = Visibility.Visible;
        }

        public void AjouterPreferencesRealisateurs()
        {
            if(RealisateurSelectionne != null)
            {
                var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
                _preferenceService.AddPreferenceRealisateur(preferences, RealisateurSelectionne);
                Realisateurs = new ObservableCollection<Realisateur>(preferences.ListPreferenceRealisateur);
                RealisateursPreferencesVisibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un réalisateur à ajouter.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
