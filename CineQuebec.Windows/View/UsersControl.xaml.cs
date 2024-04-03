using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour UsersControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {

        private readonly AbonneService _abonneService;
        private Dictionary<int, Abonne> abonnesDictionary;
        public List<Abonne> Abonnes;
        private System.Windows.Controls.DataGrid dataGrid;
         private int rowIndex = 1;
        public UsersControl(AbonneService abonneService)
        {
            InitializeComponent();
           _abonneService = abonneService;
            abonnesDictionary = new Dictionary<int, Abonne>();
            dataGrid = (System.Windows.Controls.DataGrid)this.FindName("dataGridAbonnes");


            //clnRang.Binding.

            GetAbonnes();

            DataGrid();

        }

        private void GetAbonnes()
        {
           Abonnes= _abonneService.GetAbonnes();
            //this.DataContext = Abonnes;

            for (int i = 0; i < Abonnes.Count; i++)
            {
                abonnesDictionary.Add(i + 1, Abonnes[i]);
            }

            // Définissez le DataContext sur le dictionnaire
            this.DataContext = abonnesDictionary;

        }

        public void DataGrid()
        {
            DataGridTextColumn clnRang = (DataGridTextColumn)this.FindName("clnRang");
            DataGridTextColumn dateColumn = (DataGridTextColumn)this.FindName("date");
            if (dateColumn != null)
            {
                

                dateColumn.Binding.StringFormat = "yyyy-MM-dd";
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPair = (KeyValuePair<int, Abonne>)dataGrid.SelectedItem;
            int selectedKey = selectedPair.Key;

            if (abonnesDictionary.TryGetValue(selectedKey, out Abonne selectedAbonne))
            {
                //MessageBox.Show($"Abonne: {selectedAbonne.Username}");
                gridAbonneDetails.DataContext = selectedAbonne;
            }

        }
    }
}
