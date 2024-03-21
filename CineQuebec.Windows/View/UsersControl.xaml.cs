using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
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

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour UsersControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {

        private DatabaseGestion db;
        //public List<Abonne> Abonnes;
        public UsersControl()
        {
            InitializeComponent();
             db = new DatabaseGestion();

            //_abonnes = new List<Abonne>();
            List<Abonne> Abonnes = db.ReadAbonnes();

            this. DataContext = Abonnes;
        }
    }
}
