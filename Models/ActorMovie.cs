namespace eTickets.Models;

public class ActorMovie
{
    public int ActorId { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Actor Actor { get; set; }
}
