using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using ПР52_Осокин.Common;
using ПР52_Осокин.Models;

namespace ПР52_Осокин.Classes
{
    public class EvaluationContext : Evaluation
    {
        public EvaluationContext(int Id, int IdWork, int IdStudent, string Value, string Lateness) : base(Id, IdWork, IdStudent, Value, Lateness) { }

        public static List<EvaluationContext> AllEvaluations()
        {
            List<EvaluationContext> allEvaluations = new List<EvaluationContext>();
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Evaluation = Connection.Query("Select * From `evaluation`;", connection);
            while (Evaluation.Read())
            {
                allEvaluations.Add(new EvaluationContext(
                    Evaluation.GetInt32(0),
                    Evaluation.GetInt32(1),
                    Evaluation.GetInt32(2),
                    Evaluation.GetString(3),
                    Evaluation.GetString(4)));
            }
            Connection.CloseConnection(connection);
            return allEvaluations;
        }
    }
}
