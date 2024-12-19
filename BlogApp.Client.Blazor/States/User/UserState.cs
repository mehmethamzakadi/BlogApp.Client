using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.Services.User;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.States.User;

public class UserState : BaseState
{
    private readonly IUserService _UserService;
    public Result<UserModel> ResultUser { get; private set; } = new();

    public UserModel CurrentUser { get; private set; } = new();
    public IEnumerable<UserModel> Users { get; private set; } = Array.Empty<UserModel>();
    public int TotalCount { get; private set; }

    public UserState(IUserService UserService)
    {
        _UserService = UserService;
    }

    public void SetCurrentUser(UserModel User)
    {
        CurrentUser = new UserModel
        {
            Id = User.Id,
            UserName = User.UserName,
            Email = User.Email,
        };
        NotifyStateChanged();
    }

    public void ResetCurrentUser()
    {
        CurrentUser = new UserModel();
        NotifyStateChanged();
    }

    public async Task LoadUserAsync(DataGridRequest request)
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            var response = await _UserService.GetUserPaginationListAsync(request);
            Users = response.Items;
            TotalCount = response.Count;
        });
    }

    public async Task SaveUserAsync()
    {
        await ExecuteWithSubmittingAsync(async () =>
        {
            if (CurrentUser.Id == 0)
            {
                ResultUser = await _UserService.CreateUserAsync(CurrentUser);
            }
            else
            {
                ResultUser = await _UserService.UpdateUserAsync(CurrentUser);
            }
        });
    }

    public async Task DeleteUserAsync(int UserId)
    {
        await ExecuteWithSubmittingAsync(async () =>
        {
            ResultUser = await _UserService.DeleteUserAsync(UserId);
        });
    }
}