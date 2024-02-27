using eTickets.Context;
using eTickets.Data.BaseRepository;
using eTickets.Data.Services.IServices;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services.Services;

public class ActorService : BaseEntityRepository<Actor>, IActorService
{
    public ActorService(AppDbContext context) : base(context) { }

}
