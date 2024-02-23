using eTickets.Context;
using eTickets.Models.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _service;
    public MovieController(IMovieService service)
    {
         _service = service;
    }
    public async Task<IActionResult> Movie()
    {
        var data = await _service.GetAllAsync(n => n.Cinema);

        return View(data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _service.GetEntityByIdAsync(id);

        return View(movieDetails);
    }

    public async Task<IActionResult> Create()
    {
        var movieDropdownData = await _service.GetMovieDropdownValues();

        ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");

        return View();
    }
}
