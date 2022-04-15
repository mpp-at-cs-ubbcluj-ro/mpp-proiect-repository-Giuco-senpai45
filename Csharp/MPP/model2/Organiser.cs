using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model2
{
    [Serializable]
    public class Organiser : Identifiable<int>
    {
        public Organiser()
        {
        }

        public Organiser(int id)
        {
            Id = id;
        }

        public Organiser(string name, string password) : this(name)
        {
            Password = password;
        }

        public Organiser(int id, string name, string password) : this(id, name)
        {
            Password = password;
        }

        public virtual int Id { get; set; }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Password
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
