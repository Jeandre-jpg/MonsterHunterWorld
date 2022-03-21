﻿using System;
using System.Collections.Generic;
using MonsterHunterInventory.Interfaces;

namespace MonsterHunterInventory.Models
{
	public class Product : Craftable
	{
		//recipe constructor to see count
		public Product(int newCount)
		{
			count = newCount;
		}

		//recipe constructor
		public string Name { get; set; } = string.Empty;

		public string ProductType { get; set; } = string.Empty;

		private int count;

		public int Count { get { return count; } }

		//TODO: Add Ingredient Property
		public List<string> Items { get; set; }


		//interface method
		public bool IsCraftable()
		{
			//Check if we have all the required ingredients
			//Setup empty dictionary which will contain the item and the amount we need
			//STEP 1: Create dictionary of all ingredients we need and how many of each
			var map = new Dictionary<string, int>();

			foreach (var block in Items)
			{
				if (block != "")//check if null
				{

					int count;
					if (map.TryGetValue(block, out count)) //have we added this ingredient before?
					{
						//if yes = increment the count
						map[block] += 1;
					}
					else // if we haven't added the ingredient to the dictionary
					{
						// add the block to it
						map.Add(block, 1);
					}

				}
			}




			bool result = true;

			//STEP 2: Check if we have the required amount of each ingredient
			//go create our inventory that we check our values in
			var inventory = new Inventory();

			foreach (var pair in map)
			{
				if (pair.Value > inventory.GetCount(pair.Key))
				{
					result = false;
				}
			}


			//TODO: Check if we have all the required ingredients
			return result;

		}
	}

}