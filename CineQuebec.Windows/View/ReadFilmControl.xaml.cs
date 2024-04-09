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
    public partial class ReadFilmControl: UserControl
    {
        private readonly IFilmService _filmService;
        public ReadFilmControl()
        {
            InitializeComponent();
        }

        public void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ajout projection bientôt disponible.");
        }
    }
}
