using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.UsersControls
{
    /// <summary>
    /// Logique d'interaction pour AdminHomeControl.xaml
    /// </summary>
    public partial class AdminHomeControl : UserControl
    {
        private readonly FilmService _filmService;
        public AdminHomeControl(Admin admin)
        {
            DatabaseGestion db = new DatabaseGestion();
            this.DataContext = admin;
           
            InitializeComponent();
        }

       
    }
}
