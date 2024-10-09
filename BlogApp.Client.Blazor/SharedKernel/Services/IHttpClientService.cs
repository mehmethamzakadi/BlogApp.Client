using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.SharedKernel.Services;

public interface IHttpClientService
{
    Task<T> GetAsync<T>(string endpoint);
    Task<T> PostAsync<T>(string endpoint, HttpContent content);
    Task<T> PutAsync<T>(string endpoint, HttpContent content);
    Task<T> DeleteAsync<T>(string endpoint);
    void CancelPendingRequests();
}
