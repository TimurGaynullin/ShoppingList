using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Domain
{
	public class PasswordPolicy
	{
		public bool IsOk(string password)
		{
			var x1 = password.Length > 5;
			var x2 = password != password.ToLower();
			bool x3 = false;
			for (int i = 0; i < password.Length; i++)
			{
				if(Char.IsDigit(password[i]))
				{
					x3 = true;
					break;
				}
			}
			if (x1 && x2 && x3)
			{
				return true;
			}
			return false;
		}
	}
}
