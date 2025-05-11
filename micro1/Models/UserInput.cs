using System.ComponentModel.DataAnnotations;

public class UserInput
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; } // Добавлен nullable-модификатор

    [Required]
    [EmailAddress]
    public string? Email { get; set; } // Добавлен nullable-модификатор
}