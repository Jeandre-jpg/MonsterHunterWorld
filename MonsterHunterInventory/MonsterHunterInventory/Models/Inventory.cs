﻿using System.Collections.Generic;
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

		public void UpdateCount(string name, int count)
		{
			Database.UpdateItemCount(name, count);

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

