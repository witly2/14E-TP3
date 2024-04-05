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
        private Dictionary<ObjectId, Film> filmsDictionary;

        public FilmsControl(FilmService filmService)
        {
            InitializeComponent();
            _filmService = filmService;
            filmsDictionary = new Dictionary<ObjectId, Film>();
            dataGrid = (DataGrid)this.FindName("dataGridFilms");
            GetFilms();
            DataGrid();
        }
        public void GetFilms()
        {
            films = _filmService.GetFilms();

            foreach (var film in films)
            {
                filmsDictionary.Add(film.Id, film);
            }

            this.DataContext = filmsDictionary;
        }

        public void DataGrid()
        {
            DataGridTextColumn clnRang = (DataGridTextColumn)this.FindName("clnRang");
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
                var selectedFilm = (KeyValuePair<ObjectId, Film>)dataGrid.SelectedItem;
                ObjectId selectedGuid = selectedFilm.Key;

                if(filmsDictionary.TryGetValue(selectedGuid, out Film film))
                {
                    MessageBox.Show($"Film: {film.FrenchTitle}");
                }
            }

        }

        private void ToggleButton_AddFilms_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Click add film.");
            //mainContentControl.Content = new FilmsControl(_filmService);
        }
    }
}
