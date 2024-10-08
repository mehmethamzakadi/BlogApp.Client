﻿namespace BlogApp.Client.Blazor.Models.Auth;

public sealed class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}