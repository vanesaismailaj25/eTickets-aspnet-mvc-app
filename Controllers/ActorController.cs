using eTickets.Data.Services.IServices;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _service;
        public ActorController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Actor()
        {
            var data = await _service.GetAllAsync();

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Actor));
        }

        public async Task<IActionResult> Details(int actorId)
        {
            var details = await _service.GetEntityByIdAsync(actorId);

            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }

        public async Task<IActionResult> Edit(int actorId)
        {
            var details = await _service.GetEntityByIdAsync(actorId);

            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int actorId, [Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(actorId, actor);
            return RedirectToAction(nameof(Actor));
        }

        public async Task<IActionResult> Delete(int actorId)
        {
            var details = await _service.GetEntityByIdAsync(actorId);

            if (details == null)
            {
                return View("NotFound");
            }
            return View(details);
        }

        [HttpPost]
        /* we cannot have two methods with the same name and the same parameters in the controller*/
        public async Task<IActionResult> DeleteConfirmed(int actorId)
        {
            var actorExists = await _service.GetEntityByIdAsync(actorId);
            if (actorExists == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(actorId);
            return RedirectToAction(nameof(Actor));
        }
    }
}
