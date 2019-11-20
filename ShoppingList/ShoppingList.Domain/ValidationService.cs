using ShoppingList.DataBase;
using ShoppingList.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingList.Domain
{
	public class ValidationService : IValidationService
	{
		Hasher hasher;
		DataContext db;
		public ValidationService(Hasher hasher, DataContext db)
		{
			this.hasher = hasher;
			this.db = db;
		}
		
		public bool ChangingPassword(User user, string pass)
		{
			
			if (user != null)
			{
				pass = hasher.CryptPassword(pass);
				var passwords = user.Passwords.OrderByDescending(n => n.CreatingTime.Date).Take(3).ToList();
				if (passwords.Any(x => x.Value == pass))
					return false;
				user.Passwords.Add(new Password { Value = pass, CreatingTime = DateTime.Now });
				db.SaveChanges();
				return true;
			}
			return false;
		}

		public bool LogingIn(User user, string pass)
		{
			if (user != null)
			{
				var password = user.Passwords.OrderByDescending(n => n.CreatingTime.Date).Take(1).ToList();
				pass = hasher.CryptPassword(pass);
				if (password[0].Value == pass)
				{
					return true;
				}
			}
			return false;
		}
		public bool Registering(User user, string Email, string Login, string Password)
		{
			if (user == null)
			{
				var passwordValidation = new PasswordPolicy();
				if (passwordValidation.IsOk(Password))
				{
					Password = hasher.CryptPassword(Password);
					db.Users.Add(new User { Email = Email, Login = Login, Passwords = new List<Password> { new Password { Value = Password, CreatingTime = DateTime.Now } } });
					db.SaveChanges();
					return true;
				}
			}
			return false;
		}
	}
}
