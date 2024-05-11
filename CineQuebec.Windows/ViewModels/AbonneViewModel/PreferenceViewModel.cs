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
        private readonly IPreferenceService _preferenceService;
        private ObservableCollection<Realisateur> _realisateurs;
        private ObservableCollection<Acteur> _acteurs;
        private ObservableCollection<Categorie> _categories;
        private List<Realisateur> _realisateursBDList;
        private List<Acteur> _acteursBDList;
        private List<Categorie> _categoriesBDList;
        private Realisateur _realisateurSelectionne;
        private Realisateur _realisateurAEnleverSelectionne;
        private Acteur _acteurSelectionne;
        private Acteur _acteurAEnleverSelectionne;
        private Categorie _categorieSelectionne;
        private Categorie _categorieAEnleverSelectionne;

        public Realisateur RealisateurSelectionne
        {
            get { return _realisateurSelectionne; }
            set
            {
                _realisateurSelectionne = value; OnPropertyChanged();
            }
        }

        public Realisateur RealisateurAEnleverSelectionne
        {
            get { return _realisateurAEnleverSelectionne; }
            set
            {
                _realisateurAEnleverSelectionne = value; OnPropertyChanged();
            }
        }

        public List<Realisateur> RealisateursBDList
        {
            get { return _realisateursBDList; }
            set { _realisateursBDList = value; OnPropertyChanged(); }
        }

        public Acteur ActeurSelectionne
        {
            get { return _acteurSelectionne; }
            set
            {
                _acteurSelectionne = value; OnPropertyChanged();
            }
        }

        public Acteur ActeurAEnleverSelectionne
        {
            get { return _acteurAEnleverSelectionne; }
            set
            {
                _acteurAEnleverSelectionne = value; OnPropertyChanged();
            }
        }

        public List<Acteur> ActeursBDList
        {
            get { return _acteursBDList; }
            set { _acteursBDList = value; OnPropertyChanged(); }
        }

        public Categorie CategorieSelectionne
        {
            get { return _categorieSelectionne; }
            set
            {
                _categorieSelectionne = value; OnPropertyChanged();
            }
        }

        public Categorie CategorieAEnleverSelectionne
        {
            get { return _categorieAEnleverSelectionne; }
            set
            {
                _categorieAEnleverSelectionne = value; OnPropertyChanged();
            }
        }

        public List<Categorie> CategoriesBDList
        {
            get { return _categoriesBDList; }
            set { _categoriesBDList = value; OnPropertyChanged(); }
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

        public async void ModifierPreferencesActeurs() 
        {
            var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
            ActeursBDList = await _preferenceService.GetAllActeurs(preferences);
            ActeursPreferencesVisibility = Visibility.Visible;
        }

        public async void ModifierPreferencesCategories() 
        {
            var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
            CategoriesBDList = await _preferenceService.GetAllCategories(preferences);
            CategoriesPreferencesVisibility = Visibility.Visible;
        }

        public void AjouterPreferencesRealisateurs()
        {
            if(RealisateurSelectionne != null)
            {
                var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
                if(preferences.ListPreferenceRealisateur.Count >= 5)
                {
                    MessageBox.Show("La limite de 5 réalisateurs préférés est atteinte.", "Limite atteinte", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _preferenceService.AddPreferenceRealisateur(preferences, RealisateurSelectionne);
                    Realisateurs = new ObservableCollection<Realisateur>(preferences.ListPreferenceRealisateur);
                    RealisateursPreferencesVisibility = Visibility.Hidden;
                } 
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un réalisateur à ajouter.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void EnleverPreferencesRealisateurs()
        {
            if (RealisateurAEnleverSelectionne != null)
            {
                var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
                _preferenceService.RemovePreference(preferences, RealisateurAEnleverSelectionne, p => p.ListPreferenceRealisateur);
                Realisateurs.Remove(RealisateurAEnleverSelectionne);
                RealisateursPreferencesVisibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un réalisateur à supprimer.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void AjouterPreferencesActeurs()
        {
            if (ActeurSelectionne != null)
            {
                var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
                if (preferences.ListPreferenceActeur.Count >= 5)
                {
                    MessageBox.Show("La limite de 5 acteurs préférés est atteinte.", "Limite atteinte", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _preferenceService.AddPreferenceActeur(preferences, ActeurSelectionne);
                    Acteurs = new ObservableCollection<Acteur>(preferences.ListPreferenceActeur);
                    ActeursPreferencesVisibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un acteur à ajouter.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        internal void AjouterPreferencesCategories()
        {
            if (CategorieSelectionne != null)
            {
                var preferences = _preferenceService.GetPreferenceAbonne(_abonne);
                if (preferences.ListPreferenceCategorie.Count >= 3)
                {
                    MessageBox.Show("La limite de 3 catégories préférés est atteinte.", "Limite atteinte", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _preferenceService.AddPreferenceCategorie(preferences, CategorieSelectionne);
                    Categories = new ObservableCollection<Categorie>(preferences.ListPreferenceCategorie);
                    CategoriesPreferencesVisibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à ajouter.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        internal void EnleverPreferencesActeurs()
        {
            throw new NotImplementedException();
        }

        internal void EnleverPreferencesCategories()
        {
            throw new NotImplementedException();
        }
    }
}
