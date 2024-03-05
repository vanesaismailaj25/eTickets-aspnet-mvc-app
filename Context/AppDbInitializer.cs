using eTickets.Data.Enums;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;

namespace eTickets.Context;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            //Seed for cinema, actors, producers and movies

            if (!context.Cinemas.Any())
            {
                context.Cinemas.AddRange(new List<Cinema>()
                {
                    new Cinema()
                    {
                        Name = "Cinema  1",
                        Description = "Description of cinema 1",
                        Logo = "https://dotnethow.net/images/cinemas/cinema-1.jpeg"
                    },

                    new Cinema()
                    {
                        Name = "Cinema 2",
                        Description = "Description of cinema 2",
                        Logo = "https://dotnethow.net/images/cinemas/cinema-2.jpeg"
                    },

                    new Cinema()
                    {
                        Name = "Cinema 3",
                        Description = "Description of cinema 3",
                        Logo = "https://dotnethow.net/images/cinemas/cinema-1.jpeg"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Actors.Any())
            {
                context.Actors.AddRange(new List<Actor>()
                {
                    new Actor()
                    {
                        FullName = "Actor 1",
                        Bio = "Bio of actor 1",
                        ProfilePictureURL = "https://dotnethow.net/images/actors/actor-1.jpeg"
                    },

                    new Actor()
                    {
                        FullName = "Actor 2",
                        Bio = "Bio of actor 2",
                        ProfilePictureURL = "https://dotnethow.net/images/actors/actor-2.jpeg"
                    },

                    new Actor()
                    {
                        FullName = "Actor 3",
                        Bio = "Bio of actor 3",
                        ProfilePictureURL = "https://dotnethow.net/images/actors/actor-3.jpeg"
                    },

                    new Actor()
                    {
                        FullName = "Actor 4",
                        Bio = "Bio of actor 4",
                        ProfilePictureURL = "https://dotnethow.net/images/actors/actor-4.jpeg"
                    },

                    new Actor()
                    {
                        FullName = "Actor 5",
                        Bio = "Bio of actor 5",
                        ProfilePictureURL = "https://dotnethow.net/images/actors/actor-5.jpeg"
                    },

                    new Actor()
                    {
                        FullName = "Actor 6",
                        Bio = "Bio of actor 6",
                        ProfilePictureURL= "https://dotnethow.net/images/actors/actor-6.jpeg"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Producers.Any())
            {
                context.Producers.AddRange(new List<Producer>()
                {
                    new Producer()
                    {
                        FullName= "Producer 1",
                        Bio="Bio of producer 1",
                        ProfilePictureURL ="https://dotnethow.net/images/producers/producer-1.jpeg"
                    },

                    new Producer()
                    {
                        FullName = "Producer 2",
                        Bio="Bio of producer 2",
                        ProfilePictureURL="https://dotnethow.net/images/producers/producer-2.jpeg"
                    },

                    new Producer()
                    {
                        FullName = "Producer 3",
                        Bio="Bio of producer 3",
                        ProfilePictureURL="https://dotnethow.net/images/producers/producer-3.jpeg"
                    }
                });
                context.SaveChanges();
            }

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(new List<Movie>()
                {
                    new Movie()
                    {
                        Name = "Movie 1",
                        Description = "Description of movie 1",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(15),
                        Price = 10.99,
                        ImageURL = "https://dotnethow.net/images/movies/movie-1.jpeg",
                        MovieCategory = MovieCategoryEnum.Action,
                        CinemaId = 1,
                        ProducerId =2
                    },

                    new Movie()
                    {
                        Name = "Movie 2",
                        Description = "Description of movie 2",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(5),
                        Price = 9.99,
                        ImageURL = "https://dotnethow.net/images/movies/movie-2.jpeg",
                        MovieCategory = MovieCategoryEnum.Documentary,
                        CinemaId = 2,
                        ProducerId = 3
                    },

                    new Movie()
                    {
                        Name = "Movie 3",
                        Description = "Description of movie 3",
                        StartDate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(15),
                        Price = 14.99,
                        ImageURL = "https://dotnethow.net/images/movies/movie-3.jpeg",
                        MovieCategory = MovieCategoryEnum.Drama,
                        CinemaId = 3,
                        ProducerId = 1
                    },

                    new Movie()
                    {
                        Name = "Movie 4",
                        Description = "Description of movie 4",
                        StartDate = DateTime.Now.AddDays(5),
                        EndDate = DateTime.Now.AddDays(30),
                        Price = 19.99,
                        ImageURL = "https://dotnethow.net/images/movies/movie-4.jpeg",
                        MovieCategory = MovieCategoryEnum.Comedy,
                        CinemaId = 1,
                        ProducerId =2
                    },
                });
                context.SaveChanges();
            }

            if (!context.ActorsMovies.Any())
            {
                context.ActorsMovies.AddRange(new List<ActorMovie>()
                {
                    new ActorMovie()
                    {
                        ActorId = 1,
                        MovieId = 2
                    },
                    new ActorMovie()
                    {
                        ActorId = 2,
                        MovieId = 2
                    },
                    new ActorMovie()
                    {
                        ActorId = 3,
                        MovieId = 2
                    },

                    new ActorMovie()
                    {
                        ActorId = 3,
                        MovieId = 1,
                    },
                    new ActorMovie()
                    {
                        ActorId= 4,
                        MovieId= 1,
                    },

                    new ActorMovie()
                    {
                        ActorId = 6,
                        MovieId = 3
                    },
                    new ActorMovie()
                    {
                        ActorId = 5,
                        MovieId = 3
                    },
                    new ActorMovie()
                    {
                        ActorId = 4,
                        MovieId = 3
                    },
                });
                context.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string adminUserEmail = "admin@etickets.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if(adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    FullName = "Admin User",
                    UserName = "admin",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "Coding@123");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

           
            string appUserEmail = "user@etickets.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new ApplicationUser()
                {
                    FullName = "Application User",
                    UserName = "user",
                    Email = appUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAppUser, "Coding@123");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}
