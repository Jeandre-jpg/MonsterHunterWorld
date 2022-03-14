using System;
namespace MonsterHunterInventory.Models
{


 public class Potion
{

    //Fields

    //public int ID { get; set;}
    public int ID { get; set; }

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

    public Potion(int newCount)
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

    public void destroy()
    {
        Console.WriteLine("Potion Destroyed");
        count = 0;
    }

    internal static string ToSting()
    {
        throw new NotImplementedException();
    }




}
}
