using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services.Recompenses;
using System.Windows;
using CineQuebec.Windows.ViewModels;

namespace CineQuebec.Windows.View.RecompenseAdminView
{
    public partial class RecompenseView : UserControl
    {
        private readonly RecompenseViewModel _viewModel;
        public RecompenseView(IRecompenseService referenceService)
        {
            InitializeComponent();
            _viewModel = new RecompenseViewModel();
            DataContext = _viewModel;
        }
    }
}
