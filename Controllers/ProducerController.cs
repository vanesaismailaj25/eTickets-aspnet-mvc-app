﻿using eTickets.Data.Services.IServices;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;
[Authorize(Roles = UserRoles.Admin)]
public class ProducerController : Controller
{
    private readonly IProducerService _service;
    public ProducerController(IProducerService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Producer()
    {
        var data = await _service.GetAllAsync();

        return View(data);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var producerDetails = await _service.GetEntityByIdAsync(id);

        if (producerDetails == null)
        {
            return View("NotFound");
        }

        return View(producerDetails);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }

        await _service.AddAsync(producer);
        return RedirectToAction(nameof(Producer));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var producerDetails = await _service.GetEntityByIdAsync(id);
        if (producerDetails == null)
        {
            return View("NotFound");
        }
        return View(producerDetails);
    }

    public async Task<IActionResult> Edit(int id, [Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }

        if (id == producer.Id)
        {
            await _service.UpdateAsync(id, producer);

            return RedirectToAction(nameof(Producer));
        }
        return View(producer);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var producerDetails = await _service.GetEntityByIdAsync(id);
        if(producerDetails == null)
        {
            return View("NotFound");
        }
        return View(producerDetails);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producerDetails = await _service.GetEntityByIdAsync(id);
        if(producerDetails == null)
        {
            return View("NotFound");
        }
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Producer));
    }
}
