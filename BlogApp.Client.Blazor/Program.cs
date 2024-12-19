using BlogApp.Client.Blazor.Components;
using BlogApp.Client.Blazor.Services.Auth;
using BlogApp.Client.Blazor.Services.Category;
using BlogApp.Client.Blazor.Services.Common;
using BlogApp.Client.Blazor.Services.DataGrid;
using BlogApp.Client.Blazor.Services.User;
using BlogApp.Client.Blazor.SharedKernel.Services;
using BlogApp.Client.Blazor.States.Auth;
using BlogApp.Client.Blazor.States.Category;
using BlogApp.Client.Blazor.States.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add memory cache
builder.Services.AddMemoryCache();

builder.Services.AddRadzenComponents();

var activator = new RadzenComponentActivator();
activator.Override(typeof(RadzenDataGrid<>), typeof(CustomRadzenDataGrid<>));
builder.Services.AddSingleton<IComponentActivator>(activator);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("BaseApiUrl").Value!) });

// Service registrations
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IExceptionHandlerService, ExceptionHandlerService>();
builder.Services.AddScoped<IFilterService, FilterService>();
builder.Services.AddScoped<CategoryState>();
builder.Services.AddScoped<UserState>();


// Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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