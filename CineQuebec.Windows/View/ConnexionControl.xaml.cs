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

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour ConnexionControl.xaml
    /// </summary>
    public partial class ConnexionControl : UserControl
    {
        public ConnexionControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            

            // Créer une nouvelle instance de la fenêtre AdminHomeControl
            NavWindows navWindows = new NavWindows();

            // Afficher la nouvelle fenêtre
            navWindows.Show();
            ((MainWindow)Application.Current.MainWindow).Close();
        }


        private void Afficher_form_inscription(object sender, MouseButtonEventArgs e)
        {
            // Code pour afficher le formulaire d'inscription
            ((MainWindow)Application.Current.MainWindow).InscriptionControl();
        }

    }
}
