using System.Windows.Input;
using CineQuebec.Windows.Utilities;
using Page_Navigation_App.Utilities;

namespace CineQuebec.Windows.ViewModels;

internal class NavigationViewModel : ViewModelBase
{
    private object _currentView;


    public NavigationViewModel()
    {
        UsersCommand = new RelayCommand(Users);
        HomeAdminCommand = new RelayCommand(HomeAdmin);

        // Startup Page
        CurrentView = new AdminHomeViewModel();
    }

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public ICommand UsersCommand { get; set; }
    public ICommand HomeAdminCommand { get; set; }


    private void Users(object obj)
    {
        CurrentView = new UsersViewModel();
    }

    private void HomeAdmin(object obj)
    {
        CurrentView = new AdminHomeViewModel();
    }
}