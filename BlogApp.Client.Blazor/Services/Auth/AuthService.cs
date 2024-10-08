using BlogApp.Client.Blazor.Models.Auth;
using BlogApp.Client.Blazor.Models.Common;
using BlogApp.Client.Blazor.States.Authentication;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = BlogApp.Client.Blazor.Models.Auth.LoginRequest;

namespace BlogApp.Client.Blazor.Services.Auth;

public class AuthService(HttpClient httpClient, ProtectedLocalStorage localStorageService) : IAuthService
{
    public async Task<Result<string>> CreateUser(RegisterRequest registerRequest)
    {
        var response = await httpClient
                 .PostAsync("user",
                     AuthGenerics.GenerateStringContent(
                     AuthGenerics.SerializeObj(registerRequest)));

        //Read Response
        if (!response.IsSuccessStatusCode)
            return new Result<string> { Data = null, Success = false, Message = "Bir hata oluştu. Tekrar deneyiniz." };

        var apiResponse = await response.Content.ReadAsStringAsync();
        return AuthGenerics.DeserializeJsonString<Result<string>>(apiResponse);
    }

    public async Task<Result<LoginResponse>> LoginUser(LoginRequest loginRequest)
    {
        var response = await httpClient
               .PostAsync("auth/login",
                   AuthGenerics.GenerateStringContent(
                   AuthGenerics.SerializeObj(loginRequest)));

        //Read Response
        if (!response.IsSuccessStatusCode)
            return new Result<LoginResponse> { Data = null, Success = false, Message = "Bir hata oluştu. Tekrar deneyiniz." };

        var apiResponse = await response.Content.ReadAsStringAsync();

        var loginResponse = AuthGenerics.DeserializeJsonString<Result<LoginResponse>>(apiResponse);
        if (loginResponse.Success)
        {
            await localStorageService.SetAsync("sessionState", loginResponse.Data);
        }
        else
        {
            await localStorageService.DeleteAsync("sessionState");
        }

        return loginResponse;
    }
}
