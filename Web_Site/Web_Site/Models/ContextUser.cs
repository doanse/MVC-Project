﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web_Site.Models
{
	public class ContextUser : DbContext
	{
		public DbSet<User> Users { get; set; }
	}
}