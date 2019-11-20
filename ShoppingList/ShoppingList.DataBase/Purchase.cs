using System;

namespace ShoppingList.Models
{
	public class Purchase
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public int Discount { get; set; }
		public DateTime TimeOfNotation { get; set; }
	}
}
