using System.Windows.Controls;
using System.Windows.Input;
using CineQuebec.Windows.Utilities;
using Page_Navigation_App.Utilities;

namespace CineQuebec.Windows.ViewModels;

public class AccueilViewModel : ViewModelBase
{
    private readonly ContentControl _mainContentControl;

    public AccueilViewModel(ContentControl mainContentControl)
    {
        _mainContentControl = mainContentControl;

        YourCommand = new RelayCommand(ShowHome);
    }


    public ICommand YourCommand { get; }

    private void ShowHome(object obj)
    {
        _mainContentControl.Content = new MainWindow();
    }
}