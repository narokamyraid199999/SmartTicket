using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket.Controllers
{

    public class MoviesController : Controller
    {

        public MoviesController(IService<Movie> movieService, IService<Category> categoryService, IService<Cinema> cinemaService, IService<MovieActor> movieActorService) 
        {
            _movieService = movieService;
            _categoryService = categoryService;
            _cinemaService = cinemaService;
            _movieActorService = movieActorService;
        }

        private readonly IService<Movie> _movieService;
        private readonly IService<Category> _categoryService;
        private readonly IService<Cinema> _cinemaService;
        private readonly IService<MovieActor> _movieActorService;

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var tempMovies = await _movieService.GetAll();

            return View(tempMovies);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
			ViewBag.CatId = new SelectList(await _cinemaService.GetAll(), "Id", "Name");
			ViewBag.cinemaId = new SelectList(await _categoryService.GetAll(), "Id", "Name");

			return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Movie newMovie)
		{
            if (ModelState.IsValid)
            {
                await _movieService.Create(newMovie);
                return RedirectToAction("Index", "Home");
			}

            ViewBag.CatId = new SelectList(await _cinemaService.GetAll(), "Id", "Name");
            ViewBag.cinemaId = new SelectList(await _categoryService.GetAll(), "Id", "Name");

            return View(newMovie);
		}

        [HttpGet]
        public async Task<IActionResult> Details( int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var movie = await _movieService.getById(id);

            if (movie == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tempActors = await _movieActorService.getMovieACtorForMovie(id);

            if (tempActors != null && tempActors.Count != 0)
            {
                ViewBag.actos = tempActors;
            }

            return View(movie);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var movie = await _movieService.getById(id);

            if (movie == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _movieService.Delete(id);

            TempData["delMsg"] = $"Movie {movie.Name} has been deleted succfully";

            return RedirectToAction("Index", "Home");
        }


    }
}
