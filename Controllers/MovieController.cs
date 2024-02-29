using Microsoft.AspNetCore.Mvc;
using eTickets.Data.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _service;
    public MovieController(IMovieService service)
    {
         _service = service;
    }
    public async Task<IActionResult> Movie()
    {
        var data = await _service.GetAllAsync(n => n.Cinema);

        return View(data);
    }
    public async Task<IActionResult> Filter(string searchString)
    {
        var data = await _service.GetAllAsync(n => n.Cinema);

        if (!string.IsNullOrEmpty(searchString))
        {
            var filteredResult = data.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

            return View("Movie", filteredResult);
        }

        return View("Movie", data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _service.GetEntityByIdAsync(id);

        return View(movieDetails);
    }

    public async Task<IActionResult> Create()
    {
        var movieDropdownData = await _service.GetMovieDropdownValues();

        ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MovieVM movie)
    {
        if(!ModelState.IsValid)
        {
            var movieDropdownData = await _service.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");

            return View(movie);
        }

        await _service.AddNewMovie(movie);
        return RedirectToAction(nameof(Movie));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var movieDetails = await _service.GetEntityByIdAsync(id);

        if (movieDetails == null)
            return View("NotFound");

        var response = new MovieVM()
        {
            Id = movieDetails.Id,
            Name = movieDetails.Name,
            Description = movieDetails.Description,
            StartDate = movieDetails.StartDate,
            EndDate = movieDetails.EndDate,
            Price = movieDetails.Price,
            ImageURL = movieDetails.ImageURL,
            MovieCategory = movieDetails.MovieCategory,
            CinemaId = movieDetails.CinemaId,
            ProducerId = movieDetails.ProducerId,
            ActorIds = movieDetails.ActorsMovies.Select(a => a.ActorId).ToList(),
        };

        var movieDropdownData = await _service.GetMovieDropdownValues();

        ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, MovieVM movie)
    {
        if(id != movie.Id)
        {
            return View("NotFound");
        }

        if (!ModelState.IsValid)
        {
            var movieDropdownData = await _service.GetMovieDropdownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "Name");

            return View(movie);
        }

        await _service.UpdateMovie(movie);
        return RedirectToAction(nameof(Movie));
    }
}
