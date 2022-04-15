using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    public class Ticket : Identifiable<int>
    {
        public int Id { get; set; }

        public int Quantity
        {
            get;
            set;
        }

        public Match TicketMatch
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Ticket()
        {
        }
        
        public Ticket(int id)
        {
            Id = id;
        }

        public Ticket(int id,int quantity, Match ticketMatch, string name)
        {
            Id = id;
            Quantity = quantity;
            TicketMatch = ticketMatch;
            Name = name;
        }

        public Ticket(int quantity, Match ticketMatch, string name)
        {
            Quantity = quantity;
            TicketMatch = ticketMatch;
            Name = name;
        }

        public override string ToString()
        {
            return Id + " " + Name + " " + TicketMatch + " " + Quantity;
        }
    }
}
