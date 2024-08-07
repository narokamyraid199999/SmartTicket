using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;

namespace SmartTicket.Controllers
{
	public class HomeController : Controller
	{

		public HomeController(SmartTicketContext myDb) 
		{
			_myDB = myDb;
		}

		private readonly SmartTicketContext _myDB;

		public IActionResult Index()
		{
			var movies = _myDB.Movies.Include(x=>x.Cinema).Include(x=>x.Category).ToList();
			return View(movies);
		}
	}
}
