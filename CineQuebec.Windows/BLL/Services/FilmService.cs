using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineQuebec.Windows.DAL.Repositories;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.BLL.Services
{
    public class FilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public List<Film> GetFilms()
        {
            return _filmRepository.GetFilms();
        }
    }
}
