using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.DAL.Repositories.Abonnés;

public interface IAbonneRepsitory
{
    List<Abonne> GetAbonnes();
    Task AddAbonne(Abonne abonne);

    Task<Abonne> GetByEmail(string email);
}