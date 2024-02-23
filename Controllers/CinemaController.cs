using eTickets.Context;
using eTickets.Models;
using eTickets.Models.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class CinemaController : Controller
{
    private readonly ICinemaService _service;

    public CinemaController(ICinemaService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Cinema()
    {
        var data = await _service.GetAllAsync();

        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }

        await _service.AddAsync(cinema);
        return RedirectToAction(nameof(Cinema));
    }

    public async Task<IActionResult> Details(int id)
    {
        var cinemaDetails = await _service.GetEntityByIdAsync(id);
        if(cinemaDetails == null)
        {
            return View("NotFound");
        }

        return View(cinemaDetails);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cinemaDetails = await _service.GetEntityByIdAsync(id);
        if (cinemaDetails == null)
        {
            return View("NotFound");
        }
        return View(cinemaDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,[Bind("Logo,Name,Description")] Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }

        await _service.UpdateAsync(id, cinema);
        return RedirectToAction(nameof(Cinema));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cinemaDetails = await _service.GetEntityByIdAsync(id);
        if(cinemaDetails == null)
        {
            return View("NotFound");
        }
        return View(cinemaDetails);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cinemaDetails = await _service.GetEntityByIdAsync(id);
        if (cinemaDetails == null)
        {
            return View("NotFound");
        }

        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Cinema));
    }
}
