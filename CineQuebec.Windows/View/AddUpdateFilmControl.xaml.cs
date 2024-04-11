using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class AddUpdateFilmControl: UserControl
    {
        private readonly IFilmService _filmServiceInterface;
        private readonly IProjectionService _projectionService;
        public AddUpdateFilmControl(IFilmService filmService, Film filmToUpdate = null)
        {
            InitializeComponent();
            _filmServiceInterface = filmService;
            TextBlock addUpdateFilmTextBlock = (TextBlock)this.FindName("addUpdateButton");
            TextBlock addUpdateTitleTextBlock = (TextBlock)this.FindName("addUpdateTitle");
            if (filmToUpdate != null)
            {
                Debug.WriteLine($"Titre FR: {filmToUpdate.FrenchTitle}");
                addUpdateFilmTextBlock.Text = "Modifier";
                addUpdateTitleTextBlock.Text = "Modification film";
                this.DataContext = filmToUpdate;
            }
            else
            {
                addUpdateFilmTextBlock.Text = "Ajouter";
                addUpdateTitleTextBlock.Text = "Ajout film";
            }
        }

        public Film GetFilmForm()
        {
            string frenchTitleForm = titreFr.Text;
            string originalTitleForm = originalTitle.Text;
            DateTime internationalReleaseDateForm = InternationalReleaseDate.SelectedDate ?? DateTime.Now;
            ushort durationForm;
            if (!ushort.TryParse(duration.Text, out durationForm))
            {
                MessageBox.Show("La durée doit être un nombre entier positif.");
            }
            string descriptionForm = description.Text;
            ushort ratingForm;
            Film filmToUpdate = this.DataContext as Film;
            if (filmToUpdate != null && filmToUpdate.Rating.HasValue)
            {
                ratingForm = filmToUpdate.Rating.Value;
            }
            else
            {
                ratingForm = 0;
            }

            Film film = new Film(originalTitleForm, frenchTitleForm, descriptionForm, durationForm, internationalReleaseDateForm, ratingForm)
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
            MessageBox.Show($"Ajout projection bientôt disponible.");
            if(this.DataContext as Film != null)
            {
                Film film = this.DataContext as Film;
                AddProjectionControl addProjection = new AddProjectionControl(_projectionService, film);
                this.Content = addProjection;
            }
            else
            {
                MessageBox.Show("Veuillez ajouter un film ou retourner à la liste des films pour ajouter une projection.");
            }
        }

        public async void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ajout addUpdate.");
            /*
            TextBlock addUpdateFilmTextBlock = (TextBlock)this.FindName("addUpdateButton");
            Film film = GetFilmForm();

            if (film != null)
            {
                if (addUpdateFilmTextBlock.Text == "Ajouter")
                {
                    try
                    {
                        Film newFilm = await _filmServiceInterface.AddFilm(film);

                        if (newFilm != null)
                        {
                            ReadFilmControl readFilmControl = new ReadFilmControl();
                            readFilmControl.DataContext = newFilm;
                            this.Content = readFilmControl;
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
                }
                else if (addUpdateFilmTextBlock.Text == "Modifier")
                {
                    try
                    {
                        Film updateFilm = await _filmServiceInterface.UpdateFilm(film);

                        if (updateFilm != null)
                        {
                            ReadFilmControl readFilmControl = new ReadFilmControl();
                            readFilmControl.DataContext = updateFilm;
                            this.Content = readFilmControl;
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
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération du film formulaire.");
            }*/
        }

    }
}
