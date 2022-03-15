using System;
namespace MonsterHunterInventory.Models
{


 public class Item
{

    //Fields

    //public int ID { get; set;}
    public int ID { get; set; }

    public string IngredientType { get; set; } = string.Empty;

    public string GroupType { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string ImageURL { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int count;


    public int Count
    {
        get
        {

            //end product
            return count;
        }

        set
        {

            //functionality
            if (value > 0)
                count = value;
            else
                count = 0;
        }
    }

    //constructor

    public Item(int newCount)
    {
        count = newCount;

    }

    internal void Craft()
    {
        throw new NotImplementedException();
    }

    //internal object Craft()
    //{
    //    throw new NotImplementedException();
    //}



    public virtual void place()
    {
        Console.WriteLine($"{Name} has been added");
    }

        //new Ingredient()
        //{
        //    ingredientType = "potion"
        //}

        public void destroy()
    {
        Console.WriteLine($"{Name} Destroyed");
        count = 0;
    }

    internal static string ToSting()
    {
        throw new NotImplementedException();
    }




}
}
