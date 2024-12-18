using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;

namespace BlogApp.Client.Blazor.Services.Category;

public interface ICategoryService
{
    Task<PaginationListResponse<CategoryModel>> GetCategoryPaginationListAsync(DataGridRequest dataGridRequest);
    Task<Result<CategoryModel>> CreateCategoryAsync(CategoryModel category);
    Task<Result<CategoryModel>> UpdateCategoryAsync(CategoryModel category);
    Task<Result<CategoryModel>> DeleteCategoryAsync(int categoryId);
}