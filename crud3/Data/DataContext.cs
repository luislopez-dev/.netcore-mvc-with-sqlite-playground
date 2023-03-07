using crud3.Models;
using Microsoft.EntityFrameworkCore;

namespace crud3.Data;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }
  public DbSet<User> Users { get; set; }
  public DbSet<Character> Characters { get; set; }
}