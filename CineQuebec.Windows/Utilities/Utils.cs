using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace CineQuebec.Windows.Utilities;

public static class Utils
{
    /// <summary>
    ///     Permet de générer le SALT pour un nouvel utilisateur
    /// </summary>
    /// <returns>Un SALT</returns>
    public static byte[] CreerSALT()
    {
        var salt = new byte[16]; // Taille du sel (16 octets pour SHA256 par exemple)
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        return salt;
    }

    /// <summary>
    ///     Combine un mot de passe clair + un salt et retourne une concaténation hachée du mot de passe
    ///     Cet appel est lourd, car il s'exécute en multi threads (Bloc mémoire de 1 Go)
    /// </summary>
    /// <param name="password">Mot de passe clair d'un utilisateur</param>
    /// <param name="salt">SALT d'un utilisateur</param>
    /// <returns>Hash du mot de passe (mot de passe haché)</returns>
    public static byte[] HacherMotDePasse(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 1024
        };

        return argon2.GetBytes(16);
    }


    /// <summary>
    ///     Reteste le hachage de la concaténation d'un mot de passe clair + SALT et compare avec une version hachée fournie
    ///     Si les 2 correspondent, c'est que le mot de passe clair + SALT correspondent à la version hachée fournie,
    ///     donc le mot de passe correspond (true) ou pas (false)
    /// </summary>
    /// <param name="password">Mot de passe en clair</param>
    /// <param name="salt">SALT</param>
    /// <param name="motDePasseEncrypte">Combinaison encryptée du mot de passe combiné + SALT</param>
    /// <returns></returns>
    public static bool EstMotDePasseCorrespond(string password, byte[] salt, byte[] motDePasseHache)
    {
        return motDePasseHache.SequenceEqual(HacherMotDePasse(password, salt));
    }
}