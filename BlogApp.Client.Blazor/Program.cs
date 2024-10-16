using BlogApp.Client.Blazor.Components;
using BlogApp.Client.Blazor.Services.Auth;
using BlogApp.Client.Blazor.Services.Category;
using BlogApp.Client.Blazor.SharedKernel.Services;
using BlogApp.Client.Blazor.States.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using Radzen.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRadzenComponents();

var activator = new RadzenComponentActivator();
// Override any RadzenDataGrid with MyDataGrid which has some property defaults set.
activator.Override(typeof(RadzenDataGrid<>), typeof(CustomRadzenDataGrid<>));
builder.Services.AddSingleton<IComponentActivator>(activator);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();



builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("BaseApiUrl").Value!) });

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
