using eTickets.Context;
using eTickets.Models.BaseRepository;
using eTickets.Models.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Services.Services;

public class ActorService : BaseEntityRepository<Actor>, IActorService
{
    public ActorService(AppDbContext context) : base(context) { }
   
}
