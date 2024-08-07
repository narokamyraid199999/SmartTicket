using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTicket.DTO;

namespace SmartTicket.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
	{


		public RoleController(RoleManager<IdentityRole> roleManager) 
		{
			_roleManager = roleManager;
		}

		private readonly RoleManager<IdentityRole> _roleManager;

		public async Task<IActionResult> Index()
		{
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> Add() 
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(RoleDTO newRole)
		{
			if(ModelState.IsValid)
			{

				var isRoleExists = await _roleManager.RoleExistsAsync(newRole.Name);

                if (!isRoleExists)
				{
                    IdentityRole tempRole = new IdentityRole(newRole.Name);

                    var res = await _roleManager.CreateAsync(tempRole);

                    if (res.Succeeded)
                    {
                        return View();
                    }

                    string errorDetails = "";

                    foreach (var error in res.Errors)
                    {
                        errorDetails += error.Description + ", ";
                    }

                    return NotFound(errorDetails);

                }
            }



			return View();

		}

	}
}
