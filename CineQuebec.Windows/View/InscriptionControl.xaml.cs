using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.View;

/// <summary>
///     Logique d'interaction pour InscriptionControl1.xaml
/// </summary>
public partial class InscriptionControl1 : UserControl
{
    private readonly AbonneService _abonneService;
    private string erreurMessage;

    private readonly Abonne newAbonne;

    public InscriptionControl1(Abonne abonne = null)
    {
        InitializeComponent();
        if (abonne != null) newAbonne = abonne;

        newAbonne = new Abonne();
        var db = new DatabaseGestion();
        _abonneService = new AbonneService(new AbonneRepsitory(db));

        DataContext = newAbonne;
    }

    private void Afficher_form_login(object sender, MouseButtonEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).ConnexionControl();
    }


    private void btnCreerCompte_Click(object sender, RoutedEventArgs e)
    {
        var nom = newAbonne.Username;

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
                ((MainWindow)Application.Current.MainWindow).ConnexionControl();
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
        var pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
        var rgxMail = new Regex(pattern);

        if (newAbonne.Username == null || newAbonne.Username == "" || newAbonne.Email == null ||
            newAbonne.Email == "" || txtMdP.Password == "" || txtMdP.Password == "")
        {
            erreurMessage += "Veuiller remplir tous les champs\n. ";
        }
        else
        {
            if (!rgxMail.IsMatch(newAbonne.Email)) erreurMessage += "Le format du courriel c'est test@example.com\n. ";

            if (txtMdP.Password != txtCMdP.Password) erreurMessage += "Les mot de passe doivent être identique\n. ";
        }

        if (erreurMessage.Trim() != "")
            return false;
        return true;
    }
}