using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ПР52_Осокин.Common;
using ПР52_Осокин.Models;

namespace ПР52_Осокин.Classes
{
    public class GroupContext : Group
    {
        public GroupContext(int Id, string Name) : base(Id, Name) { }

        public static List<GroupContext> AllGroups()
        {
            List<GroupContext> allGroups = new List<GroupContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Group = Connection.Query("Select * From `group` Order By `Name`;", connection);
            while (Group.Read())
            {
                allGroups.Add(new GroupContext(
                    Group.GetInt32(0),
                    Group.GetString(1)));
            }
            Connection.CloseConnection(connection);
            return allGroups;
        }
    }
}
