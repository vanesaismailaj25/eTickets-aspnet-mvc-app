using eTickets.Context;
using eTickets.Models.BaseRepository;
using eTickets.Models.Services.IServices;

namespace eTickets.Models.Services.Services;

public class CinemaService: BaseEntityRepository<Cinema>, ICinemaService
{
    public CinemaService(AppDbContext context) : base(context) { }

}
