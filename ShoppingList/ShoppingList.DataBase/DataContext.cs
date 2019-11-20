using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.DataBase
{
	public class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Purchase> Purchases { get; set; }

		public DataContext(DbContextOptions<DataContext> options)
		    : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
