using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Page_Navigation_App.Utilities;
using System.Windows.Input;
using CineQuebec.Windows.ViewModels;
using CineQuebec.Windows.View;

namespace CineQuebec.Windows.ViewModels
{
    class NavigationVM :Utilities. ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand UsersCommand { get; set; }
        public ICommand HomeAdminCommand { get; set; }
        //public ICommand ProductsCommand { get; set; }
        //public ICommand OrdersCommand { get; set; }
        //public ICommand TransactionsCommand { get; set; }
        //public ICommand ShipmentsCommand { get; set; }
        //public ICommand SettingsCommand { get; set; }

        private void Users(object obj) => CurrentView = new UsersVm();
        private void HomeAdmin(object obj) => CurrentView = new AdminHomVm();
        //private void Product(object obj) => CurrentView = new ProductVM();
        //private void Order(object obj) => CurrentView = new OrderVM();
        //private void Transaction(object obj) => CurrentView = new TransactionVM();
        //private void Shipment(object obj) => CurrentView = new ShipmentVM();
        //private void Setting(object obj) => CurrentView = new SettingVM();

        public NavigationVM()
        {
            UsersCommand = new RelayCommand(Users);
            HomeAdminCommand = new RelayCommand(HomeAdmin);
          
            // Startup Page
            CurrentView = new AdminHomVm();
        }
    }
}
