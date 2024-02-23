using eTickets.Context;
using eTickets.Models.BaseRepository;
using eTickets.Models.Services.IServices;
using eTickets.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Services.Services;

public class MovieService : BaseEntityRepository<Movie>, IMovieService
{
    private readonly AppDbContext _context;
    public MovieService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movieDetails = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.ActorsMovies).ThenInclude(a => a.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);

        return movieDetails;
    }

    public async Task<MovieDropdownVM> GetMovieDropdownValues()
    {
        var response = new MovieDropdownVM()
        {
            Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(a => a.FullName).ToListAsync()
        };

        return response;
    }
}
