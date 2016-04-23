using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVC_Project.Models;
using System.Net.Mail;

namespace MVC_Project.App_Start
{
	public class EmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// настройка логина, пароля отправителя
			var from = "antoxadoanse@gmail.com";
			var pass = "das03071993";

			// адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
			SmtpClient client = new SmtpClient("smtp.gmail.com", 25);

			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(from, pass);
			client.EnableSsl = true;

			// создаем письмо: message.Destination - адрес получателя
			var mail = new MailMessage(from, message.Destination);
			mail.Subject = message.Subject;
			mail.Body = message.Body;
			mail.IsBodyHtml = true;

			return client.SendMailAsync(mail);
		}
	}

	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your SMS service here to send a text message.
			return Task.FromResult(0);
		}
	}
}