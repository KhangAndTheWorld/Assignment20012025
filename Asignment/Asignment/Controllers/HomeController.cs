using System.Diagnostics;
using Asignment.Data;
using Microsoft.AspNetCore.Mvc;
using Asignment.Models;

namespace Asignment.Controllers;

public class HomeController : Controller
{
    private readonly AsignmentContext _context;

    public HomeController(AsignmentContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEvent(Event @event)
    {
        if (ModelState.IsValid)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View("Create", @event);
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}