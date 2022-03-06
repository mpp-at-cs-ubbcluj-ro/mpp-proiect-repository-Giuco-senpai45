using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    internal class Participant : Identifiable<int>
    {
        private int _id;
        private string _name;
        private string _email;
        private string _phone;

        public Participant(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Participant(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public override string? ToString()
        {
            return Name + " " + Email + " "+ Phone+  "\n";
        }
    }
}
