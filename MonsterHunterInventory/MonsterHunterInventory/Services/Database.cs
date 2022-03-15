using System;
using System.Collections.Generic;
using MonsterHunterInventory.Models;
using MySql.Data.MySqlClient;

namespace MonsterHunterInventory.Services
{
	public class Database
	{
		//configuration to connect to our localhost database
		private static string serverConfiguration = @"server=localhost;userid=root;password=root;database=monsterhunter;port=8889;";

		public static string GetVersion()
		{
			//Creating a new connection to dtabase using our config and NuGet package
			using var con = new MySqlConnection(serverConfiguration);

			con.Open();

			return con.ServerVersion;
		}

		//Get all of items
		public static List<Item> GetAllItems()
		{
			//Create and open our db collection
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in 

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			//init our return list
			var results = new List<Item>();

			while (reader.Read())
			{
                var item = new Item(reader.GetInt32(3))
				{
					IngredientType = reader.GetString(0),
					GroupType = reader.GetString(1),
					Name = reader.GetString(2),
					Location = reader.GetString(3),
					ImageURL = reader.GetString(4),
					Description = reader.GetString(5),
					
				};

				results.Add(item);
			}

			return results;
		}

		public static void UpdateItemCount(string name, int newCount)
		{

			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			string sql = $"UPDATE `items` SET `count` = @count WHERE `name` = @name";
			using var cmd = new MySqlCommand(sql, con);

			cmd.Parameters.AddWithValue("@name", name);
			cmd.Parameters.AddWithValue("@count", newCount);

			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}

	}
}

