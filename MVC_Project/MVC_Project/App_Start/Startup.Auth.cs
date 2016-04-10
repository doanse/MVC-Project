using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using MVC_Project.Models;

namespace MVC_Project
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
			//Метод CreatePerOwinContext регистрирует в OWIN менеджер пользователей ApplicationUserManager и класс контекста ApplicationContext.
			//Метод UseCookieAuthentication с помощью объекта CookieAuthenticationOptions устанавливает аутентификацию на основе куки в качестве типа аутентификации в приложении.
			//свойство LoginPath позволяет установить адрес URL, по которому будут перенаправляться неавторизованные пользователи. Это адрес /Account/Login.
			// настраиваем контекст и менеджер
			app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

			// регистрация менеджера ролей
			app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
			});			
		}
    }
}