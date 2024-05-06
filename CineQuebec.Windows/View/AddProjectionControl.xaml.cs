using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MahApps.Metro.Controls;

namespace CineQuebec.Windows.View
{
    public partial class AddProjectionControl: UserControl
    {
        private readonly IProjectionService _projectionService;
        private readonly Film _film;
        public AddProjectionControl(IProjectionService projectionService, Film film)
        {
            InitializeComponent();
            _projectionService = projectionService;
            _film = film;
            DataContext = film;

            //datePicker.SelectedDateChanged += SelectionChanged;
            //timePicker.SelectedDateTimeChanged += SelectionChanged;
        }

        private void validerDateHeureButton_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue)
            {
                salleComboBox.Visibility = Visibility.Visible;
                UpdateSalleComboBox();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une date et une heure pour valider.");
            }
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
            if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue)
            {
                salleComboBox.Visibility = Visibility.Visible;
                UpdateSalleComboBox();
            }
            else
            {
                salleComboBox.Visibility = Visibility.Collapsed;
                salleComboBox.Items.Clear();
            }
        }

        private void UpdateButtonState()
        {
            bool isSalleSelected = salleComboBox.SelectedItem != null;
            if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue && isSalleSelected)
                addProjectionButton.IsEnabled = true;
        }

        private async void UpdateSalleComboBox()
        {
            if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue)
            {
                DateTime selectedDateTime = timePicker.SelectedDateTime.Value;
                TimeSpan selectedTime = selectedDateTime.TimeOfDay;
                DateTime dateHeureDebut = datePicker.SelectedDate.Value.Date + selectedTime;
                List<Salle> salles = await _projectionService.GetSallesDisponibles(_film, dateHeureDebut);
                salleComboBox.Items.Clear();
                foreach (Salle salle in salles)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Content = "Salle " + salle.NumeroSalle + " - " + salle.NombrePlace + " places";
                    comboBoxItem.Tag = salle;
                    salleComboBox.Items.Add(comboBoxItem);
                }
            }
        }

        public async void addProjectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatePicker datePicker = (DatePicker)this.FindName("datePicker");
                TimePicker timePicker = (TimePicker)this.FindName("timePicker");
                ComboBox salleComboBox = (ComboBox)this.FindName("salleComboBox");
                bool isSalleSelected = salleComboBox.SelectedItem != null;

                if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue && isSalleSelected)
                {
                    DateTime selectedDateTime = timePicker.SelectedDateTime.Value;
                    TimeSpan selectedTime = selectedDateTime.TimeOfDay;
                    DateTime dateHeureDebut = datePicker.SelectedDate.Value.Date + selectedTime;

                    ComboBoxItem selectedSalleItem = (ComboBoxItem)salleComboBox.SelectedItem;
                    Salle selectedSalle = selectedSalleItem.Tag as Salle;
                    Projection newProjection = new Projection(dateHeureDebut, selectedSalle, _film);
                    await _projectionService.AddProjection(newProjection);
                    MessageBox.Show("La projection a été ajoutée avec succès.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);              
                } else
                {
                    MessageBox.Show("Veuillez sélectionner une date, une heure et une salle pour la projection.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la projection : {ex.Message}", "Erreur");
            }

        }
    }
}
