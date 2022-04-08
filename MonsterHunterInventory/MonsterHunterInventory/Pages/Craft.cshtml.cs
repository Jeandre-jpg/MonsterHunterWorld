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

        private ProductBook productBook = new ProductBook();
        private List<Product> list = new List<Product>();
        public List<Product> allProducts = new List<Product>();

        public string Message { get; set; } = string.Empty;

        public void OnGet(string message = "")
        {

            list = new ProductBook().Products;
            foreach(var product in list)
            {
                product.ItemCount = new ProductBook().GetCountOfProduct(product.ID);
                product.isCraftable = productBook.GetCountOfItem(product.ItemOneId) > 0 && productBook.GetCountOfItem(product.ItemTwoId) > 0;
                allProducts.Add(product);
            }

        }

        public RedirectResult CraftProduct(int productId, int count, List<int> ingredients)
        {
            //TODO: Call the model function and pass the name, updated count and ingredients
            new ProductBook().CraftProduct(productId, count + 1, ingredients);

            return Redirect("./Craft");

        }
    }
}