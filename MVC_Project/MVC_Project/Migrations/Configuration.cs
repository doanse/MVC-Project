namespace MVC_Project.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<MVC_Project.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVC_Project.Models.ApplicationContext";
        }

        protected override void Seed(MVC_Project.Models.ApplicationContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			//context.Roles.AddOrUpdate(r => r.Name,
			//	new IdentityRole { Name = "Admin" });
			//var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			//um.AddToRole("13a4d1de-d2da-4e5e-875d-4f6068996024", "Admin");

		}
    }
}
