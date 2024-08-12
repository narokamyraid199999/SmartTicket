using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using System.Security.Claims;

namespace SmartTicket.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{

        public OrderController(SmartTicketContext myDb)
        {
            _myDb = myDb;
        }

		private readonly SmartTicketContext _myDb;

        public IActionResult Index()
		{
			string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			var orders = _myDb.Orders.Where(x=>x.userId == id).ToList();

			return View("Index", orders);
		}

		[HttpGet]
		public IActionResult Details(int id) 
		{
			string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			var userOrder = _myDb.Orders.FirstOrDefault(x=>x.Id == id && x.userId == UserId);

			if (userOrder == null) 
			{ 
				return RedirectToAction("Index", "Home");
			}

			List<OrderDetails> orders = _myDb.OrderDetails.Include(x=>x.Movie).Where(x=>x.orderId== userOrder.Id).ToList();

			return View("Details", orders);

		}

	}
}
