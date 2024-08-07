using SmartTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SmartTicket.ViewModel;

namespace SmartTicket.Controllers
{
	public class AccountController : Controller
	{

		public AccountController(UserManager<IdentityUser> MyManager, SignInManager<IdentityUser> _signINManage) { 
			_MyUserManager = MyManager;
			_mySignInManager = _signINManage;
        }

		public readonly UserManager<IdentityUser> _MyUserManager;
        public readonly SignInManager<IdentityUser> _mySignInManager;

        [HttpGet]
		public IActionResult Regester()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Regester(RegesterViewModel newUserName)
		{
			if (ModelState.IsValid)
			{

				IdentityUser user = new IdentityUser();
				user.UserName = newUserName.Username;
				user.Email = newUserName.Username;


				IdentityResult res = await _MyUserManager.CreateAsync(user, newUserName.Password);

				if (res.Succeeded)
				{
					IdentityResult roleRes =  await _MyUserManager.AddToRoleAsync(user, "User");

					if (roleRes.Succeeded)
					{
                        await _mySignInManager.SignInAsync(user, true);
                        // create cookie
                        return RedirectToAction("Index", "Home");
                    }
                    else
					{
						ModelState.AddModelError("", "Invalid Role Name Used for User");
						return View(newUserName);
					}

				}
				else
				{
					// check for errors
					foreach (var errorItem in res.Errors)
					{
						ModelState.AddModelError("Password", errorItem.Description);
					}
				}

			}
			return View(newUserName);
		}

		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }


		[HttpPost]
		public async Task<IActionResult> Login(LoginModelView LoginUser)
		{
			if (ModelState.IsValid)
			{

				var userModel = await _MyUserManager.FindByNameAsync(LoginUser.Username);
				if (userModel != null)
				{
					bool isValidPassword = await _MyUserManager.CheckPasswordAsync(userModel, LoginUser.Password);
					if (isValidPassword)
					{
						await _mySignInManager.SignInAsync(userModel, LoginUser.RememberMe);
						return RedirectToAction("Index", "Home");
					}
					else
					{
                        ModelState.AddModelError("", "Invalid Password");
                        return View(LoginUser);
                    }
                }
                else
				{
					ModelState.AddModelError("", "Invalid Username");
                    return View(LoginUser);
                }
            }
            else
			{
				return View(LoginUser);
			}
		}

		[HttpGet]
		[HttpDelete]
		public async Task<IActionResult> SignOut()
		{
            await _mySignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}


    }
}
