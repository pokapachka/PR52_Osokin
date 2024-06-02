﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ПР52_Осокин.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        public int IdWork { get; set; }
        public int IdStudent { get; set; }
        public string Value { get; set; }
        public string Lateness { get; set; }

        public Evaluation(int Id, int IdWork, int IdStudent, string Value, string Lateness)
        {
            this.Id = Id;
            this.IdWork = IdWork;
            this.IdStudent = IdStudent;
            this.Value = Value;
            this.Lateness = Lateness;
        }
    }
}
