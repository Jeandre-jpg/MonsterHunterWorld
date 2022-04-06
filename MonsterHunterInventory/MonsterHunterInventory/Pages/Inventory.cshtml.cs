using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterHunterInventory.Models;

namespace MonsterHunterInventory.Pages
{

    public class InventoryModel : PageModel
    {

        //Block[] blockArray = { new Glass(1), new Sand(1), new Coal(1), new Wood(1) };

        public List<Item> allItems = new List<Item>();

        public void OnGet()
        {
            //Creating instance of Inventory class for the blocks
            allItems = new Inventory().Items;


        }


        public RedirectToPageResult OnPostHomebase(string name, int count)
        {
            Console.WriteLine($"{name} should change to {count}");
            new Inventory().UpdateHomebaseCount(name, count);

            return RedirectToPage("./Inventory");
        }

        public RedirectToPageResult OnPostBunker(string name, int count)
        {
            Console.WriteLine($"{name} should change to {count}");
            new Inventory().UpdateBunkerCount(name, count);

            return RedirectToPage("./Inventory");
        }

        public RedirectToPageResult OnPostPouch(string name, int count)
        {
            Console.WriteLine($"{name} should change to {count}");
            new Inventory().UpdatePouchCount(name, count);

            return RedirectToPage("./Inventory");
        }




        //public RedirectToPageResult OnPostUpdate(string name, int count)
        //{
        //    Console.WriteLine($"{name} should change to {count}");
        //    new Inventory().UpdateHomebaseCount(name, count);

        //    return RedirectToPage("./Inventory");
        //}
    }
}