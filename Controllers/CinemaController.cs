using eTickets.Data.Services.IServices;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;
[Authorize(Roles = UserRoles.Admin)]
public class CinemaController : Controller
{
    private readonly ICinemaService _service;

    public CinemaController(ICinemaService service)
    {
        _service = service;
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
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
