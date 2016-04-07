using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MVC_Project.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// настраиваем контекст и менеджер
			app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
			});
		}
	}
}