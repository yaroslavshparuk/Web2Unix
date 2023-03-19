namespace Web2Unix.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException(string message = "Credentials are invalid") : base(message)
    {
    }
}
