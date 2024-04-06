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
       // private Film film;

        public AddUpdateFilmControl(Film filmToUpdate = null)
        {
            InitializeComponent();
            TextBlock addUpdateFilmButton = (TextBlock)this.FindName("addUpdateButton");
            if (filmToUpdate != null)
            {
                Debug.WriteLine($"Titre FR: {filmToUpdate.FrenchTitle}");
                //film = filmToUpdate;
                addUpdateFilmButton.Text = "Modifier";
                this.DataContext = filmToUpdate;
            }
            else
            {
                addUpdateFilmButton.Text = "Ajouter";
            }
        }
        public void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ajout projection.");
        }

        public void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ajout ou update film.");
        }
    }
}
