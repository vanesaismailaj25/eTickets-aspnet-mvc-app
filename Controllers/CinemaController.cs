using eTickets.Context;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext _context;

        public CinemaController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Cinema()
        {
            var data = _context.Cinemas.ToList();

            return View(data);
        }
    }
}
