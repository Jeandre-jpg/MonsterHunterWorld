using System;
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

		public void UpdateCount(string name, int count)
		{
			Database.UpdateItemCount(name, count);

		}
	}
}
