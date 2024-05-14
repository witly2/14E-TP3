using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.UsersControls
{
    public partial class ReadFilmControl: UserControl
    {
        private readonly IFilmService _filmService;
        private readonly IProjectionService _projectionService;
        private readonly Film _film;
        public ReadFilmControl(Film film)
        {
            InitializeComponent();
            _film = film;
        }

        public void ToggleButton_AddProjection_Click(object sender, RoutedEventArgs e)
        {
            AddProjectionControl addProjectionControl = new AddProjectionControl(_projectionService, _film);
            this.Content = addProjectionControl;
        }
    }
}
