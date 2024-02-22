namespace eTickets.Models.Services.IServices;

public interface IActorService
{
    public Task<IEnumerable<Actor>> GetActors();
    public Task<Actor> GetActorById(int actorId);
    public void AddActor(Actor actor);
    public Task<Actor> UpdateActor(int actorId, Actor newActor);
    public Task DeleteActor(int actorId);
}
