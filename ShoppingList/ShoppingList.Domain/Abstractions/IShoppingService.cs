using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Domain.Abstractions
{
	public interface IShoppingService
	{
		void InitializeDatabase();
		IEnumerable<Purchase> GetPurchases();
		Purchase GetPurchaseById(int id);
		void PostPurchase([FromBody]Purchase list);
		void PutPurchase([FromBody]Purchase list);
		void DeletePurchaseById(Purchase purchase, int id);
		Purchase FindPurchase(int id);
	}
}
