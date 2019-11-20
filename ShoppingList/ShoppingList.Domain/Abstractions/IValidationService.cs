using ShoppingList.DataBase;

namespace ShoppingList.Domain.Abstractions
{
	public interface IValidationService
	{
		bool ChangingPassword(User user, string pass);
		bool LogingIn(User user, string pass);
		bool Registering(User user, string Email, string Login, string Password);
	}
}
