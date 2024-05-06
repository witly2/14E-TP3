using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Abonnes;
using CineQuebec.Windows.Utilities;

namespace CineQuebec.Windows.View;

/// <summary>
///     Logique d'interaction pour ConnexionControl.xaml
/// </summary>
public partial class ConnexionControl : UserControl
{
    private readonly AbonneService _abonneService;

    private string erreurMessage;
    private readonly Abonne newAbonne;

    public ConnexionControl()
    {
        InitializeComponent();
        newAbonne = new Abonne();
        var db = new DatabaseGestion();
        _abonneService = new AbonneService(new AbonneRepsitory(db));
        DataContext = newAbonne;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (ValidateForm())
        {
            var existeAbonne = await _abonneService.GetAbonneByEmail(newAbonne.Email);

            if (existeAbonne != null && Utils.EstMotDePasseCorrespond(txtMdP.Password.Trim(), existeAbonne.Salt,
                    existeAbonne.Password))
            {
                var navWindows = new AdminHomeWindows(existeAbonne);

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
        ((MainWindow)Application.Current.MainWindow).InscriptionControl();
    }

    private bool ValidateForm()
    {
        erreurMessage = "";
        // Le motif regex ci-dessous correspond à une adresse courriel valide.
        var pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
        var rgxMail = new Regex(pattern);

        if (newAbonne.Email == null || newAbonne.Email == "" || txtMdP.Password == "" || txtMdP.Password == "")
        {
            erreurMessage += "Veuiller remplir tous les champs\n. ";
        }
        else
        {
            if (!rgxMail.IsMatch(newAbonne.Email)) erreurMessage += "Le format du courriel c'est test@example.com\n. ";
        }

        if (erreurMessage.Trim() != "")
            return false;
        return true;
    }
}