using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View
{
    public partial class FilmsControl: UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;
        private List<Film> films;
        private DataGrid dataGrid;
        private Dictionary<ObjectId, Tuple<Film, string>> filmsDictionary;

        public FilmsControl(IFilmService filmService, IProjectionService projectionService)
        {
            InitializeComponent();
            _filmService = filmService;
            _projectionService = projectionService;
            filmsDictionary = new Dictionary<ObjectId, Tuple<Film, string>>();
            dataGrid = (DataGrid)this.FindName("dataGridFilms");
            GetFilms();
            DataGrid();
        }
        public async void GetFilms()
        {
            films = await _filmService.GetFilms();

            foreach (var film in films)
            {
                var projections = await _filmService.GetProjections(film);
                string lastProjectionDateString;
                if (projections.Count > 0)
                {
                    DateTime lastProjectionDate = projections.Max(p => p.DateHeureDebut);
                    lastProjectionDateString = lastProjectionDate.ToString("yyyy-mm-dd");
                }
                else
                {
                    lastProjectionDateString = "Aucune projection";
                }
                filmsDictionary.Add(film.Id, new Tuple<Film, string>(film, lastProjectionDateString));
            }

            this.DataContext = filmsDictionary;
        }

        public void DataGrid()
        {
            DataGridTextColumn dateColumn = (DataGridTextColumn)this.FindName("date");
            if (dateColumn != null)
            {
                dateColumn.Binding.StringFormat = "yyyy-MM-dd";
            }
        }

        private void ToggleButton_AddFilm_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateFilmControl filmAddUpdateControl = new AddUpdateFilmControl(_filmService, _projectionService);
            this.Content = filmAddUpdateControl;
        }

        private void ToggleButton_UpdateFilm_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
                var filmToUpdate = selectedKeyValue.Value.Item1 as Film;
                if (filmToUpdate != null)
                {
                    AddUpdateFilmControl updateControl = new AddUpdateFilmControl(_filmService, _projectionService, filmToUpdate);
                    this.Content = updateControl;
                } else
                {
                    MessageBox.Show($"Veuillez sélectionner un film à mettre à jour.");
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
                var selectedTuple = selectedKeyValue.Value;
                Film selectedFilm = selectedTuple.Item1;
            }
        }

        private void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
                Film selectedFilm = selectedKeyValue.Value.Item1;
                AddProjectionControl addProjectionControl = new AddProjectionControl(_projectionService, selectedFilm);
                this.Content = addProjectionControl;
            }
            else
            {
                MessageBox.Show($"Veuillez sélectionner un film pour ajouter une projection.");
            }
        }

        private async void ToggleButton_DeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
            Film selectedFilm = selectedKeyValue.Value.Item1;

            MessageBoxResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le film \"{selectedFilm.FrenchTitle}\" ?", "Confirmation de suppression", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                await _filmService.DeleteFilm(selectedFilm);
                MessageBox.Show($"Film supprimé avec succès.");
                GetFilms();
            } 
        }
    }
}
