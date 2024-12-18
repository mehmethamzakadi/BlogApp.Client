using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.Services.Category;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.States.Category;

public class CategoryState : BaseState
{
    private readonly ICategoryService _categoryService;
    public Result<CategoryModel> ResultCategory { get; private set; } = new();

    public CategoryModel CurrentCategory { get; private set; } = new();
    public IEnumerable<CategoryModel> Categories { get; private set; } = Array.Empty<CategoryModel>();
    public int TotalCount { get; private set; }

    public CategoryState(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public void SetCurrentCategory(CategoryModel category)
    {
        CurrentCategory = new CategoryModel
        {
            Id = category.Id,
            Name = category.Name
        };
        NotifyStateChanged();
    }

    public void ResetCurrentCategory()
    {
        CurrentCategory = new CategoryModel();
        NotifyStateChanged();
    }

    public async Task LoadCategoriesAsync(DataGridRequest request)
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            var response = await _categoryService.GetCategoryPaginationListAsync(request);
            Categories = response.Items;
            TotalCount = response.Count;
        });
    }

    public async Task SaveCategoryAsync()
    {
        await ExecuteWithSubmittingAsync(async () =>
        {
            if (CurrentCategory.Id == 0)
            {
                ResultCategory = await _categoryService.CreateCategoryAsync(CurrentCategory);
            }
            else
            {
                ResultCategory = await _categoryService.UpdateCategoryAsync(CurrentCategory);
            }
        });
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await ExecuteWithSubmittingAsync(async () =>
        {
            ResultCategory = await _categoryService.DeleteCategoryAsync(categoryId);
        });
    }
}