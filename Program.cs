using eTickets.Context;
using eTickets.Data.Cart;
using eTickets.Data.Services.IServices;
using eTickets.Data.Services.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//configure the httpcontextaccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Here we are going to configure authentication and authorization
//the first parameter for the AddIdentity method is going to be the IdentityUser class, and the second is going to be IdentityRole and then we need to define where we are going
//to store the authentication related data and which file we want to use
//after this method we are going to add the AddEntityFrameworStores method which is going to take a parameter, so which file does the ef to work with the user data
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

//here we are going to add the memory cashe
builder.Services.AddMemoryCache();

builder.Services.AddSession();

//here we are going to add the authentication method and inside the brackets we can put the default one or we can define some options
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //here we can also define other Identity options for examplehow long you want the password to be at least  etc
    //we can also define custom properties for the cookie
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//define the authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Movie}/{id?}");

AppDbInitializer.Seed(app);
//we use Wait() because it is an async method
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
