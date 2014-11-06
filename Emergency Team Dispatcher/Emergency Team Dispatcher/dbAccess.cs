using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Emergency_Team_Dispatcher
{
	static class dbAccess
	{
		private static string server = "127.0.0.1";
		private static string database = "etd";
		private static string uid = "csharp";
		private static string password = "csharp";
		private static MySqlConnection connection = new MySqlConnection("SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + "PASSWORD=" + password + ";");

		//Connection to the database
		public static String OpenConnection()
		{
			try
			{
				connection.Open();
				Console.WriteLine("test");
			}
			catch(MySqlException ex)
			{
				//Possibility to add 
				switch(ex.ErrorCode)
				{
					case 0:
						return "No connection";
					case 1045:
						return "Invalid cred";
				}
			}
			return "pass";
		}

		//Closing of the connection
		private static bool CloseConnection()
		{
			try
			{
				connection.Close();
			}
			catch(MySqlException ex)
			{
				//Possibility to add displaying of the Message
				return false;
			}
			return true;
		}

		private static void ExecuteNonQuery(String query)
		{
			if(OpenConnection().Equals("pass"))
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				cmd.ExecuteNonQuery();
				CloseConnection();
			}
		}

		private static void ExecuteQuery(String query)
		{

		}

		private static int getID(String query)
		{
			if(OpenConnection().Equals("pass"))
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				int id = Convert.ToInt32(cmd.ExecuteScalar());
				CloseConnection();
				return id;
			}
			return -1;
		}

		private static String now()
		{
			DateTime now = DateTime.Now;
			string current = now.ToString("dd/MM/yy HH:mm");
			return current;
		}

		public static int insertTeam(String name)
		{
			ExecuteNonQuery("INSERT INTO teams(name,creation) VALUES('" + name + "', '" + now() + "')");
			return getID("SELECT last_insert_id();");
		}

		/*public static void insertMember(String name, String timeDeparture, )
		{

		}*/
	}
}
