namespace Web2Unix.Domain.Exceptions;
public class CreationException : Exception
{
    public CreationException(string message = "Creation of some instanse failed") : base(message)
    {
    }
}