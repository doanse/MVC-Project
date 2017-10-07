using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
	//Test 2
	//Test souceTree
	//КЛАСС КОНТЕКСТА ДАННЫХ (ОН НУЖЕН ДЛЯ СВЯЗИ С БАЗОЙ)
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext() : base("IdentityDb") { }

		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}
	}
}