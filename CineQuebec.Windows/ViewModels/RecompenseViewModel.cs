using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.ViewModels
{
    public class RecompenseViewModel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IRecompenseService _recompenseService;
        private readonly IFilmService _filmService;
        private ObservableCollection<Recompense> _recompenses;
        private int _nombrePlaceRestante;

        public RecompenseViewModel(IRecompenseService recompenseService, IFilmService filmService)
        {
            _recompenseService = recompenseService;
            _filmService = filmService;
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
                    OnPropertyChanged(nameof(Recompenses));
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
