namespace BlogApp.Client.Blazor.SharedKernel.Models;

public class DataGridRequest
{
    public PaginatedRequest PaginatedRequest { get; set; }
    public DynamicQuery? DynamicQuery { get; set; }
}

