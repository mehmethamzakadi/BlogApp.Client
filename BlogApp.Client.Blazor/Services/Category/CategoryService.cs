using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;
using BlogApp.Client.Blazor.SharedKernel.Services;

namespace BlogApp.Client.Blazor.Services.Category;

public class CategoryService(IHttpClientService httpClientService) : ICategoryService
{
    public async Task<PaginationListResponse<CategoryModel>> GetCategoryPaginationListAsync(PageRequest pageRequest)
    {
        var response = await httpClientService
            .GetAsync<PaginationListResponse<CategoryModel>>($"category?pageIndex{pageRequest.PageIndex}&pageSize={pageRequest.PageSize}");

        return response;
    }
}
