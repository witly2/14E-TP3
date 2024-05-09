using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Connexion;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories.Persons;
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
        private readonly IConnexionService _connexionService;
        public ConnexionControl(IConnexionService connexionService)
        {
            InitializeComponent();
            newAbonne = new Abonne();
            DatabaseGestion db = new DatabaseGestion();
            _abonneService = new AbonneService(new AbonneRepsitory(db));
            _connexionService = connexionService;
            this.DataContext = newAbonne;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                /*
                var existeAbonne = await _abonneService.GetAbonneByEmail(newAbonne.Email) as Abonne;

                if (existeAbonne != null && Utils.EstMotDePasseCorrespond(txtMdP.Password.Trim(), existeAbonne.Salt,
                       existeAbonne.Password))
                {
                    NavWindows navWindows = new NavWindows(existeAbonne);

                    navWindows.Show();
                    ((MainWindow)Application.Current.MainWindow).Close();
                   
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe est incorrect");
                }  */
                var personne = await _connexionService.GetPersonByEmail(newAbonne.Email);
                if (personne != null)
                {
                    if(personne is Abonne abonne)
                    {
                        // TODO : Envoyer à page accueil Abonne
                    } 
                    else if (personne is Admin admin)
                    {
                        NavWindows navWindows = new NavWindows(admin);

                        navWindows.Show();
                        ((MainWindow)Application.Current.MainWindow).Close();
                    }
                    else if (personne is Employe employe)
                    {
                        // TODO : Envoyer à page accueil Employe
                    }
                }
            }
            else
            {
                MessageBox.Show(erreurMessage);
            }
        }


        private void Afficher_form_inscription(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).InscriptionControl();
        }

        private bool ValidateForm()
        {
            erreurMessage = "";
            // Le motif regex ci-dessous correspond à une adresse courriel valide.
            string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            Regex rgxMail = new Regex(pattern);

            if (newAbonne.Email == null || newAbonne.Email == "" || txtMdP.Password == "" || txtMdP.Password == "")
            {
                erreurMessage += "Veuiller remplir tous les champs\n. ";
            }
            else
            {
                if (!rgxMail.IsMatch(newAbonne.Email))
                {
                    erreurMessage += "Le format du courriel c'est test@example.com\n. ";
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
