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


		public bool CraftProduct(int productId, int count, List<string> ingredients)
		{

			//TODO: Call Database

			return Database.CraftProduct(productId, count, ingredients);
		}

		//public void CraftProduct(int productId, int count, List<int> ingredients)
		//{

		//	//TODO: Call Database

		//	Database.CraftProduct(productId, count, ingredients);
		//}

		public int GetLocationProductCount(int locationId, int productId)
		{
			return Database.GetLocationProductCount(locationId, productId);

		}

		public int GetCountOfProduct(int productId)
		{
			return Database.GetCountOfProduct(productId);

		}

		public int GetCountOfItem(int itemId)
		{
			return Database.GetCountOfItem(itemId);

		}

        internal void CraftProduct(int productId, int v, List<int> ingredients)
        {
            throw new NotImplementedException();
        }
    }
}

