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
using System.Text.RegularExpressions;
using CineQuebec.Windows.Utilities;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Exceptions;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour InscriptionControl1.xaml
    /// </summary>
    public partial class InscriptionControl1 : UserControl
    {

        private Abonne newAbonne;
        private string erreurMessage;
        private readonly AbonneService _abonneService;
        private readonly Film _film;
        public InscriptionControl1(Film film=null, Abonne abonne = null)
        {
            InitializeComponent();
            if(abonne != null)
            {
                newAbonne = abonne;
            }

            newAbonne = new Abonne();
            DatabaseGestion db = new DatabaseGestion();
            _abonneService = new AbonneService(new AbonneRepsitory(db));

            this.DataContext = newAbonne;
            _film = film;
           
        }

        private void Afficher_form_login(object sender, MouseButtonEventArgs e)
        {
            if (_film is not null)
            {
                ((MainWindow)Application.Current.MainWindow).ConnexionControl(_film);
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).ConnexionControl();
            }
            
            
        }

     

        private void btnCreerCompte_Click(object sender, RoutedEventArgs e)
        {
            string nom = newAbonne.Username;

            if (!ValidateForm())
            {
                MessageBox.Show(erreurMessage);
            }
            else
            {
                newAbonne.Salt = Utils.CreerSALT();

                newAbonne.Password = Utils.HacherMotDePasse(txtMdP.Password, newAbonne.Salt);

                try
                {
                    _abonneService.AddAbonne(newAbonne);
                    if (_film is not null)
                    {
                        ((MainWindow)Application.Current.MainWindow).ConnexionControl(_film);
                    }
                    else
                    {
                        ((MainWindow)Application.Current.MainWindow).ConnexionControl();
                    }

                }
                catch (EmailExisteExeption ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool ValidateForm()
        {
            erreurMessage = "";
            // Le motif regex ci-dessous correspond à une adresse courriel valide.
            string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            Regex rgxMail = new Regex(pattern);

            if(newAbonne.Username == null || newAbonne.Username == "" || newAbonne.Email==null || newAbonne.Email == "" || txtMdP.Password=="" || txtMdP.Password == "")
            {
                erreurMessage += "Veuiller remplir tous les champs\n. ";
            }
            else
            {
                if ( !rgxMail.IsMatch(newAbonne.Email))
                {
                    erreurMessage += "Le format du courriel c'est test@example.com\n. ";
                }

                if (txtMdP.Password != txtCMdP.Password)
                {
                    erreurMessage += "Les mot de passe doivent être identique\n. ";
                }
            }

            if (erreurMessage.Trim()!="")
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
