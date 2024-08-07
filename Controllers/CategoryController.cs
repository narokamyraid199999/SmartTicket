using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket.Controllers
{

    public class CategoryController : Controller
	{
        public CategoryController(IService<Category> categoryService, IService<Movie> movieService)
        {
            _categoryService = categoryService;
            _movieService = movieService;
        }

        private readonly IService<Category> _categoryService;
        private readonly IService<Movie> _movieService;

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetAll();	
			return View(categories);
		}


		[HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Category newCategory)
		{
			if (ModelState.IsValid)
			{
                await _categoryService.Create(newCategory);
				TempData["successMsg"] = $"Category {newCategory.Name} has been added successfully";
				return RedirectToAction("Index", "Category");
			}

			return View(newCategory);

		}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var tempCategory = _categoryService.getById(id);

            if (tempCategory == null)
            {
                return RedirectToAction("Index", "Home");
            }

			await _categoryService.Delete(id);

            //TempData["delMsg"] = $"Category {tempCategory.Name} has been deleted succfully";

            return RedirectToAction("Index", "Home");
        }


		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var categoryMovies = await _movieService.getMovieByCategoryId(id);
			return View(categoryMovies);
		}

    }
}
