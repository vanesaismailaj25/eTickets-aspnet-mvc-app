using eTickets.Context;
using eTickets.Data.BaseRepository;
using eTickets.Data.Services.IServices;
using eTickets.Models;

namespace eTickets.Data.Services.Services;

public class CinemaService : BaseEntityRepository<Cinema>, ICinemaService
{
    public CinemaService(AppDbContext context) : base(context) { }

}
