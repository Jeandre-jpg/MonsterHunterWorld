using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterHunterInventory.Models;

namespace MonsterHunterInventory.Pages
{
    public class LocationsModel : PageModel
    {
        private List<Location> locationList = new List<Location>();
        public List<Location> allLocations = new List<Location>();

        public void OnGet()
        {
            //Creating instance of Inventory class for the blocks
            locationList = new Locations().allLocations;
            foreach(var location in locationList)
            {
                location.items = new Locations().GetItemsForLocation(location.ID);
                allLocations.Add(location);
            }
        }

       

    }
}