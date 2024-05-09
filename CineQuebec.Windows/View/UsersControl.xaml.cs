using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View;

public partial class UsersControl : UserControl
{
    private readonly AbonneService _abonneService;
    public List<Abonne> Abonnes;
    private readonly Dictionary<int, Abonne> abonnesDictionary;
    private readonly DataGrid dataGrid;
    private int rowIndex = 1;

    public UsersControl(AbonneService abonneService)
    {
        InitializeComponent();

        _abonneService = abonneService;
        abonnesDictionary = new Dictionary<int, Abonne>();
        dataGrid = (DataGrid)FindName("dataGridAbonnes");

        GetAbonnes();
        DataGrid();
    }

    private void GetAbonnes()
    {
        Abonnes = _abonneService.GetAbonnes();

        for (var i = 0; i < Abonnes.Count; i++) abonnesDictionary.Add(i + 1, Abonnes[i]);
        DataContext = abonnesDictionary;
    }

    public void DataGrid()
    {
        var clnRang = (DataGridTextColumn)FindName("clnRang");
        var dateColumn = (DataGridTextColumn)FindName("date");
        if (dateColumn != null) dateColumn.Binding.StringFormat = "yyyy-MM-dd";
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPair = (KeyValuePair<int, Abonne>)dataGrid.SelectedItem;
        var selectedKey = selectedPair.Key;

        if (abonnesDictionary.TryGetValue(selectedKey, out var selectedAbonne))
            gridAbonneDetails.DataContext = selectedAbonne;
    }

    private void Button_Edit(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Action indisonible pour l'instant");
    }

    private void Button_delete(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Action indisonible pour l'instant");
    }
}