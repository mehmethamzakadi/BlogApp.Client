namespace BlogApp.Client.Blazor.SharedKernel.Models;

public sealed class Sort
{
    public string Field { get; set; }
    public string Dir { get; set; }

    public Sort()
    {
        Field = string.Empty;
        Dir = string.Empty;
    }

    public Sort(string field, string dir)
    {
        Field = field;
        Dir = dir;
    }
}
