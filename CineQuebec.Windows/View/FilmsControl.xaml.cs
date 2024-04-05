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
        private readonly FilmService _filmService;
        private List<Film> films;
        private DataGrid dataGrid;
        private Dictionary<ObjectId, Tuple<Film, string>> filmsDictionary;

        public FilmsControl(FilmService filmService)
        {
            InitializeComponent();
            _filmService = filmService;
            filmsDictionary = new Dictionary<ObjectId, Tuple<Film, string>>();
            dataGrid = (DataGrid)this.FindName("dataGridFilms");
            GetFilms();
            DataGrid();
        }
        public void GetFilms()
        {
            films = _filmService.GetFilms();

            foreach (var film in films)
            {
                var projections = _filmService.GetProjections(film.Id);
                string lastProjectionDateString;
                if (projections.Any())
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
                var selectedTuple = selectedKeyValue.Value;
                Film selectedFilm = selectedTuple.Item1;
                MessageBox.Show($"Film: {selectedFilm.FrenchTitle}");
            }
        }

        private void ToggleButton_AddFilms_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Click add film.");
            //mainContentControl.Content = new FilmsControl(_filmService);
        }
    }
}
