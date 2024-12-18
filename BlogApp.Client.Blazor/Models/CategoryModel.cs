using System.ComponentModel.DataAnnotations;

namespace BlogApp.Client.Blazor.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [MinLength(5, ErrorMessage = "Kategori adı en az 5 karakter olmalıdır.")]
    [MaxLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olmalıdır.")]
    public string Name { get; set; }
}