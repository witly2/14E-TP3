using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Films;
using CineQuebec.Windows.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.View.AbonneView
{
    /// <summary>
    /// Logique d'interaction pour AbonneHomeView.xaml
    /// </summary>
    public partial class AbonneHomeView : UserControl
    {
        private readonly AbonneService _abonneService;
        public AbonneHomeView(Abonne abonne)
        {
            InitializeComponent();
            this.DataContext = abonne;
        }

       
    }
}
