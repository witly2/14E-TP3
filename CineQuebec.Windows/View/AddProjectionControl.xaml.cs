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
           
        }
    }
}
