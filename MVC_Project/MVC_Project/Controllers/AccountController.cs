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
		private ApplicationUserManager UserManager //Через него мы будем взаимодействовать с хранилищем пользователей.
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}

		// вытаскиваю пользователей
		//[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult Entrance()
		{
			//IEnumerable<UserData> users = db.Users.Select(x => new UserData {Email = x.Email, Password = x.PasswordHash, Year = x.Year });
			return View(UserManager.Users);
		}

		public ActionResult Register() // метод для регистрации
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model) // Post-версия представляет асинхронный метод, поскольку для создания пользователя здесь используется асинхронный вызов UserManager.CreateAsync().
		{
			if (ModelState.IsValid)
			{
				//ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
				ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
				IdentityResult result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{

				    UserManager.AddToRole(user.Id, "Admin");
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
		public async Task<ActionResult> DeleteConfirmed(string id, string name)
		{			

			ApplicationUser user = await UserManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await UserManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					if (name == User.Identity.Name)
					{
						return RedirectToAction("Logout", "Account");
					}
					else
					{
						return RedirectToAction("Entrance", "Account");
					}
					
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> Edit(string id)
		{
			ApplicationUser user = await UserManager.FindByIdAsync(id);
			if (user != null)
			{
				EditModel model = new EditModel { Year = user.Year, Id = user.Id };
				return View(model);
			}
			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		public async Task<ActionResult> Edit(EditModel model)
		{
			ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
			if (user != null)
			{
				user.Year = model.Year;
				IdentityResult result = await UserManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Entrance", "Account");
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