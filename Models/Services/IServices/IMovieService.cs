using eTickets.Models.BaseRepository;
using eTickets.Models.ViewModels;

namespace eTickets.Models.Services.IServices;

public interface IMovieService : IBaseEntityRepository<Movie>
{
    Task<Movie> GetMovieByIdAsync(int id);

    Task<MovieDropdownVM> GetMovieDropdownValues();
}
