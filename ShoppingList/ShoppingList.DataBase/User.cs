using System;
using System.Collections.Generic;

namespace ShoppingList.DataBase
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public List<Password> Passwords { get; set; }
		public string Email { get; set; }
	}
}
