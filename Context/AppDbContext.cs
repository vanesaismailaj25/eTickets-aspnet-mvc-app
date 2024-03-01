using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<ActorMovie> ActorsMovies { get; set; }

    //order related tables
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ActorMovie>().HasKey(am => new
        {
            am.ActorId,
            am.MovieId
        });

        modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie)
            .WithMany(am => am.ActorsMovies)
            .HasForeignKey(m => m.MovieId);

        modelBuilder.Entity<ActorMovie>().HasOne(a => a.Actor)
            .WithMany(am => am.ActorsMovies)
            .HasForeignKey(a => a.ActorId);

        base.OnModelCreating(modelBuilder);
    }
}
