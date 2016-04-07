using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Site.Models;


namespace Web_Site.Controllers
{
    public class HomeController : Controller
    {
		// создаем контекст данных
		ContextUser db = new ContextUser();
		public ActionResult Index()
        {			
			return View();
        }

		/// <summary>
		/// Вход на сайт
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Entrance()
		{
			IEnumerable<User> users = db.Users;
			return View(users);
		}

		/// <summary>
		/// Добавление
		/// </summary>
		/// <returns></returns>

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(User user)
		{
			db.Users.Add(user);
			db.SaveChanges();			
			return RedirectToAction("Index");
		}


	}
}