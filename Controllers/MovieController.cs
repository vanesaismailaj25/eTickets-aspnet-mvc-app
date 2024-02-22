using eTickets.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        public MovieController(AppDbContext context)
        {
             _context = context;
        }
        public async Task<IActionResult> Movie()
        {
            var data = await _context.Movies.Include(c => c.Cinema).ToListAsync();

            return View(data);
        }
    }
}
