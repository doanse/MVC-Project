using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
	public class AccountController : Controller
	{
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
	}
}