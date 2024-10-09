using BlogApp.Client.Blazor.Models.Auth;
using BlogApp.Client.Blazor.SharedKernel.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlogApp.Client.Blazor.SharedKernel.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorageService;

    public HttpClientService(HttpClient httpClient, ProtectedLocalStorage localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    private async Task SetAuthorizationHeader()
    {
        var sessionState = await _localStorageService.GetAsync<LoginResponse>("sessionState");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.Value.Token);

    }

    private async Task<T> GetResponse<T>(HttpResponseMessage httpResponse)
    {
        if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new Exception("İşlem Yapmaya Yetkiniz Yoktur.");
        }
        else if (httpResponse.StatusCode == HttpStatusCode.BadRequest || httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var json = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return response;
        }
        else
        {
            httpResponse.EnsureSuccessStatusCode();
        }
        throw new Exception("Bir hata oluştu.");
    }

    // Generic GET isteği yapma metodu
    public async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.GetAsync(endpoint);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    // Generic POST isteği yapma metodu
    public async Task<T> PostAsync<T>(string endpoint, HttpContent content)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.PostAsync(endpoint, content);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

    }

    // Generic PUT isteği yapma metodu
    public async Task<T> PutAsync<T>(string endpoint, HttpContent content)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.PutAsync(endpoint, content);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

    }

    // Generic DELETE isteği yapma metodu
    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.DeleteAsync(endpoint);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

    }

    // İstekleri iptal etmek için
    public void CancelPendingRequests()
    {
        _httpClient.CancelPendingRequests();
    }
}
