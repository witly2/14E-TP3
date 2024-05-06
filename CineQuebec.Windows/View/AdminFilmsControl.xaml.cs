using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;

namespace CineQuebec.Windows.View;

public partial class AdminFilmsControl : UserControl
{
    private readonly IFilmService _filmService;
    private readonly IProjectionService _projectionService;
    private readonly DataGrid dataGrid;
    private List<Film> films;
    private readonly Dictionary<ObjectId, Tuple<Film, string>> filmsDictionary;

    public AdminFilmsControl(IFilmService filmService, IProjectionService projectionService)
    {
        InitializeComponent();

        _filmService = filmService;
        _projectionService = projectionService;
        filmsDictionary = new Dictionary<ObjectId, Tuple<Film, string>>();
        dataGrid = (DataGrid)FindName("dataGridFilms");

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
                var lastProjectionDate = projections.Max(p => p.DateHeureDebut);
                lastProjectionDateString = lastProjectionDate.ToString("yyyy-mm-dd");
            }
            else
            {
                lastProjectionDateString = "Aucune projection";
            }

            filmsDictionary.Add(film.Id, new Tuple<Film, string>(film, lastProjectionDateString));
        }

        DataContext = filmsDictionary;
    }

    public void DataGrid()
    {
        var dateColumn = (DataGridTextColumn)FindName("date");
        if (dateColumn != null) dateColumn.Binding.StringFormat = "yyyy-MM-dd";
    }

    private void ToggleButton_AddFilm_Click(object sender, RoutedEventArgs e)
    {
        var filmAddUpdateControl = new AddUpdateFilmControl(_filmService, _projectionService);
        Content = filmAddUpdateControl;
    }

    private void ToggleButton_UpdateFilm_Click(object sender, RoutedEventArgs e)
    {
        if (dataGrid.SelectedItem != null)
        {
            var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
            var filmToUpdate = selectedKeyValue.Value.Item1;
            if (filmToUpdate != null)
            {
                var updateControl = new AddUpdateFilmControl(_filmService, _projectionService, filmToUpdate);
                Content = updateControl;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un film à mettre à jour.");
            }
        }
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dataGrid.SelectedItem != null)
        {
            var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
            var selectedTuple = selectedKeyValue.Value;
            var selectedFilm = selectedTuple.Item1;
        }
    }

    private void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
    {
        if (dataGrid.SelectedItem != null)
        {
            var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
            var selectedFilm = selectedKeyValue.Value.Item1;
            var addProjectionControl = new AddProjectionControl(_projectionService, selectedFilm);
            Content = addProjectionControl;
        }
        else
        {
            MessageBox.Show("Veuillez sélectionner un film pour ajouter une projection.");
        }
    }

    private async void ToggleButton_DeleteFilm_Click(object sender, RoutedEventArgs e)
    {
        var selectedKeyValue = (KeyValuePair<ObjectId, Tuple<Film, string>>)dataGrid.SelectedItem;
        var selectedFilm = selectedKeyValue.Value.Item1;

        var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le film \"{selectedFilm.FrenchTitle}\" ?",
            "Confirmation de suppression", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            await _filmService.DeleteFilm(selectedFilm);
            MessageBox.Show("Film supprimé avec succès.");
            GetFilms();
        }
    }
}