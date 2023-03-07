using crud3.Data;
using crud3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud3.Controllers;

public class UsersController : Controller
{
  private readonly DataContext _dataContext;

  public UsersController(DataContext dataContext)
  {
    _dataContext = dataContext;
  }

  public async Task<IActionResult> Index()
  {
    if (_dataContext.Users == null)
    {
      return Problem("No users");
    }
    var users = from n in _dataContext.Users select n;
    return View(await users.ToListAsync());
  }

  public IActionResult Create()
  {
    return View();
  }
  
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("Id, Name, Email")] User user)
  {
    if (ModelState.IsValid)
    {
      _dataContext.Add(user);
      await _dataContext.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(user);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var user = await _dataContext.Users.FirstOrDefaultAsync( m => m.Id == id);

    if (user == null)
    {
      return NotFound();
    }
    return View(user);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit([Bind("Id, Name, Email")] User user)
  {
    if (ModelState.IsValid)
    {
      try
      {
        _dataContext.Update(user);
        await _dataContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(user.Id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return RedirectToAction(nameof(Index));
    }
    return View(user);
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var user = await _dataContext.Users.FindAsync(id);
    
    if (user == null)
    {
      return NotFound();
    }
    
    return View(user);
  }

  public bool UserExists(int id)
  {
    return _dataContext.Users.Any(user => user.Id == id);
  }
  
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var user = await _dataContext.Users.FirstOrDefaultAsync(m => m.Id == id);

    if (user == null)
    {
      return NotFound();
    }
    return View(user);
  }

  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var user = await _dataContext.Users.FindAsync(id); 
    _dataContext.Users.Remove(user);
    await _dataContext.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

  public string Update()
  {
    return "";
  }
}