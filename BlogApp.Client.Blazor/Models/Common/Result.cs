namespace BlogApp.Client.Blazor.Models.Common;

public class Result<T>
{
    public T Data { get; set; }

    // İşlemin başarılı olup olmadığını belirten alan
    public bool Success { get; set; }

    // İşlemle ilgili mesaj
    public string Message { get; set; }
}