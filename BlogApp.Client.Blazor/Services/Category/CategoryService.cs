using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;
using BlogApp.Client.Blazor.SharedKernel.Services;
using BlogApp.Client.Blazor.Utils;
using Microsoft.AspNetCore.Identity.Data;

namespace BlogApp.Client.Blazor.Services.Category;

public class CategoryService(IHttpClientService httpClientService) : ICategoryService
{
    public async Task<PaginationListResponse<CategoryModel>> GetCategoryPaginationListAsync(DataGridRequest dataGridRequest)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(dataGridRequest));
        var response = await httpClientService
            .PostAsync<PaginationListResponse<CategoryModel>>($"category/GetPaginatedList", stringContent);

        return response;
    }
}
