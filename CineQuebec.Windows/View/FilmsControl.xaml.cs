using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View
{
    public partial class FilmsControl: UserControl
    {
        private readonly FilmService _filmService;
        private List<Film> films;

        public FilmsControl(FilmService filmService)
        {
            InitializeComponent();
            _filmService = filmService;
            GetFilms();
        }
        public void GetFilms()
        {
            films = _filmService.GetFilms();
            this.DataContext = films;
        }
    }
}
