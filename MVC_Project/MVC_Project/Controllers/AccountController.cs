using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
	public class AccountController : Controller
	{
		//РЕГИСТРАЦИЯ
		private ApplicationUserManager UserManager
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				//ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
				ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
				IdentityResult result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
			}
			return View(model);
		}

		//АУТЕНТИФИКАЦИЯ
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public ActionResult Login(string returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
				if (user == null)
				{
					ModelState.AddModelError("", "Неверный логин или пароль.");
				}
				else
				{
					ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
											DefaultAuthenticationTypes.ApplicationCookie);
					AuthenticationManager.SignOut();
					AuthenticationManager.SignIn(new AuthenticationProperties
					{
						IsPersistent = true
					}, claim);
					if (String.IsNullOrEmpty(returnUrl))
						return RedirectToAction("Index", "Home");
					return Redirect(returnUrl);
				}
			}
			ViewBag.returnUrl = returnUrl;
			return View(model);
		}

		//ВЫЙТИ
		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Login");
		}		

		//УДАЛЕНИЕ И ИЗМЕНЕНИЕ
		[HttpGet]
		public ActionResult Delete()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Delete")]
		public async Task<ActionResult> DeleteConfirmed()
		{
			ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				IdentityResult result = await UserManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Logout", "Account");
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> Edit()
		{
			ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				EditModel model = new EditModel { Year = user.Year };
				return View(model);
			}
			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		public async Task<ActionResult> Edit(EditModel model)
		{
			ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				user.Year = model.Year;
				IdentityResult result = await UserManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Что-то пошло не так");
				}
			}
			else
			{
				ModelState.AddModelError("", "Пользователь не найден");
			}

			return View(model);
		}
	}
	

}