using Blazored.LocalStorage;
using BlogApp.Client.Blazor.Models.Common;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace BlogApp.Client.Blazor.Services.Common;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public HttpClientService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    private async Task SetAuthorizationHeader()
    {
        var token = await _localStorageService.GetItemAsStringAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task<Result<T>> GetResponse<T>(HttpResponseMessage httpResponse)
    {
        if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new Result<T> { Success = false, Message = "İşlem Yapmaya Yetkiniz Yoktur." };
        }
        else if (httpResponse.StatusCode == HttpStatusCode.BadRequest || httpResponse.StatusCode == HttpStatusCode.OK)
        {
            var json = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<Result<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return response;
        }
        else
        {
            httpResponse.EnsureSuccessStatusCode();
        }
        return new Result<T>();
    }

    // Generic GET isteği yapma metodu
    public async Task<Result<T>> GetAsync<T>(string endpoint)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.GetAsync(endpoint);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            return new Result<T> { Success = false, Message = ex.Message };
        }
    }

    // Generic POST isteği yapma metodu
    public async Task<Result<T>> PostAsync<T>(string endpoint, HttpContent content)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.PostAsync(endpoint, content);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            return new Result<T> { Success = false, Message = ex.Message };
        }

    }

    // Generic PUT isteği yapma metodu
    public async Task<Result<T>> PutAsync<T>(string endpoint, HttpContent content)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.PutAsync(endpoint, content);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            return new Result<T> { Success = false, Message = ex.Message };
        }

    }

    // Generic DELETE isteği yapma metodu
    public async Task<Result<T>> DeleteAsync<T>(string endpoint)
    {
        try
        {
            await SetAuthorizationHeader();

            var httpResponse = await _httpClient.DeleteAsync(endpoint);
            return await GetResponse<T>(httpResponse);
        }
        catch (Exception ex)
        {
            return new Result<T> { Success = false, Message = ex.Message };
        }

    }

    // İstekleri iptal etmek için
    public void CancelPendingRequests()
    {
        _httpClient.CancelPendingRequests();
    }
}
