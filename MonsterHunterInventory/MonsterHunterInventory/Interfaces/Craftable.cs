using System;
using MonsterHunterInventory.Models;

namespace MonsterHunterInventory.Interfaces
{
	public interface Craftable
	{
		//rule that all classes that inherit from this, needs this IsCraftable function
		bool IsCraftable();

	}
}

