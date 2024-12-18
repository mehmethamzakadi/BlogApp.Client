using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.Services.Category;

public interface ICategoryService
{
    Task<PaginationListResponse<CategoryModel>> GetCategoryPaginationListAsync(DataGridRequest dataGridRequest);
    Task<CategoryModel> CreateCategoryAsync(CategoryModel category);
    Task<CategoryModel> UpdateCategoryAsync(CategoryModel category);
    Task<bool> DeleteCategoryAsync(int categoryId);
}