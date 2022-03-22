using System;
using System.Collections.Generic;
using MonsterHunterInventory.Services;

namespace MonsterHunterInventory.Models
{
	public class ProductBook
	{
		//Recipes variable that we can call from frontend
		public List<Product> Products = new List<Product>();

		//Constructor
		public ProductBook()
		{

			//TODO: Call Database
			Products = Database.GetAllProducts();
		}


		public void CraftProduct(string name, int count, List<string> ingredients)
		{

			//TODO: Call Database

			Database.CraftProduct(name, count, ingredients);
		}
	}
}

