using Microsoft.AspNet.Identity.Owin;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
	public class HomeController : Controller
	{
		//private ApplicationUserManager UserManager //Через него мы будем взаимодействовать с хранилищем пользователей.
		//{
		//	get
		//	{
		//		return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
		//	}
		//}
		ApplicationContext db = new ApplicationContext();
		public ActionResult Index()
		{
			return View();
		}

		//// вытаскиваю пользователей
		//[Authorize(Roles = "Admin")]
		//[HttpGet]
		//public ActionResult Entrance()
		//{
		//	//IEnumerable<UserData> users = db.Users.Select(x => new UserData {Email = x.Email, Password = x.PasswordHash, Year = x.Year });
		//	return View(UserManager.Users);
		//}

	}
}