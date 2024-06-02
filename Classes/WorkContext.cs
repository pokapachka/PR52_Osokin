using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ПР52_Осокин.Common;
using ПР52_Осокин.Models;

namespace ПР52_Осокин.Classes
{
    public class WorkContext : Work
    {
        public WorkContext(int Id, int IdDiscipline, int IdType, DateTime Date, string Name, int Semester) : base(Id, IdDiscipline, IdType, Date, Name, Semester) { }

        public static List<WorkContext> AllWorks()
        {
            List<WorkContext> allWorks = new List<WorkContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Work = Connection.Query("Select * From `work` Order By `Date`;", connection);
            while (Work.Read())
            {
                allWorks.Add(new WorkContext(
                    Work.GetInt32(0),
                    Work.GetInt32(1),
                    Work.GetInt32(2),
                    Work.GetDateTime(3),
                    Work.GetString(4),
                    Work.GetInt32(5)));
            }
            Connection.CloseConnection(connection);
            return allWorks;
        }
    }
}
