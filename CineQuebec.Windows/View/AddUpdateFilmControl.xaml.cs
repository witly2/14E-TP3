using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View
{
    public partial class AddUpdateFilmControl: UserControl
    {
        private readonly IFilmService _filmServiceInterface;
        private readonly IProjectionService _projectionService;
        public AddUpdateFilmControl(IFilmService filmService, IProjectionService projectionService, Film filmToUpdate = null)
        {
            InitializeComponent();

            _filmServiceInterface = filmService;
            _projectionService = projectionService;

            TextBlock addUpdateFilmTextBlock = (TextBlock)this.FindName("addUpdateButton");
            TextBlock addUpdateTitleTextBlock = (TextBlock)this.FindName("addUpdateTitle");
            ComboBox realisateurComboBox = (ComboBox)this.FindName("realisateurComboBox");
            ComboBox acteurComboBox = (ComboBox)this.FindName("acteurComboBox");
            ComboBox categorieComboBox = (ComboBox)this.FindName("categorieComboBox");

            if (filmToUpdate != null)
            {
                addUpdateFilmTextBlock.Text = "Modifier";
                addUpdateTitleTextBlock.Text = "Modification film";
                this.DataContext = filmToUpdate;
                LoadComboBox();
            }
            else
            {
                LoadComboBox();
                addUpdateFilmTextBlock.Text = "Ajouter";
                addUpdateTitleTextBlock.Text = "Ajout film";
            }
        }

        public async Task LoadComboBox()
        {
            List<Acteur> acteurs = await _filmServiceInterface.GetAllActeurs();
            acteurComboBox.ItemsSource = acteurs;
            List<Realisateur> realisateurs = await _filmServiceInterface.GetAllRealisateurs();
            realisateurComboBox.ItemsSource = realisateurs;
            List<Categorie> categories = await _filmServiceInterface.GetAllCategories();
            categorieComboBox.ItemsSource = categories;
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

            Realisateur selectedRealisateur = realisateurComboBox.SelectedItem as Realisateur;
            List<Realisateur> realisateurs = new List<Realisateur> { selectedRealisateur };
            Acteur selectedActeur = acteurComboBox.SelectedItem as Acteur;
            List<Acteur> acteurs = new List<Acteur> { selectedActeur };
            Categorie selectedCategorie = categorieComboBox.SelectedItem as Categorie;
            List<Categorie> categories = new List<Categorie> { selectedCategorie };


            Film film = new Film(originalTitleForm, frenchTitleForm, descriptionForm, durationForm, internationalReleaseDateForm, ratingForm)
            {
                OriginalTitle = originalTitleForm,
                FrenchTitle = frenchTitleForm,
                Description = descriptionForm,
                Duration = durationForm,
                InternationalReleaseDate = internationalReleaseDateForm,
                Rating = ratingForm
            };
            film.SetListRealisateur(realisateurs);
            film.SetListActeur(acteurs);
            film.SetListCategorie(categories);
            return film;
        }
        public void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
        {
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
                            ReadFilmControl readFilmControl = new ReadFilmControl(newFilm);
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
                            ReadFilmControl readFilmControl = new ReadFilmControl(updateFilm);
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
            }
        }

    }
}
