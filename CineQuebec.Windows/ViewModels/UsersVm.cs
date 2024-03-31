using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.ViewModels
{
    public class UsersVm:Utilities.ViewModelBase
    {

        private readonly DatabaseGestion _db;
        private ObservableCollection<Abonne> _abonnes;

        public ObservableCollection<Abonne>? Abonnes
        {
            get { return _abonnes; }
            set
            {
                _abonnes = value;
                OnPropertyChanged();
            }
        }

        public UsersVm()
        {
            _db = new DatabaseGestion();
            Abonnes = new ObservableCollection<Abonne>(_db.ReadAbonnes());
        }
    }
}
