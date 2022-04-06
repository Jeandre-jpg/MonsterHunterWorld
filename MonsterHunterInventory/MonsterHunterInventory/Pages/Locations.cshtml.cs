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
        public List<Item> AllHomeBaseItems = new List<Item>();
        public List<Item> AllPouchItems = new List<Item>();
        public List<Item> AllBunkerItems = new List<Item>();
        public void OnGet()
        {
            //Creating instance of Inventory class for the blocks
            AllHomeBaseItems = new Inventory().Items;
            AllPouchItems = new Inventory().Items;
            AllBunkerItems = new Inventory().Items;
        }

       

    }
}