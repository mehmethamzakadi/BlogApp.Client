namespace BlogApp.Client.Blazor.Models.Auth;

public class LoginResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Expiration { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
