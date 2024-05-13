using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services.Recompenses;
using System.Windows;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View.RecompenseAdminView
{
    public partial class RecompenseView : UserControl
    {
        private readonly RecompenseViewModel _viewModel;

        public RecompenseView(IRecompenseService recompenseService)
        {
            InitializeComponent();
            _viewModel = new RecompenseViewModel(recompenseService);
            DataContext = _viewModel;
        }

        private void ToggleButton_AddRecompense_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddRecompense();
        }

        private void ToggleButton_EditRecompense_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EditRecompense();
        }
    }
}
