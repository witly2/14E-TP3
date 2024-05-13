using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.DAL.Data;
using System.Collections.ObjectModel;
using System.Windows;

namespace CineQuebec.Windows.ViewModels
{
    public class RecompenseViewModel : NotifyPropertyChangeBase
    {
        private readonly IRecompenseService _recompenseService;
        private ObservableCollection<Recompense> _recompenses;
        private Dictionary<Recompense, int> _placeRestanteDictionary;

        public RecompenseViewModel(IRecompenseService recompenseService)
        {
            _recompenseService = recompenseService;
            _placeRestanteDictionary = new Dictionary<Recompense, int>();
            LoadRecompenses();
        }

        public ObservableCollection<Recompense> Recompenses
        {
            get { return _recompenses; }
            set
            {
                if (_recompenses != value)
                {
                    _recompenses = value;
                    OnPropertyChanged();
                }
            }
        }

        public Dictionary<Recompense, int> PlaceRestanteDictionary
        {
            get { return _placeRestanteDictionary;  }
        }

        private async void LoadRecompenses()
        {
            List<Recompense> recompenses = await _recompenseService.GetAllRecompenses();
            if(recompenses != null)
            {
                Recompenses = new ObservableCollection<Recompense>(recompenses);
                foreach(var recompense in Recompenses)
                {
                    int placeRestante = await _recompenseService.GetCountPlaceRestante(recompense);
                    _placeRestanteDictionary.Add(recompense, placeRestante);
                }
            }
            else
            {
                Recompenses = new ObservableCollection<Recompense>();
            }
        }

        public async void AddRecompense()
        {
            MessageBox.Show("AddRecompense.", "Recompense", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async void EditRecompense()
        {
            MessageBox.Show("EditRecompense.", "Recompense", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
