using BlogApp.Client.Blazor.Models.Auth;
using BlogApp.Client.Blazor.SharedKernel.Models;
using BlogApp.Client.Blazor.Utils;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = BlogApp.Client.Blazor.Models.Auth.LoginRequest;

namespace BlogApp.Client.Blazor.Services.Auth;

public class AuthService(HttpClient httpClient, ProtectedLocalStorage localStorageService) : IAuthService
{
    public async Task<Result<string>> CreateUser(RegisterRequest registerRequest)
    {
        var response = await httpClient
            .PostAsync("user", JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(registerRequest)));

        //Read Response
        if (!response.IsSuccessStatusCode)
            return new Result<string> { Data = null, Success = false, Message = "Bir hata oluştu. Tekrar deneyiniz." };

        var apiResponse = await response.Content.ReadAsStringAsync();

        return JsonUtils.DeserializeJsonString<Result<string>>(apiResponse);
    }

    public async Task<Result<LoginResponse>> LoginUser(LoginRequest loginRequest)
    {
        var response = await httpClient
            .PostAsync("auth/login", JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(loginRequest)));

        //Read Response
        if (!response.IsSuccessStatusCode)
            return new Result<LoginResponse> { Data = null, Success = false, Message = "Bir hata oluştu. Tekrar deneyiniz." };

        var apiResponse = await response.Content.ReadAsStringAsync();

        var loginResponse = JsonUtils.DeserializeJsonString<Result<LoginResponse>>(apiResponse);
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
