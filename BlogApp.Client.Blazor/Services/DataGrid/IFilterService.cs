using BlogApp.Client.Blazor.SharedKernel.Models;
using Radzen;

namespace BlogApp.Client.Blazor.Services.DataGrid;

public interface IFilterService
{
    DataGridRequest CreateDataGridRequest(LoadDataArgs args, int pageSize = 10);
}