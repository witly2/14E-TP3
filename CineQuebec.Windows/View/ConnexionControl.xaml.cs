using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string erreurMessage ;
        private Abonne newAbonne;
        private readonly AbonneService _abonneService;
        public ConnexionControl()
        {
            InitializeComponent();
            newAbonne = new Abonne();
            DatabaseGestion db = new DatabaseGestion();
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            this.DataContext = newAbonne;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            

            // Créer une nouvelle instance de la fenêtre AdminHomeControl
            NavWindows navWindows = new NavWindows();

            // Afficher la nouvelle fenêtre
            if (ValidateForm())
            {
                var existeAbonne = await _abonneService.GetAbonneByEmail(newAbonne.Email) as Abonne;
                if (existeAbonne != null && Utils.EstMotDePasseCorrespond(txtMdP.Password.Trim(), existeAbonne.Salt,
                       existeAbonne.Password))
                {
                    navWindows.Show();
                    ((MainWindow)Application.Current.MainWindow).Close();
                   
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe est incorrect");
                }

               
            }
            else
            {
                MessageBox.Show(erreurMessage);
            }
        }


        private void Afficher_form_inscription(object sender, MouseButtonEventArgs e)
        {
            // Code pour afficher le formulaire d'inscription
            ((MainWindow)Application.Current.MainWindow).InscriptionControl();
        }

        private bool ValidateForm()
        {
            erreurMessage = "";
            string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            Regex rgxMail = new Regex(pattern);

            if (newAbonne.Email == null || newAbonne.Email == "" || txtMdP.Password == "" || txtMdP.Password == "")
            {
                erreurMessage += "veuiller remplir tous les champs\n";
            }
            else
            {
                if (!rgxMail.IsMatch(newAbonne.Email))
                {
                    erreurMessage += "le format du couriel c'est test@example.com\n";
                }

            }


            if (erreurMessage.Trim() != "")
            {
                return false;
            }
            else
            {
                return true;
            }



        }

    }
}
