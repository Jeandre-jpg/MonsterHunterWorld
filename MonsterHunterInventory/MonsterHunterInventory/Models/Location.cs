using System;
using System.Collections.Generic;
using MonsterHunterInventory.Services;

namespace MonsterHunterInventory.Models
{
	public class Location


	{
        public List<Item> allItems = new List<Item>();

        public void OnGet()
        {
            //Creating instance of Inventory class for the blocks
            allItems = new Inventory().Items;


        }

        //public void UpdateCount(string name, int count)
        //{
        //    Database.UpdatePlaceCount(name, count);

        //}

        ////function to check how many items there are

        //public int GetCount(string name)
        //{
        //    foreach (var place in Locations) //searching for specific Place
        //    {
        //        if (place.Name == name)
        //        {
        //            return place.Count;
        //        }

        //    }
        //    return -1; //because there are no Items with that name
        //}


    }
}

