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
                var item = new Item(reader.GetInt32(5))
				{
					IngredientType = reader.GetString(0),
					GroupType = reader.GetString(1),
					Name = reader.GetString(2),
					Location = reader.GetString(3),
					ImageURL = reader.GetString(4),
					Description = reader.GetString(6),
					
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


		//Get all of recipes
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
				var recipe = new Product(reader.GetInt32(2))
				{
					Name = reader.GetString(0), //0 = index of our colum number
					RecipeType = reader.GetString(1),
				};

				var ingredients = new List<string>();

				ingredients.Add(reader.GetString(3)); //Ingredient 1
				ingredients.Add(reader.GetString(4)); //Ingredient 2
				ingredients.Add(reader.GetString(5)); //Ingredient 4
				ingredients.Add(reader.GetString(6)); //Ingredient 5
				ingredients.Add(reader.GetString(7)); //Ingredient 6
				ingredients.Add(reader.GetString(8)); //Ingredient 7
				ingredients.Add(reader.GetString(9)); //Ingredient 8
				ingredients.Add(reader.GetString(10)); //Ingredient 9

				recipe.Ingredients = ingredients;

				results.Add(recipe);
			}

			return results;
		}

		public static void CraftProduct(string nameId, int newCount, List<string> ingredients)
		{

			UpdateBlockCountAfterCraft(ingredients);

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
					int currentCount = GetCountOfBlock(ingredient);

					//sql query
					string sql = $"UPDATE `blocks` SET `count` = @count WHERE `name` = @name";
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

		public static int GetCountOfBlock(string name)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT count FROM blocks WHERE name = @name";
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



