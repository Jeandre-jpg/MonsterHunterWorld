using System.Collections.Generic;
using MonsterHunterInventory.Services;

namespace MonsterHunterInventory.Models
{
    public class Inventory


	{

		public List<Item> Items = new List<Item>();


		//constructor is going to fetch our database
		public Inventory()
		{

			Items = Database.GetAllItems();
			
		}

		public int GetLocationItemCount(int locationId, int itemId)
        {
			return Database.GetLocationItemCount(locationId, itemId);

		}

		public void UpdateHomebaseCount(int itemId, int count)
		{
			Database.UpdateHomebaseCount(itemId, count);

		}

		public void UpdatePouchCount(int itemId, int count)
		{
			Database.UpdatePouchCount(itemId, count);

		}

		public void UpdateBunkerCount(int itemId, int count)
		{
			Database.UpdateBunkerCount(itemId, count);

		}

		public int GetTotalItemCount(int itemId)
		{
			return Database.GetCountOfItem(itemId);

		}



		//public int GetHomebaseOfItem(string name)
		//{
		//	foreach (var homebasecount in Items) //searching for specific Item
		//	{
		//		if (homebasecount.Name == name)
		//		{
		//			return homebasecount.Count;
		//		}

		//	}
		//	return -1; //because there are no Items with that name
		//}

		//function to check how many items there are

		public int GetCount(string name)
		{
			foreach (var item in Items) //searching for specific Item
			{
				if (item.Name == name)
				{
					return item.Count;
				}

			}
			return -1; //because there are no Items with that name
		}



	}
}

