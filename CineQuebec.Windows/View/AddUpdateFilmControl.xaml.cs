using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class AddUpdateFilmControl : UserControl
{
    private readonly IFilmService _filmServiceInterface;
    private readonly IProjectionService _projectionService;

    public AddUpdateFilmControl(IFilmService filmService, IProjectionService projectionService,
        Film filmToUpdate = null)
    {
        InitializeComponent();

        _filmServiceInterface = filmService;
        _projectionService = projectionService;

        var addUpdateFilmTextBlock = (TextBlock)FindName("addUpdateButton");
        var addUpdateTitleTextBlock = (TextBlock)FindName("addUpdateTitle");

        if (filmToUpdate != null)
        {
            Debug.WriteLine($"Titre FR: {filmToUpdate.FrenchTitle}");
            addUpdateFilmTextBlock.Text = "Modifier";
            addUpdateTitleTextBlock.Text = "Modification film";
            DataContext = filmToUpdate;
        }
        else
        {
            addUpdateFilmTextBlock.Text = "Ajouter";
            addUpdateTitleTextBlock.Text = "Ajout film";
        }
    }

    public Film GetFilmForm()
    {
        var frenchTitleForm = titreFr.Text;
        var originalTitleForm = originalTitle.Text;
        var internationalReleaseDateForm = InternationalReleaseDate.SelectedDate ?? DateTime.Now;
        ushort durationForm;

        if (!ushort.TryParse(duration.Text, out durationForm))
            MessageBox.Show("La durée doit être un nombre entier positif.");

        var descriptionForm = description.Text;
        ushort ratingForm;
        var filmToUpdate = DataContext as Film;

        if (filmToUpdate != null && filmToUpdate.Rating.HasValue)
            ratingForm = filmToUpdate.Rating.Value;
        else
            ratingForm = 0;

        var film = new Film(originalTitleForm, frenchTitleForm, descriptionForm, durationForm,
            internationalReleaseDateForm, ratingForm)
        {
            OriginalTitle = originalTitleForm,
            FrenchTitle = frenchTitleForm,
            Description = descriptionForm,
            Duration = durationForm,
            InternationalReleaseDate = internationalReleaseDateForm,
            Rating = ratingForm
        };

        return film;
    }

    public void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext as Film != null)
        {
            var film = DataContext as Film;
            var addProjection = new AddProjectionControl(_projectionService, film);
            Content = addProjection;
        }
        else
        {
            MessageBox.Show("Veuillez ajouter un film ou retourner à la liste des films pour ajouter une projection.");
        }
    }

    public async void addUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        var addUpdateFilmTextBlock = (TextBlock)FindName("addUpdateButton");
        var film = GetFilmForm();

        if (film != null)
        {
            if (addUpdateFilmTextBlock.Text == "Ajouter")
                try
                {
                    var newFilm = await _filmServiceInterface.AddFilm(film);

                    if (newFilm != null)
                    {
                        var readFilmControl = new ReadFilmControl(newFilm);
                        readFilmControl.DataContext = newFilm;
                        Content = readFilmControl;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout du film.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du film : {ex.Message}");
                }
            else if (addUpdateFilmTextBlock.Text == "Modifier")
                try
                {
                    var updateFilm = await _filmServiceInterface.UpdateFilm(film);

                    if (updateFilm != null)
                    {
                        var readFilmControl = new ReadFilmControl(updateFilm);
                        readFilmControl.DataContext = updateFilm;
                        Content = readFilmControl;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la mise à jour du film.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise à jour du film : {ex.Message}");
                }
        }
        else
        {
            MessageBox.Show("Erreur lors de la récupération du film formulaire.");
        }
    }
}