using BlogApp.Client.Blazor.Models.Auth;
using BlogApp.Client.Blazor.Models.Common;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = BlogApp.Client.Blazor.Models.Auth.LoginRequest;

namespace BlogApp.Client.Blazor.Services.Auth;

public interface IAuthService
{
    Task<Result<string>> CreateUser(RegisterRequest registerRequest);
    Task<Result<LoginResponse>> LoginUser(LoginRequest loginRequest);
}
