using BlogApp.Client.Blazor.Models.Common;

namespace BlogApp.Client.Blazor.Services.Common;

public interface IHttpClientService
{
    Task<Result<T>> GetAsync<T>(string endpoint);
    Task<Result<T>> PostAsync<T>(string endpoint, HttpContent content);
    Task<Result<T>> PutAsync<T>(string endpoint, HttpContent content);
    Task<Result<T>> DeleteAsync<T>(string endpoint);
    void CancelPendingRequests();
}
