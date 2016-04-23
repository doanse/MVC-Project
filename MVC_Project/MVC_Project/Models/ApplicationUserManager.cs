using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MVC_Project.App_Start;
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

			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();

			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}

			return manager;
		}
	}
}