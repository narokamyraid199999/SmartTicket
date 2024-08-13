using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;
using System.Security.Claims;

namespace SmartTicket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        public CartController(SmartTicketContext myDb, UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _myDB = myDb;
            _userManager = userManager;
            _emailService = emailService;
        }

        private readonly SmartTicketContext _myDB;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        [HttpGet]
        public IActionResult Index()
        {
            string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var userCart = _myDB.carts.FirstOrDefault(x => x.UserId.Contains(id));

            if (userCart == null)
            {
                return BadRequest(new { error = $"Invalid user id {id}" });
            }

            var userCartDetails = _myDB.cartDetails.Include(y => y.Movie).Include(m => m.cart).Where(x => x.cartId == userCart.Id).ToList();
            
            /*
            if (userCartDetails == null || userCartDetails.Count == 0)
            {
                return Ok(new { msg = $"Cart id {userCart.Id} is empty" });
            }
            */
            return View(userCartDetails);
        }

        [HttpPost]
        public IActionResult Add(int Id)
        {
            if (Id != 0)
            {
                string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var userCart = _myDB.carts.FirstOrDefault(x => x.UserId.Contains(id));

                if (userCart == null)
                {
                    return NotFound(new { error = $"Invalid user id {id}" });
                }

                var userMovie = _myDB.Movies.FirstOrDefault(x => x.Id == Id);

                if (userCart == null)
                {
                    return NotFound(new { error = $"Invalid user id {id}" });
                }

                _myDB.cartDetails.Add(new cartDetails { cartId=userCart.Id, movieId= Id });
                _myDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");

        }


        [HttpGet]
        [HttpDelete]
        public IActionResult Clear()
        {
			string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			var userCart = _myDB.carts.FirstOrDefault(x => x.UserId.Contains(id));

			if (userCart == null)
			{
				return NotFound(new { error = $"Invalid user id {id}" });
			}

			var userCartDetails = _myDB.cartDetails.Where(x => x.cartId == userCart.Id).ToList();

            foreach(var cartDetails in userCartDetails)
            {
                _myDB.cartDetails.Remove(cartDetails);
            }

            _myDB.SaveChanges();

			return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult decrease(int cartDetailsId) 
        {
			string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			var userCart = _myDB.carts.FirstOrDefault(x => x.UserId.Contains(id));

			if (userCart == null)
			{
				return NotFound(new { error = $"Invalid user id {id}" });
			}

            var cartDetailsEntity = _myDB.cartDetails.FirstOrDefault(x=>x.Id == cartDetailsId);

            if (cartDetailsEntity == null) 
            {
				return RedirectToAction("Index");
			}

            _myDB.cartDetails.Remove(cartDetailsEntity);
            _myDB.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult increase(int movieId)
		{
			string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			var userCart = _myDB.carts.FirstOrDefault(x => x.UserId.Contains(id));

			if (userCart == null)
			{
				return NotFound(new { error = $"Invalid user id {id}" });
			}

            _myDB.cartDetails.Add(new cartDetails { cartId=userCart.Id, movieId=movieId});
            _myDB.SaveChanges();

			return RedirectToAction("Index");
		}

        [HttpGet]
        public async Task<IActionResult> checkout()
        {
			string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
			string email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

			Order newOrder = new Order() { userId= id };
            _myDB.Orders.Add(newOrder);
			_myDB.SaveChanges();

			var cartItems = _myDB.cartDetails.Include(x=>x.Movie).ToList();

            if (cartItems.Count == 0) 
            {
			    return RedirectToAction("Index", "Home");
			}

			foreach (var cartItem in cartItems)
            {
                _myDB.OrderDetails.Add(new OrderDetails {orderId=newOrder.Id, movieId=cartItem.movieId });
            }

            _myDB.SaveChanges();

            string msgBody = "";

            foreach (var cartItem in cartItems)
            {
                msgBody += $"Movie: {cartItem.Movie.Name}, Price: {cartItem.Movie.Price}, StartData: {cartItem.Movie.StartDate}, EndDate: {cartItem.Movie.EndDate}";
                await _emailService.sendMailAsync(email, "Movie Ticket", msgBody);
                msgBody = "";
            }

            foreach (var cartItem in cartItems)
			{
			    _myDB.cartDetails.Remove(cartItem);
            }

			_myDB.SaveChanges();

			return View("checkout", email);
        }

	}
}
