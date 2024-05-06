using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MahApps.Metro.Controls;

namespace CineQuebec.Windows.View;

public partial class AddProjectionControl : UserControl
{
    private readonly Film _film;
    private readonly IProjectionService _projectionService;

    public AddProjectionControl(IProjectionService projectionService, Film film)
    {
        InitializeComponent();
        _projectionService = projectionService;
        _film = film;
        DataContext = film;
        loadSalles();
    }

    public async void loadSalles()
    {
        var salles = await _projectionService.GetSalles();
        var salleComboBox = (ComboBox)FindName("salleComboBox");
        foreach (var salle in salles)
        {
            var comboBoxItem = new ComboBoxItem();
            comboBoxItem.Content = "Salle " + salle.NumeroSalle + " - " + salle.NombrePlace + " places";
            comboBoxItem.Tag = salle;
            salleComboBox.Items.Add(comboBoxItem);
        }
    }

    public async void addProjectionButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var datePicker = (DatePicker)FindName("datePicker");
            var timePicker = (TimePicker)FindName("timePicker");
            var salleComboBox = (ComboBox)FindName("salleComboBox");

            if (datePicker.SelectedDate.HasValue && timePicker.SelectedDateTime.HasValue)
            {
                var selectedDateTime = timePicker.SelectedDateTime.Value;
                var selectedTime = selectedDateTime.TimeOfDay;
                var dateHeureDebut = datePicker.SelectedDate.Value.Date + selectedTime;

                var selectedSalleItem = (ComboBoxItem)salleComboBox.SelectedItem;
                var selectedSalle = selectedSalleItem.Tag as Salle;
                var salleDisponible =
                    await _projectionService.estSalleDisponibleThisDay(selectedSalle,
                        datePicker.SelectedDate.Value.Date);
                if (salleDisponible)
                {
                    var newProjection = new Projection(dateHeureDebut, selectedSalle, _film);
                    await _projectionService.AddProjection(newProjection);
                    MessageBox.Show("La projection a été ajoutée avec succès.", "Validation", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("La salle n'est pas disponible cette journée.", "Validation", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            else
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