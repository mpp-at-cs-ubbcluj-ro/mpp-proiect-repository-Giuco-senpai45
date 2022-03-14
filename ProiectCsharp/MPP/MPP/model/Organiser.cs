using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    public class Organiser : Identifiable<int>
    {
        public Organiser()
        {
        }

        public int Id { get; set; }

        public string Name
        {
            get;
            set;
        }

        public Organiser(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Organiser(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
