using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.Services.Category;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.States.Category;

public class CategoryState
{
    private readonly ICategoryService _categoryService;
    
    public CategoryModel CurrentCategory { get; private set; } = new();
    public IEnumerable<CategoryModel> Categories { get; private set; } = Array.Empty<CategoryModel>();
    public int TotalCount { get; private set; }
    public bool IsLoading { get; private set; }
    public bool IsSubmitting { get; private set; }
    
    public event Action OnStateChanged;

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
        try
        {
            IsLoading = true;
            NotifyStateChanged();

            var response = await _categoryService.GetCategoryPaginationListAsync(request);
            Categories = response.Items;
            TotalCount = response.Count;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    public async Task SaveCategoryAsync()
    {
        try
        {
            IsSubmitting = true;
            NotifyStateChanged();

            if (CurrentCategory.Id == 0)
            {
                await _categoryService.CreateCategoryAsync(CurrentCategory);
            }
            else
            {
                await _categoryService.UpdateCategoryAsync(CurrentCategory);
            }
        }
        finally
        {
            IsSubmitting = false;
            NotifyStateChanged();
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        try
        {
            IsSubmitting = true;
            NotifyStateChanged();

            await _categoryService.DeleteCategoryAsync(categoryId);
        }
        finally
        {
            IsSubmitting = false;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}