using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ПР52_Осокин.Common
{
    public class Connection
    {
        public static string config = "server=localhost;port=3307;uid=root;database=journal;";

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(config);
            connection.Open();
            return connection;
        }
        public static MySqlDataReader Query(string SQL, MySqlConnection connection)
        {
            new MySqlCommand(SQL, connection).ExecuteReader();
        }
        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
