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











            //    //Create each instance and content

            //    Block myGlass = new Glass(25)
            //    {
            //        ID = 1,
            //        Name = "Glass Block",
            //        ImageURL = "/"
            //    };

            //    Block myCoal = new Coal(25)
            //    {
            //        ID = 2,
            //        Name = "Coal Block",
            //        ImageURL = "/"
            //    };

            //    Block myWood = new Wood(25)
            //    {
            //        ID = 3,
            //        Name = "Wood Block",
            //        ImageURL = "/"
            //    };

            //    Block mySand = new Sand(25)
            //    {
            //        ID = 4,
            //        Name = "Sand Block",
            //        ImageURL = "/"
            //    };

            //    allBlocks.Add(myGlass);
            //    allBlocks.Add(myCoal);
            //    allBlocks.Add(myWood);
            //    allBlocks.Add(mySand);










            //Coal myCoal = new Coal(12);
            //Block mySand = new Sand(22);
            //Block myGlass = new Glass(53);

            //myCoal.place();
            //mySand.place();
            //myGlass.place();

            //Flamable myFlamable = new Wood(15);
            //Block myWood = new Wood(72);

            //myFlamable.Burn();
            //myWood.Burn();

            //foreach(Block block in blockArray)
            //{
            //    try
            //    {
            //        Flamable flame = (Flamable)block;
            //        flame.Burn();
            //    }
            //    catch(Exception ex)
            //    {
            //        Console.WriteLine("This block is not flammable: " + Block.ToSting());
            //    }
            //}

            //var myNewCoal = myWood.Melt();
            //Console.WriteLine($"My New Coal Count: {myNewCoal.Count}");

            //Block newWood = new Wood(1);
            //Flamable newFlamable = (Flamable)newWood;
            //Meltable myMeltable = (Meltable)myWood;



            //public int number;
            //
            //{
            //    //var myBlock = new Blocks(30);

            //    var myWood = new Wood(45);
            //    var myGlass = new Glass(22);


            //    number = myWood.Count;




            //    //myBlock.place();
            //    //myBlock.destroy();


            // }


        }


        public RedirectToPageResult OnPostUpdate(string name, int count)
        {
            Console.WriteLine($"{name} should change to {count}");
            new Inventory().UpdateCount(name, count);

            return RedirectToPage("./Inventory");
        }
    }
}