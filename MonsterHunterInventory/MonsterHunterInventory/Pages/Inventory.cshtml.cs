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
        private Inventory inventory = new Inventory();
        private List<Item> items = new List<Item>();
        public List<Item> allItems = new List<Item>();


        public void OnGet()
        {
            //Creating instance of Inventory class for the blocks
            items = new Inventory().Items;
            foreach (var item in items)
            {
                item.ItemCount = new Inventory().GetTotalItemCount(item.ID);
                item.HomebaseCount = inventory.GetLocationItemCount(1, item.ID);
                item.BunkerCount = inventory.GetLocationItemCount(2, item.ID);
                item.PouchCount = inventory.GetLocationItemCount(3, item.ID);
                allItems.Add(item);
            }


        }


        public RedirectToPageResult OnPostHomebase(int itemId, int count)
        {
            Console.WriteLine($"{itemId} should change to {count}");
            new Inventory().UpdateHomebaseCount(itemId, count);

            return RedirectToPage("./Inventory");
        }

        public RedirectToPageResult OnPostBunker(int itemId, int count)
        {
            Console.WriteLine($"{itemId} should change to {count}");
            new Inventory().UpdateBunkerCount(itemId, count);

            return RedirectToPage("./Inventory");
        }

        public RedirectToPageResult OnPostPouch(int itemId, int count)
        {
            Console.WriteLine($"{itemId} should change to {count}");
            new Inventory().UpdatePouchCount(itemId, count);

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