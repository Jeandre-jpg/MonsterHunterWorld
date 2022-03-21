using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonsterHunterInventory.Models;

namespace MonsterHunterInventory.Pages
{

public class CraftModel : PageModel
{


    public List<Product> allProducts = new List<Product>();

    public string Message { get; set; } = string.Empty;

    public void OnGet(string message = "")
    {

            allProducts = new CraftedItems().Products;

    }

    public void OnPostCraft(string name, int count, List<string> items)
    {
        //TODO: Call the model function and pass the name, updated count and ingredients
        new CraftedItems().CraftProduct(name, count + 1, items);

        return Redirect($"./Crafting?message={name} has been crafted");

    }
}
}