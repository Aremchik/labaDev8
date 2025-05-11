using System.ComponentModel.DataAnnotations;

public class UserData
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; } // Добавлен nullable-модификатор
    public string? Email { get; set; } // Добавлен nullable-модификатор
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}