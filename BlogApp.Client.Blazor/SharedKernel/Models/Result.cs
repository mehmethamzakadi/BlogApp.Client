namespace BlogApp.Client.Blazor.SharedKernel.Models;

public class Result<T>
{
    public T Data { get; set; }

    // İşlemin başarılı olup olmadığını belirten alan
    public bool Success { get; set; }

    // İşlemle ilgili mesaj
    public string Message { get; set; }
}
