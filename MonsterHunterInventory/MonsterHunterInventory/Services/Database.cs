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

		//Get all of Locations
		public static List<Location> GetAllLocations()
		{
			//Create and open our db collection
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM locations";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in 

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			//init our return list
			var results = new List<Location>();

			while (reader.Read())
			{
				var item = new Location()
				{
					ID = reader.GetInt32(0),
					Name = reader.GetString(1),
					ImageURL = reader.GetString(2),
					Description = reader.GetString(3),

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

			if (GetLocationItemCount(2, itemId) > 0)
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

			// Insert into db 
			string sql = $"INSERT INTO `itemLocationCount`(`locationId`, `itemId`, `totalCount`) VALUES (3,@itemId,@count)";

			// If items already exist update existing data
			if (GetLocationItemCount(3, itemId) > 0)
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
				var product = new Product(reader.GetInt32(0))
				{
					ID = reader.GetInt32(0),
					Name = reader.GetString(1),
					ImageURL = reader.GetString(2),
					ProductType = reader.GetString(3),
					Description = reader.GetString(4),
					ItemOne = reader.GetString(6),
					ItemTwo = reader.GetString(7),
					ItemOneId = reader.GetInt32(8),
					ItemTwoId = reader.GetInt32(9)

				};

				var ingredients = new List<string>();

				ingredients.Add(GetProductIngredients(product.ItemOneId)); //Ingredient 1
				ingredients.Add(GetProductIngredients(product.ItemTwoId)); //Ingredient 2
				
				product.Ingredients = ingredients;

				results.Add(product);
			}

			return results;
		}

		public static int GetCountOfProduct(int productId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = $"SELECT SUM(totalCount) FROM productLocationCount WHERE `productId` = @productId";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@productId", productId);

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

		public static void CraftProduct(int productId, int newCount, List<int> ingredients)
		{


			UpdateItemCountAfterCraft(ingredients, 1);

            //establish connection to db
            using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//sql query
			//sql query
			string sql = $"INSERT INTO `productLocationCount`(`locationId`, `productId`, `totalCount`) VALUES (1,@productId,@count)";

			if (GetLocationProductCount(1, productId) > 0)
			{
				sql = $"UPDATE `productLocationCount`SET `locationId`=1, `productId`=@productId, `totalCount`=@count WHERE `locationId` = 1 AND `productId` = @productId";
			}
			using var cmd = new MySqlCommand(sql, con);

			//adding the actual values by replacing the @ placeholders
			cmd.Parameters.AddWithValue("@productId", productId);
			cmd.Parameters.AddWithValue("@count", newCount);

			//prepare command
			cmd.Prepare();
			//exucute command
			cmd.ExecuteNonQuery();// Non query = Because we don't want to get a query value back

			//TODO: Remove the ingredients
		}



		public static void UpdateItemCountAfterCraft(List<int> ingredients, int locationId)
		{
			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			foreach (int ingredient in ingredients)
			{
				
				int currentCount = GetCountOfItem(ingredient);

                //sql query
                string sql = $"UPDATE `itemLocationCount` SET `totalCount` = @count WHERE `locationId` = @locationId AND itemId = @itemId";
				using var cmd = new MySqlCommand(sql, con);

				//adding the actual values by replacing the @ placeholders
				cmd.Parameters.AddWithValue("@itemId", ingredient);
				cmd.Parameters.AddWithValue("@locationId", locationId);
				cmd.Parameters.AddWithValue("@count", currentCount - 1);

                //prepare command
                cmd.Prepare();
				//exucute command
				cmd.ExecuteNonQuery();// Non query = Because we don't want to get a query value back
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

		public static String GetProductIngredients(int itemId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();
			//SELECT products.id, products.name,products.img, products.producttype, products.description, itemsListOne.name as item1, itemsListTwo.name as item2
			//FROM((products INNER JOIN items itemsListOne ON itemsListOne.id = products.itemOneId) INNER JOIN items itemsListTwo ON itemsListTwo.id = products.itemTwoId)

			//setup our query
			string sql = $"SELECT items.name FROM products INNER JOIN items ON items.id = @itemId";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@itemId", itemId);

			//Creates an instance of our cmd result that can be read in C#
			using MySqlDataReader reader = cmd.ExecuteReader();

			String homebasecount = String.Empty;

			while (reader.Read())
			{
				homebasecount = reader.GetString(0);

			}

			con.Close();



			return homebasecount;

		}

		public static int GetLocationProductCount(int locationId, int productId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = $"SELECT `totalCount` FROM productLocationCount WHERE productId = @productId AND locationId = @locationId";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@productId", productId);
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

		public static List<Item> GetItemsForLocation(int locationId)
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = $"SELECT * FROM items INNER JOIN itemLocationCount ON items.id = itemLocationCount.itemId WHERE `locationId` = @locationId AND itemLocationCount.totalCount > 0";
			using var cmd = new MySqlCommand(sql, con); //preform this new cmd which is sql & do it in

			cmd.Parameters.AddWithValue("@locationId", locationId);
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
					GroupType = reader.GetString(5),
					ItemCount = reader.GetInt32(8)

				};

				results.Add(item);
			}

			return results;

		}

		public static List<Item> GetAllHomeBaseItems()
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items INNER JOIN itemLocationCount ON items.id = itemLocationCount.itemId WHERE `locationId` = 1 AND itemLocationCount.totalCount > 0";
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


		public static List<Item> GetAllPouchItems()
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items INNER JOIN itemLocationCount ON items.id = itemLocationCount.itemId WHERE `locationId` = 2 AND itemLocationCount.totalCount > 0";
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


		

		public static List<Item> GetAllBunkerItems()
		{

			//establish connection to db
			using var con = new MySqlConnection(serverConfiguration);
			con.Open();

			//setup our query
			string sql = "SELECT * FROM items INNER JOIN itemLocationCount ON items.id = itemLocationCount.itemId WHERE `locationId` = 3 AND itemLocationCount.totalCount > 0";
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


