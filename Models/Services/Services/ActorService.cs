using eTickets.Context;
using eTickets.Models.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Services.Services;

public class ActorService : IActorService
{
    private readonly AppDbContext _context;
    public ActorService(AppDbContext context)
    {
        _context = context;
    }
    public void AddActor(Actor actor)
    {
         _context.Actors.Add(actor);
        _context.SaveChanges();
    }

    public async Task DeleteActor(int actorId)
    {
        var result = await _context.Actors.FirstOrDefaultAsync(a => a.Id == actorId);
        
        _context.Actors.Remove(result); 
        await _context.SaveChangesAsync();
    }

    public async Task<Actor> GetActorById(int actorId)
    {
        var result = await _context.Actors.FirstOrDefaultAsync(a => a.Id == actorId);

        return result;
    }

    public async Task<IEnumerable<Actor>> GetActors()
    {
        var result = await _context.Actors.ToListAsync();

        return result;
    }

    public async Task<Actor> UpdateActor(int actorId, Actor newActor)
    {
        _context.Update(newActor);
        await _context.SaveChangesAsync();

        return newActor;
    }
}
