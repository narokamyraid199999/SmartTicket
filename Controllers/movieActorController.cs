using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket.Controllers
{
    [Authorize(Roles = "Admin")]

    public class movieActorController : Controller
    {


        public movieActorController(IService<MovieActor> movieActorService, IService<Movie> movieService, IService<Actor> actorService)
        {
			_movieActorService = movieActorService;
			_movieService = movieService;
			_actorService = actorService;
        }

        private readonly IService<MovieActor> _movieActorService;
        private readonly IService<Movie> _movieService;
        private readonly IService<Actor> _actorService;

        public async Task<IActionResult> Index()
        {
            var movieActor = await _movieActorService.GetAll();
            return View(movieActor);
        }

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			ViewBag.movieId = new SelectList(await _movieService.GetAll(), "Id", "Name");
			ViewBag.actorId = new SelectList(await _actorService.GetAll(), "Id", "FirstName");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(MovieActor newMovieActor)
		{
			if (ModelState.IsValid)
			{
				await _movieActorService.Create(newMovieActor);
				return RedirectToAction("Index");
			}
			ViewBag.movieId = new SelectList(await _movieService.GetAll(), "Id", "Name");
			ViewBag.actorId = new SelectList(await _actorService.GetAll(), "Id", "FirstName");

			return View(newMovieActor);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var tempMovieActor = _movieActorService.getById(id);
			if (tempMovieActor == null) 
			{
				return NotFound($"Invalid MovieActor Id {id}");
			}

			await _movieActorService.Delete(id);

			return RedirectToAction("Index");

		}


    }
}
