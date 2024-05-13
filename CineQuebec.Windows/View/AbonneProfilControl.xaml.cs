using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class AbonneProfilControl : UserControl
{
   
    public AbonneProfilControl(Abonne abonne)
    {
        DatabaseGestion db = new DatabaseGestion();
        this.DataContext = abonne;
        
           
        InitializeComponent();
    }
}