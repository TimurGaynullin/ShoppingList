using Microsoft.AspNetCore.Mvc;
using ShoppingList.DataBase;
using ShoppingList.Domain.Abstractions;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingList.Domain
{
	public class ShoppingService : IShoppingService
	{
		Hasher hasher;
		DataContext db;
		public ShoppingService(DataContext db, Hasher hasher)
		{
			this.hasher = hasher;
			this.db = db;
		}

		public void InitializeDatabase()
		{
			if (!db.Purchases.Any())
			{
				db.Purchases.Add(new Purchase { Name = "Bread", Price = 30, Discount = 0, TimeOfNotation = new DateTime(2019, 1, 1) });
				db.Purchases.Add(new Purchase { Name = "Milk", Price = 50, Discount = 5, TimeOfNotation = new DateTime(2019, 1, 10) });
				db.Purchases.Add(new Purchase { Name = "Cheese", Price = 500, Discount = 10, TimeOfNotation = new DateTime(2019, 2, 5) });
				db.Purchases.Add(new Purchase { Name = "Meat", Price = 1000, Discount = 7, TimeOfNotation = new DateTime(2019, 2, 23) });
				db.Purchases.Add(new Purchase { Name = "Bananas", Price = 80, Discount = 0, TimeOfNotation = new DateTime(2019, 3, 8) });

				db.SaveChanges();
			}
			if (!db.Users.Any())
			{
				db.Users.Add(new User { Login = "Timur", Passwords = new List<Password> { new Password { Value = hasher.CryptPassword("qwerty"), CreatingTime = DateTime.Now } }, Email = "123456@yandex.ru" }) ;
				db.SaveChanges();
			}
		}

		public IEnumerable<Purchase> GetPurchases()
		{
			return db.Purchases.ToList();
		}
		public Purchase GetPurchaseById(int id)
		{
			Purchase list = db.Purchases.FirstOrDefault(x => x.Id == id);
			return list;
		}
		public void PostPurchase([FromBody]Purchase list)
		{
			db.Purchases.Add(list);
			db.SaveChanges();
		}
		public void PutPurchase([FromBody]Purchase list)
		{
			db.Update(list);
			db.SaveChanges();

		}
		public void DeletePurchaseById(Purchase purchase, int id)
		{
			db.Purchases.Remove(purchase);
			db.SaveChanges();
		}
		public Purchase FindPurchase(int id)
		{
			Purchase purchase = db.Purchases.FirstOrDefault(x => x.Id == id);
			return purchase;
		}
	}
}
