using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket.Controllers
{
	public class ActorController : Controller
	{

		public ActorController(IService<Actor> actorService)
		{
			_actorService = actorService;
		}

		private readonly IService<Actor> _actorService;

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
		{
			var tempActors = await _actorService.GetAll();

			return View(tempActors);
		}

		[HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Actor newActor)
		{
			if (ModelState.IsValid)
			{
				await _actorService.Create(newActor);
				TempData["successMsg"] = $"Actor {newActor.FirstName} {newActor.LastName} has been added successfully";
				return RedirectToAction("Index", "Actor");
			}

			return View(newActor);

		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if (id == 0)
			{
				return RedirectToAction("Index", "Home");
			}

			var tempActors = await _actorService.getById(id);

			if (tempActors == null)
			{
				return RedirectToAction("Index", "Home");
			}

			return View(tempActors);
		}


		[HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
			{
				return RedirectToAction("Index", "Actor");
			}

			var tempActor = _actorService.getById(id);

			if (tempActor == null)
			{
				return RedirectToAction("Index", "Actor");
			}

			await _actorService.Delete(id);

			//TempData["msg"] = $"Movie {tempActor.FirstName} {tempActor.LastName} has been deleted succfully";

			return RedirectToAction("Index", "Actor");

		}


	}
}
