using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.Services.User;

public interface IUserService
{
    Task<PaginationListResponse<UserModel>> GetUserPaginationListAsync(DataGridRequest dataGridRequest);
    Task<Result<UserModel>> CreateUserAsync(UserModel user);
    Task<Result<UserModel>> UpdateUserAsync(UserModel user);
    Task<Result<UserModel>> DeleteUserAsync(int userId);
}