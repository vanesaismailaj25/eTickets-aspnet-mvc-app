using eTickets.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorController : Controller
    {
        private readonly AppDbContext _context;
        public ActorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Actors()
        {
            var data =await _context.Actors.ToListAsync();

            return View(data);
        }
    }
}
