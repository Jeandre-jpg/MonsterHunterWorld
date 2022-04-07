using System;
using System.Collections.Generic;
using System.Configuration;
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

        internal static void UpdatePlaceCount(string name, int count)
        {
            throw new NotImplementedException();
        }

        //Get all of items
        public static List<Item>GetAllItems()
		{
			//Create and open our db collection
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items ";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in 

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			//init our return list
			var results = new List<Item>();

			while (reader.Read())
			{
				var item = new Item(reader.GetInt32(0))
				{
					ID = reader.GetInt32(0),
					Name = reader.GetString(1),
					ImageURL = reader.GetString(2),
					ItemType = reader.GetString(3),
					Description = reader.GetString(4),
					GroupType = reader.GetString(5)
					
				};

				results.Add(item);
			}

			return results;
		}


		//public static void UpdateItemCount(string name, int newCount)
		//{
		//	//establish connection to db
		//	using var con = new MySqlConnection(serverConfiguration);
		//	con.Open();

		//	//sql query
		//	string sql = $"UPDATE `items` SET `count` = @count WHERE `name` = @name";
		//	using var cmd = new MySqlCommand(sql, con);

		//	//adding the actual values by replacing the @ placeholders
		//	cmd.Parameters.AddWithValue("@name", name);
		//	cmd.Parameters.AddWithValue("@count", newCount);

		//	//prepare command
		//	cmd.Prepare();
		//	//exucute command
		//	cmd.ExecuteNonQuery();
		//}


		public static void UpdateHomebaseCount(int itemId, int newCount)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			string sql = $"INSERT INTO `itemLocationCount`(`locationId`, `itemId`, `totalCount`) VALUES (1,@itemId,@count)";
			
			if (GetLocationItemCount(1, itemId) > 0)
            {
				sql = $"UPDATE `itemLocationCount`SET `locationId`=1, `itemId`=@itemId, `totalCount`=@count WHERE `locationId` = 1 AND `itemId` = @itemId"; ;
			}
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@itemId", itemId);
			cmd.Parameters.AddWithValue("@count", newCount);

			//prepare command
			cmd.Prepare();
			//exucute command
			cmd.ExecuteNonQuery();
		}


		public static void UpdateBunkerCount(int itemId, int newCount)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			string sql = $"INSERT INTO `itemLocationCount`(`locationId`, `itemId`, `totalCount`) VALUES (2,@itemId,@count)";

			if (GetLocationItemCount(1, itemId) > 0)
			{
				sql = $"UPDATE `itemLocationCount`SET `locationId`=1, `itemId`=@itemId, `totalCount`=@count WHERE `locationId` = 2 AND `itemId` = @itemId"; ;
			}
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@itemId", itemId);
			cmd.Parameters.AddWithValue("@count", newCount);

			//prepare command
			cmd.Prepare();
			//exucute command
			cmd.ExecuteNonQuery();
		}



		public static void UpdatePouchCount(int itemId, int newCount)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			string sql = $"INSERT INTO `itemLocationCount`(`locationId`, `itemId`, `totalCount`) VALUES (3,@itemId,@count)";

			if (GetLocationItemCount(1, itemId) > 0)
			{
				sql = $"UPDATE `itemLocationCount`SET `locationId`=1, `itemId`=@itemId, `totalCount`=@count WHERE `locationId` = 3 AND `itemId` = @itemId"; ;
			}
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@itemId", itemId);
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
					Description = reader.GetString(3),
					ItemOne = reader.GetString(5),
					ItemTwo = reader.GetString(6),
					ItemOneCount = reader.GetInt32(7),
					ItemTwoCount = reader.GetInt32(8)

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
					//int currentCount = GetCountOfItem(ingredient);

					//sql query
					string sql = $"UPDATE `items` SET `count` = @count WHERE `name` = @name";
					using var cmd = new MySqlCommand(sql, con);

					//adding the actual values by replacing the @ placeholders
					cmd.Parameters.AddWithValue("@name", ingredient);
					//cmd.Parameters.AddWithValue("@count", currentCount - 1);

					//prepare command
					cmd.Prepare();
					//exucute command
					cmd.ExecuteNonQuery();// Non query = Because we don't want to get a query value back
				}
			}
		}

		public static int GetCountOfItem(int itemId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = $"SELECT SUM(totalCount) FROM itemLocationCount WHERE `itemId` = @itemId";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@itemId", itemId);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int count = 0;

			//adding the actual values by replacing the @ placeholders


			while (reader.Read())
			{
				if (reader.GetValue(0) != DBNull.Value)
                {
					count = reader.GetInt32(0);
				}
				
			}

			con.Close();



			return count;

		}

		public static int GetLocationItemCount(int locationId, int itemId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = $"SELECT `totalCount` FROM itemLocationCount WHERE `itemId` = @itemId AND `locationId` = @locationId";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@itemId", itemId);
			cmd.Parameters.AddWithValue("@locationId", locationId);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int count = 0;

			//adding the actual values by replacing the @ placeholders


			while (reader.Read())
			{
				if (reader.GetValue(0) != DBNull.Value)
				{
					count = reader.GetInt32(0);
				}

			}

			con.Close();



			return count;

		}

		public static int GetAllHomeBaseItems(string name)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items WHERE homebasecount > 0;";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@name", name);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int homebasecount = 0;

			while (reader.Read())
			{
				homebasecount = reader.GetInt32(7);

			}

			con.Close();



			return homebasecount;

		}


		public static int GetAllPouchItems(string name)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items WHERE pouchcount > 0;";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@name", name);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int pouchcount = 0;

			while (reader.Read())
			{
				pouchcount = reader.GetInt32(6);

			}

			con.Close();



			return pouchcount;

		}


		

		public static int GetAllBunkerItems(string name)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items WHERE bunkercount > 0;";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@name", name);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			int bunkercount = 0;

			while (reader.Read())
			{
				bunkercount = reader.GetInt32(8);

			}

			con.Close();



			return bunkercount;

		}



		//protected void btnItem(object sender, EventArgs e)
		//{
		//    string query = string.Format("SELECT * FROM items WHERE id = 1 AND img = {0}");
		//    BindGridView(query);
		//}
		//public void BindGridView(string query)
		//{
		//    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
		//    using (MySqlConnection con = new MySqlConnection(constr))
		//    {
		//        using (MySqlDataAdapter sda = new MySqlDataAdapter(query, con))
		//        {
		//            using (DataTable dt = new DataTable())
		//            {
		//                sda.Fill(dt);
		//                GridView1.DataSource = dt;
		//                GridView1.DataBind();
		//            }
		//        }
		//    }
		//}

		//public static int GetHomebaseOfItem(string name)
		//{

		//	//establish connection to db
		//	using var con = new MySqlConnection(serverConfiguration);
		//	con.Open();

		//	//setup our query
		//	string sql = "SELECT homebase FROM items WHERE name = @name";
		//	using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

		//	cmd.Parameters.AddWithValue("@name", name);

		//	//Creates an instance of our cmd result that can be read in C#
		//	using MySqlDataReader reader = cmd.ExecuteReader();

		//	int homebase = 0;

		//	//adding the actual values by replacing the @ placeholders


		//	while (reader.Read())
		//	{
		//		homebase = reader.GetInt32(7);

		//	}

		//	con.Close();



		//	return homebase;

		//}





		//	public static List<ItemType> GetAllArtificial()
		//{
		//	//Create and open our db collection
		//	using var con = new MySqlConnection(serverConfiguration);
		//	con.Open();

		//	//setup our query
		//	string sql = "SELECT * FROM items WHERE itemtype = `Artificial`";
		//	using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in 

		//	//Creates an instance of our cmd result that can be read in C#
		//	using MySqlDataReader reader = cmd.ExecuteReader();

		//	//init our return list
		//	var results = new List<Item>();

		//	while (reader.Read())
		//	{
		//		var product = new Item(reader.GetInt32(4))
		//		{
		//			Name = reader.GetString(0),
		//			ImageURL = reader.GetString(1),
		//			ProductType = reader.GetString(2),
		//			Description = reader.GetString(3)

		//		};

		//		var ingredients = new List<string>();

		//		ingredients.Add(reader.GetString(5)); //Ingredient 1
		//		ingredients.Add(reader.GetString(6)); //Ingredient 2

		//		product.Ingredients = ingredients;

		//		results.Add(product);
		//	}

		//	return results;
		//}





		//Get all of products
		//public static List<Item> GetAllHomebase(string name)
		//{

		//	//establish connection to db
		//	using var con = new MySqlConnection(serverConfiguration);
		//	con.Open();

		//	//setup our query
		//	string sql = "SELECT COUNT (homebasecount) FROM items WHERE homebasecount > 0; ";
		//	using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

		//	cmd.Parameters.AddWithValue("@homebasecount", name);

		//	//Creates an instance of our cmd result that can be read in C#
		//	using MySqlDataReader reader = cmd.ExecuteReader();



		//	con.Close();



		//	return homebasecount;

		//}
	}

}


