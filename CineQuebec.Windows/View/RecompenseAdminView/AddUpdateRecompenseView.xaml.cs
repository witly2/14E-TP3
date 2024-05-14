using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View
{
    public partial class AddUpdateRecompenseView: UserControl
    {
        private readonly AddUpdateRecompenseViewModel _viewModel;
        public AddUpdateRecompenseView(IRecompenseService recompenseService, IFilmService filmService)
        {
            InitializeComponent();
            _viewModel = new AddUpdateRecompenseViewModel(recompenseService, filmService);
            DataContext = _viewModel;
        }

        private void VoirProjection_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.VoirProjection();
        }

        private void AddUpdateRecompense_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddUpdateRecompense();
        }
    }
}
