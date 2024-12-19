using BlogApp.Client.Blazor.SharedKernel.Models;
using Radzen;
using Radzen.Blazor;

namespace BlogApp.Client.Blazor.Helpers;

public static class DataGridHelper
{
    public static async Task<DataGridRequest> SetDataGridRequest(LoadDataArgs args)
    {
        DataGridRequest dataGridRequest = new DataGridRequest
        {
            PaginatedRequest = new PaginatedRequest
            {
                PageIndex = (args.Skip.Value / args.Top.Value),
                PageSize = args.Top.Value,
            },
            DynamicQuery = new DynamicQuery
            {
                Filter = null,
                Sort = null
            }
        };

        await Task.Yield();

        Filter baseFilter = new Filter()
        {
            Filters = new List<Filter>()
        };

        void AddFirsFilter(Filter filter)
        {
            baseFilter = filter;
        }

        if (args.Filters.Any())
        {
            foreach (var argFilt in args.Filters)
            {
                if (string.IsNullOrEmpty(baseFilter.Value))
                {
                    AddFirsFilter(new Filter
                    {
                        Field = argFilt.Property,
                        Value = (argFilt.FilterValue.ToString()).ToLower(),
                        Operator = argFilt.FilterOperator.GetDisplayDescription(),
                        Logic = argFilt.LogicalFilterOperator.GetDisplayDescription(),
                        Filters = new List<Filter>()
                    });
                }
                else
                {
                    if (!(baseFilter.Field == argFilt.Property) && !baseFilter.Filters.Any(x => x.Field == argFilt.Property))
                    {
                        baseFilter.Filters.Add(new Filter
                        {
                            Field = argFilt.Property,
                            Value = (argFilt.FilterValue.ToString()).ToLower(),
                            Operator = argFilt.FilterOperator.GetDisplayDescription(),
                            Logic = argFilt.LogicalFilterOperator.GetDisplayDescription(),
                            Filters = new List<Filter>()
                        });
                    }
                }
            }
        }
        else
        {
            baseFilter = new Filter()
            {
                Filters = new List<Filter>()
            };
        }

        var sorts = new List<Sort>();
        foreach (var sort in args.Sorts)
        {
            sorts.Add(new Sort
            {
                Field = sort.Property,
                Dir = sort.SortOrder.Value.ToString()
            });
        }

        dataGridRequest.DynamicQuery.Filter = baseFilter.Value != null ? baseFilter : null;
        dataGridRequest.DynamicQuery.Sort = sorts.Count > 0 ? sorts : null;

        return dataGridRequest;
    }

    public static DataGridRequest SetDefaultDataGridRequest(int pageIndex = 0, int pageSize = 10)
    {
        return new DataGridRequest
        {
            DynamicQuery = new DynamicQuery { Filter = null, Sort = null },
            PaginatedRequest = new PaginatedRequest { PageIndex = pageIndex, PageSize = pageSize },
        };
    }
}