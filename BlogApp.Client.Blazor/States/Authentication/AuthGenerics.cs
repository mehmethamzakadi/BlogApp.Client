using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BlogApp.Client.Blazor.States.Authentication;

public static class AuthGenerics
{
    public static ClaimsPrincipal SetClaimPrincipal(UserSession model)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>
            {
                new("Name", model.Name!),
                new("Email", model.Email!),
                new("Roles", model.Role!),
            }, "JwtAuth"));
    }

    public static UserSession GetClaimsFromToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var claims = token.Claims;

        string Name = claims.First(c => c.Type == "Name").Value!;
        string Email = claims.First(c => c.Type == "Email").Value!;
        string Role = claims.First(c => c.Type == "Roles").Value!;

        return new UserSession(Name, Email, Role);
    }

    public static JsonSerializerOptions JsonOptions()
    {
        return new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };
    }

    public static string SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
    public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
    public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
    public static StringContent GenerateStringContent(string serialiazedObj) => new(serialiazedObj, System.Text.Encoding.UTF8, "application/json");

}