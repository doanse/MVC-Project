using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
				: base(store)
		{
		}
		//Create() создает экземпляр класса ApplicationUserManager с помощью объекта контекста IOwinContext
		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
												IOwinContext context)
		{
			ApplicationContext db = context.Get<ApplicationContext>();
			ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
			return manager;
		}
	}
}