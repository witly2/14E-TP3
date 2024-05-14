using CineQuebec.Windows.BLL.Services.Recompenses;
using CineQuebec.Windows.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.BLL.Services.Preferences;
using CineQuebec.Windows.BLL.Services.Reservations;
using System.Net.Sockets;
using System.Windows.Controls;
using CineQuebec.Windows.View;
using MongoDB.Bson;
using static MaterialDesignThemes.Wpf.Theme;
using System.Windows;

namespace CineQuebec.Windows.ViewModels
{
    public class OffrirRecompenseViewModel : NotifyPropertyChangeBase
    {
        private readonly IPreferenceService _preferenceService;
        private readonly IReservationService _reservationService;
        private readonly IRecompenseService _recompenseService;
        private Recompense _recompense;
        private int _nombreReservation;

        public List<Abonne> Abonnes { get; private set; }

        public int NombreReservation
        {
            get { return _nombreReservation; }
            set
            {
                _nombreReservation = value;
                OnPropertyChanged(nameof(NombreReservation));
            }
        }
        public OffrirRecompenseViewModel(IReservationService reservationService, IPreferenceService preferenceService, IRecompenseService recompenseService, Recompense recompense)
        {
            _reservationService = reservationService;
            _preferenceService = preferenceService;
            _recompenseService = recompenseService;
            _recompense = recompense;
            LoadAbonnes();
        }

        private async Task LoadAbonnes()
        {
            var categories = _recompense.Projection.Film.ListCategorie;
            var acteurs = _recompense.Projection.Film.ListActeur;
            var realisateurs = _recompense.Projection.Film.ListRealisateur;
            
            Abonnes = new List<Abonne>();
            if(_recompense.Type == TypeRecompense.Ticket)
            {
                foreach (var categorie in categories)
                {
                    var abonnes = _preferenceService.GetAbonnesWithThisPreferenceCategorie(categorie);
                    Abonnes.AddRange(abonnes);
                }
            }
            else
            {
                foreach (var realisateur in realisateurs)
                {
                    var abonnes = _preferenceService.GetAbonnesWithThisPreferenceRealisateur(realisateur);
                    Abonnes.AddRange(abonnes);
                }
                foreach (var acteur in acteurs)
                {
                    var abonnes = _preferenceService.GetAbonnesWithThisPreferenceActeur(acteur);
                    Abonnes.AddRange(abonnes);
                }
            }
            
            foreach (var abonne in Abonnes)
            {
                var nombreReservation = await _reservationService.GetReservationCountByAbonneId(abonne);
                abonne.NombreReservation = nombreReservation.Count;
            }
            Abonnes = Abonnes.OrderByDescending(a => a.NombreReservation).ToList();
        }
    }
}
