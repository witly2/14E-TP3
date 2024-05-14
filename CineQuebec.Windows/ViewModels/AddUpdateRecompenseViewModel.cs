using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View;
using CineQuebec.Windows.View.RecompenseAdminView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineQuebec.Windows.ViewModels
{
    public class AddUpdateRecompenseViewModel : NotifyPropertyChangeBase
    {
        private readonly IRecompenseService _recompenseService;
        private IFilmService _filmService;
        private ObservableCollection<Film> _films;
        private ObservableCollection<Projection> _projections;
        private TypeRecompense _selectedTypeRecompense;
        private Film _selectedFilm;
        private Projection _selectedProjection;
        private int _nombrePlace;
        private Visibility _projectionsFilmVisibility = Visibility.Collapsed;

        public AddUpdateRecompenseViewModel(IRecompenseService recompenseService, IFilmService filmService)
        {
            _recompenseService = recompenseService;
            _filmService = filmService;
            LoadFilms();
        }

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set
            {
                _films = value;
                OnPropertyChanged(nameof(Films));
            }
        }

        public ObservableCollection<Projection> Projections
        {
            get { return _projections; }
            set
            {
                _projections = value;
                OnPropertyChanged(nameof(Projections));
            }
        }

        public TypeRecompense SelectedTypeRecompense
        {
            get { return _selectedTypeRecompense; }
            set
            {
                _selectedTypeRecompense = value;
                OnPropertyChanged(nameof(SelectedTypeRecompense));
            }
        }

        public Film SelectedFilm
        {
            get { return _selectedFilm; }
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }

        public Projection SelectedProjection
        {
            get { return _selectedProjection; }
            set
            {
                _selectedProjection = value;
                OnPropertyChanged(nameof(SelectedProjection));
            }
        }

        public int NombrePlace
        {
            get { return _nombrePlace; }
            set
            {
                _nombrePlace = value;
                OnPropertyChanged(nameof(NombrePlace));
            }
        }

        public Visibility ProjectionsFilmVisibility
        {
            get { return _projectionsFilmVisibility; }
            set { _projectionsFilmVisibility = value; OnPropertyChanged(); }
        }

        public async void LoadFilms()
        {
            List<Film> films = await _filmService.GetFilmsAvecProjectionsFutures();
            if(films != null)
            {
                Films = new ObservableCollection<Film>(films);
            }
            else
            {
                MessageBox.Show($"Auncun film à l'affiche.");
            }
        }

        public async void LoadProjectionsForFilm(Film film)
        {
            List<Projection> projections = await _filmService.GetProjectionsFutures(film);
            if (projections != null)
            {
                Projections = new ObservableCollection<Projection>(projections);
            }
            else
            {
                MessageBox.Show($"Aucune projection futures.");
            }
        }

        public async void AddUpdateRecompense()
        {
            if(SelectedTypeRecompense == null || SelectedFilm == null || SelectedProjection == null || NombrePlace <= 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs pour ajouter une récompense.");
                return;
            }
            else
            {
                Recompense recompense = new Recompense(null, SelectedTypeRecompense, SelectedProjection, NombrePlace);
                Recompense recompenseAjouter = await _recompenseService.AjouterRecompense(recompense);
                if (recompenseAjouter != null)
                {
                    MessageBox.Show("Récompense ajoutée avec succès !");
                }
                else
                {
                    MessageBox.Show($"Une erreur s'est produite lors de l'ajout de la récompense.");
                }
            }
        }

        public async void VoirProjection()
        {
            LoadProjectionsForFilm(_selectedFilm);
            ProjectionsFilmVisibility = Visibility.Visible;
        }
    }
}
