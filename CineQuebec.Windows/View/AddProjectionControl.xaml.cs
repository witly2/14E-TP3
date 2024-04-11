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
using MahApps.Metro.Controls;
using MongoDB.Bson;

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
           
        }

        public async void addProjectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatePicker datePicker = (DatePicker)this.FindName("datePicker");
                TimePicker timePicker = (TimePicker)this.FindName("timePicker");
                ComboBox salleComboBox = (ComboBox)this.FindName("salleComboBox");
                /*
                if(datePicker.SelectedDate.HasValue && timePicker.SelectedTime.HasValue)
                {
                    DateTime dateHeureDebut = datePicker.SelectedDate.Value.Date + timePicker.SelectedTime;

                    var selectedSalle = (ComboBoxItem)salleComboBox.SelectedItem;

                    Projection newProjection = new Projection(dateHeureDebut, selectedSalle, _film);

                    await _projectionService.AddProjection(newProjection);

                    MessageBox.Show("La projection a été ajoutée avec succès.", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                    // TODO : afficher la page FilmsControl
                } else
                {
                    MessageBox.Show("Veuillez sélectionner une date, une heure et une salle pour la projection.");
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la projection : {ex.Message}", "Erreur");
            }

        }
    }
}
