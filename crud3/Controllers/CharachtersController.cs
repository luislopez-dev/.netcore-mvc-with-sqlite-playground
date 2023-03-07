using crud3.Data;
using crud3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud3.Controllers;

public class CharachtersController : ControllerBase
{
  private DataContext _dataContext;

  public CharachtersController(DataContext dataContext)
  {
    _dataContext = dataContext;
  }

  [HttpGet]
  public async Task<ActionResult<List<Character>>> Index (int userId)
  {
    var charachters = await _dataContext.Characters
      .Where(c => c.UserId == userId)
      .ToListAsync();
    return charachters;
  }
}