using System.ComponentModel.DataAnnotations;

namespace BlogApp.Client.Blazor.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [MinLength(2, ErrorMessage = "Kategori adı en az 2 karakter olmalıdır.")]
    [MaxLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olmalıdır.")]
    public string Name { get; set; }
}