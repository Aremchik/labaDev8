using System.ComponentModel.DataAnnotations;

public class UserData
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}