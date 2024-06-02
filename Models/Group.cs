using System;
using System.Collections.Generic;
using System.Text;

namespace ПР52_Осокин.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
