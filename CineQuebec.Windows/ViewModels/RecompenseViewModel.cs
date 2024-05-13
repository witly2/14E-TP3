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
        private int _nombrePlaceRestante;

        public RecompenseViewModel(IRecompenseService recompenseService)
        {
            _recompenseService = recompenseService;
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

        public int NombrePlaceRestante
        {
            get { return _nombrePlaceRestante; }
            set
            {
                _nombrePlaceRestante = value;
                OnPropertyChanged(nameof(NombrePlaceRestante));
            }
        }

        private async void LoadRecompenses()
        {
            List<Recompense> recompenses = await _recompenseService.GetAllRecompenses();
            if(recompenses != null)
            {
                Recompenses = new ObservableCollection<Recompense>(recompenses);
                foreach(var recompense in Recompenses)
                {
                    int placeRestant = _recompenseService.GetCountPlaceRestante(recompense);
                    if(placeRestant != null)
                    {
                        recompense.SetNombrePlaceRestante(placeRestant);
                    }
                    else
                    {
                        recompense.SetNombrePlaceRestante(0);
                    }
                    
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
