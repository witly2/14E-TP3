using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Repositories.Employes;
using CineQuebec.Windows.DAL.Repositories.Films;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services.Employes
{
    public class EmployeService
    {
        private readonly IEmployeRepository _employeRepository;

        public EmployeService(IEmployeRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        public async Task AddEmploye(Employe employe)
        {
            try
            {
                await _employeRepository.AddEmploye(employe);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Une erreur s'est produite lors de l'ajout de l'employe : " + ex.Message);
            }
        }
    }
}
