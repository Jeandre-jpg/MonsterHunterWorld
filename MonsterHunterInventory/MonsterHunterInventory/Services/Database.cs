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
		public static List<Item>GetAllItems()
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
				var item = new Item(reader.GetInt32(4))
				{
					Name = reader.GetString(0),
					ImageURL = reader.GetString(1),
					ItemType = reader.GetString(2),
					Description = reader.GetString(3),
					GroupType = reader.GetString(4),
				};

				results.Add(item);
			}

			return results;
		}

		public static void UpdateItemCount(string name, int newCount)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			string sql = $"UPDATE `items` SET `count` = @count WHERE `name` = @name";
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@name", name);
			cmd.Parameters.AddWithValue("@count", newCount);

			//prepare command
			cmd.Prepare();
			//exucute command
			cmd.ExecuteNonQuery();
		}

		//Get all of products
		public static List<Product> GetAllProducts()
		{
			//Create and open our db collection
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM products";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in 

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			//init our return list
			var results = new List<Product>();

			while (reader.Read())
			{
				var product = new Product(reader.GetInt32(4))
				{
					Name = reader.GetString(0),
					ImageURL = reader.GetString(1),
					ProductType = reader.GetString(2),
					Description = reader.GetString(3)

				};

				var ingredients = new List<string>();

				ingredients.Add(reader.GetString(5)); //Ingredient 1
				ingredients.Add(reader.GetString(6)); //Ingredient 2
				
				product.Ingredients = ingredients;

				results.Add(product);
			}

			return results;
		}

		public static void CraftProduct(string nameId, int newCount, List<string> ingredients)
		{

			UpdateItemCountAfterCraft(ingredients);

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			string sql = $"UPDATE `products` SET `count` = @count WHERE `name` = @name";
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@name", nameId);
			cmd.Parameters.AddWithValue("@count", newCount);

			//prepare command
			cmd.Prepare();
			//exucute command
			cmd.ExecuteNonQuery();// Non query = Because we don't want to get a query value back

			//TODO: Remove the ingredients
		}



		public static void UpdateItemCountAfterCraft(List<string> ingredients)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			foreach (string ingredient in ingredients)
			{

				if (ingredient != "")
				{
					int currentCount = GetCountOfItem(ingredient);

					//sql query
					string sql = $"UPDATE `items` SET `count` = @count WHERE `name` = @name";
					using var cmd = new MySqlCommand(sql, con);

					//adding the actual values by replacing the @ placeholders
					cmd.Parameters.AddWithValue("@name", ingredient);
					cmd.Parameters.AddWithValue("@count", currentCount - 1);

					//prepare command
					cmd.Prepare();
					//exucute command
					cmd.ExecuteNonQuery();// Non query = Because we don't want to get a query value back
				}
			}
		}

		public static int GetCountOfItem(string name)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT count FROM items WHERE name = @name";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@name", name);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int count = 0;

			//adding the actual values by replacing the @ placeholders


			while (reader.Read())
			{
				count = reader.GetInt32(0);

			}

			con.Close();



			return count;

		}

	}

}


