using eTickets.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext _context;
        public ProducerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Producer()
        {
            var data = await _context.Producers.ToListAsync();

            return View(data);
        }
    }
}
