namespace BlogApp.Client.Blazor.Models.Auth;

public class LoginResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime Expiration { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
