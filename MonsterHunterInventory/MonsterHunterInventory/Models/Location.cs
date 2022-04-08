﻿using System;
using System.Collections.Generic;
using MonsterHunterInventory.Services;

namespace MonsterHunterInventory.Models
{
	public class Location


	{
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Item> items { get; set; }

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

