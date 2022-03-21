using System;
using System.Collections.Generic;
using MonsterHunterInventory.Services;

namespace MonsterHunterInventory.Models
{
	public class CraftedItems
	{
		//Finished Crafted Products variable that we can call from frontend
		public List<Product> Products = new List<Product>();

		//Constructor
		public CraftedItems()
        {

			//TODO: Call Database
			Products = Database.GetAllProducts();
        }


		public void CraftProduct(string name, int count, List<string> items)
		{

			//TODO: Call Database

			Database.CraftProduct(name, count, items);
		}
	}
}

