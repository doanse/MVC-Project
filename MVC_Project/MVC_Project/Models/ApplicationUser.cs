using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
	//КЛАСС ПОЛЬЗОВАТЕЛЕЙ, УНАСЛЕДОВАН ОТ  IdentityUser (ДОБАВЛЕНО СВОЙСТВО YEAR В ДОПОЛНЕНИЕ К СВОЙСТВАМ  IdentityUser)
	public class ApplicationUser : IdentityUser
	{
		public int Year { get; set; }
		public ApplicationUser()
		{
		}
	}
}