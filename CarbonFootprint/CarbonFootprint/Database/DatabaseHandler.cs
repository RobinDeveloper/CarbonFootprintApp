using System;
using System.Collections.Generic;
using System.Text;
using MySql;
using MySql.Data;

namespace CarbonFootprint.Database
{
	public static class DatabaseHandler
	{

		private const string CONNSTRING = "Server=studmysql01.fhict.local; Uid=dbi470506; Database=dbi470506; Pwd=CarbonFootprintApp;";

		public static void PushUserData(string username, string password)
		{
			MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(CONNSTRING);
			connection.Open();

			string queryString = String.Format("INSERT INTO users(name, password)" +
									"VALUES('{0}', '{1}')");

			MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(queryString, connection);

			command.ExecuteNonQuery();

			connection.Close();
			connection.Dispose();
		}
	}
}
