using eTickets.Data.BaseRepository;
using eTickets.Data.ViewModels;

namespace eTickets.Data.Services.IServices;

public interface IMovieService : IBaseEntityRepository<Movie>
{
    Task<Movie> GetMovieByIdAsync(int id);

    Task<MovieDropdownVM> GetMovieDropdownValues();
    Task AddNewMovie(MovieVM movie);
    Task UpdateMovie(MovieVM movie);
}
