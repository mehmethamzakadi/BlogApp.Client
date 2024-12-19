using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;
using BlogApp.Client.Blazor.SharedKernel.Services;
using BlogApp.Client.Blazor.Utils;

namespace BlogApp.Client.Blazor.Services.User;

public class UserService(IHttpClientService httpClientService) : IUserService
{
    public async Task<PaginationListResponse<UserModel>> GetUserPaginationListAsync(DataGridRequest dataGridRequest)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(dataGridRequest));
        var response = await httpClientService
            .PostAsync<PaginationListResponse<UserModel>>($"User/GetPaginatedList", stringContent);

        return response;
    }

    public async Task<Result<UserModel>> CreateUserAsync(UserModel User)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(User));
        var response = await httpClientService
            .PostAsync<Result<UserModel>>($"User/Create", stringContent);

        return response;
    }

    public async Task<Result<UserModel>> UpdateUserAsync(UserModel User)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(User));
        var response = await httpClientService
            .PutAsync<Result<UserModel>>($"User/Update", stringContent);

        return response;
    }

    public async Task<Result<UserModel>> DeleteUserAsync(int UserId)
    {
        var response = await httpClientService
            .DeleteAsync<Result<UserModel>>($"User/Delete/{UserId}");

        return response;
    }
}