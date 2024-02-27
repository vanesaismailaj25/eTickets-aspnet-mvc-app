using eTickets.Context;
using eTickets.Data.BaseRepository;
using eTickets.Data.Services.IServices;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services.Services;

public class MovieService : BaseEntityRepository<Movie>, IMovieService
{
    private readonly AppDbContext _context;
    public MovieService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddNewMovie(MovieVM movie)
    {
        var newMovie = new Movie()
        {
            Name = movie.Name,
            Description = movie.Description,
            Price = movie.Price,
            ImageURL = movie.ImageURL,
            CinemaId = movie.CinemaId,
            StartDate = movie.StartDate,
            EndDate = movie.EndDate,
            MovieCategory = movie.MovieCategory,
            ProducerId = movie.ProducerId
        };

        await _context.AddAsync(newMovie);
        await _context.SaveChangesAsync();

        //lets add the actors
        foreach (var actorId in movie.ActorIds)
        {
            var newActorMovie = new ActorMovie()
            {
                MovieId = newMovie.Id,
                ActorId = actorId
            };

            await _context.ActorsMovies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync();
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

    public async Task UpdateMovie(MovieVM movie)
    {
        var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);

        if (dbMovie != null)
        {
            dbMovie.Name = movie.Name;
            dbMovie.Description = movie.Description;
            dbMovie.Price = movie.Price;
            dbMovie.ImageURL = movie.ImageURL;
            dbMovie.CinemaId = movie.CinemaId;
            dbMovie.StartDate = movie.StartDate;
            dbMovie.EndDate = movie.EndDate;
            dbMovie.MovieCategory = movie.MovieCategory;
            dbMovie.ProducerId = movie.ProducerId;

            await _context.SaveChangesAsync();
        }

        //remove existing actors
        var existingActors = _context.ActorsMovies.Where(a => a.MovieId == movie.Id).ToList();
        _context.ActorsMovies.RemoveRange(existingActors);
        await _context.SaveChangesAsync();

        //add actors
        foreach (var actorId in movie.ActorIds)
        {
            var newActorMovie = new ActorMovie()
            {
                MovieId = movie.Id,
                ActorId = actorId,
            };
            await _context.ActorsMovies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync();
    }
}
