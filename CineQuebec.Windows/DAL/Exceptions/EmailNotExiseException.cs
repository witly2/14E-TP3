namespace CineQuebec.Windows.DAL.Exceptions;

public class EmailNotExiseException : Exception
{
    public EmailNotExiseException(string message) : base(message)
    {
    }
}