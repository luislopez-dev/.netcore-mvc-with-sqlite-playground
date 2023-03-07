namespace crud3.Models;

public class Character
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string RpgClass { get; set; }
  public User User { get; set; }
  public int UserId { get; set; }
}