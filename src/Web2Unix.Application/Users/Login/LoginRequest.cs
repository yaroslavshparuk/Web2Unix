namespace Web2Unix.Application.Users.Login;

//public record LoginRequest(string username, string password);

public class LoginRequest
{
    public string username { get; set; }
    public string password { get; set; }
}