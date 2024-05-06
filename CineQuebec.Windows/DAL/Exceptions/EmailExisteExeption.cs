namespace CineQuebec.Windows.DAL.Exceptions;

public class EmailExisteExeption : Exception
{
    public EmailExisteExeption(string message) : base(message)
    {
    }
}