using BlogApp.Client.Blazor.SharedKernel.Models;
using Microsoft.Extensions.Caching.Memory;
using Radzen;
using Radzen.Blazor;
using System.Collections.Concurrent;

namespace BlogApp.Client.Blazor.Services.DataGrid;

public class FilterService : IFilterService
{
    private readonly IMemoryCache _cache;
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public FilterService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public DataGridRequest CreateDataGridRequest(LoadDataArgs args, int pageSize = 10)
    {
        var cacheKey = GetCacheKey(args);
        var lockKey = $"lock_{cacheKey}";

        var semaphore = _locks.GetOrAdd(lockKey, new SemaphoreSlim(1, 1));

        try
        {
            semaphore.Wait();

            if (_cache.TryGetValue<DataGridRequest>(cacheKey, out var cachedRequest))
            {
                return cachedRequest;
            }

            var request = new DataGridRequest
            {
                PaginatedRequest = new PaginatedRequest
                {
                    PageIndex = 0,
                    PageSize = pageSize
                },
                DynamicQuery = new DynamicQuery
                {
                    Filter = CreateFilter(args),
                    Sort = CreateSorts(args)
                }
            };

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _cache.Set(cacheKey, request, cacheOptions);

            return request;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private string GetCacheKey(LoadDataArgs args)
    {
        var filterKey = string.Join("_", args.Filters.Select(f => $"{f.Property}:{f.FilterValue}:{f.FilterOperator}:{f.LogicalFilterOperator}"));
        var sortKey = string.Join("_", args.Sorts.Select(s => $"{s.Property}:{s.SortOrder}"));
        return $"DataGridRequest_{filterKey}_{sortKey}";
    }

    private Filter CreateFilter(LoadDataArgs args)
    {
        if (!args.Filters.Any())
        {
            return null;
        }

        var baseFilter = new Filter { Filters = new List<Filter>() };

        foreach (var argFilter in args.Filters)
        {
            var filter = new Filter
            {
                Field = argFilter.Property,
                Value = argFilter.FilterValue?.ToString()?.ToLower(),
                Operator = argFilter.FilterOperator.GetDisplayDescription(),
                Logic = argFilter.LogicalFilterOperator.GetDisplayDescription(),
                Filters = new List<Filter>()
            };

            if (string.IsNullOrEmpty(baseFilter.Value))
            {
                baseFilter = filter;
            }
            else if (!(baseFilter.Field == argFilter.Property) && !baseFilter.Filters.Any(x => x.Field == argFilter.Property))
            {
                baseFilter.Filters.Add(filter);
            }
        }

        return baseFilter.Value != null ? baseFilter : null;
    }

    private List<Sort> CreateSorts(LoadDataArgs args)
    {
        if (!args.Sorts.Any())
        {
            return null;
        }

        return args.Sorts.Select(sort => new Sort
        {
            Field = sort.Property,
            Dir = sort.SortOrder.Value.ToString()
        }).ToList();
    }
}