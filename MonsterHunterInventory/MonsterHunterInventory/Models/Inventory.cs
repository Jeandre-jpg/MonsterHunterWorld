using System;
namespace MonsterHunterInventory.Models
{
	public class Inventory
	{

		public List<Potion> Potions = new List<Potion>();


		//constructor is going to fetch our database
		public Inventory()
		{

			Potions = Database.GetAllPotions();
		}

		public void UpdateCount(string name, int count)
		{
			Database.UpdatePotionCount(name, count);

		}
	}
}
