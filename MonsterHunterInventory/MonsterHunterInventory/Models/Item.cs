using System;
namespace MonsterHunterInventory.Models
{


public class Item
{

    //Fields

    //public int ID { get; set;}

    public int ID { get; set; }

    public string ItemType { get; set; } = string.Empty;

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


    public virtual void place()
    {
        Console.WriteLine($"{Name} has been placed");
    }

    public void destroy()
    {
        Console.WriteLine("Item Destroyed");
        count = 0;
    }

    internal static string ToSting()
    {
        throw new NotImplementedException();
    }




}
}
