using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket.Controllers
{
    public class SearchController : Controller
    {

        public SearchController(ISearch searchService)
        {
            _searchService = searchService;
        }

        private readonly ISearch _searchService;

        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            if (name != null)
            {
                var searchedMovie = await _searchService.Search(name);
                
                if (searchedMovie == null || searchedMovie.Count == 0)
                {
                    return RedirectToAction("NotFound");
                }

                return View(searchedMovie);
            }

            return RedirectToAction("NotFound");

        }
    }
}
