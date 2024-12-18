﻿using BlogApp.Client.Blazor.Models;
using BlogApp.Client.Blazor.SharedKernel.Models;
using BlogApp.Client.Blazor.SharedKernel.Services;
using BlogApp.Client.Blazor.Utils;

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

    public async Task<CategoryModel> CreateCategoryAsync(CategoryModel category)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(category));
        var response = await httpClientService
            .PostAsync<CategoryModel>($"category/Create", stringContent);

        return response;
    }

    public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel category)
    {
        var stringContent = JsonUtils.GenerateStringContent(JsonUtils.SerializeObj(category));
        var response = await httpClientService
            .PutAsync<CategoryModel>($"category/Update", stringContent);

        return response;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var response = await httpClientService
            .DeleteAsync<bool>($"category/Delete/{categoryId}");

        return response;
    }
}