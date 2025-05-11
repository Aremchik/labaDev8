using Microsoft.EntityFrameworkCore;

namespace CSharpService.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<UserData> UserData { get; set; } = null!;
}