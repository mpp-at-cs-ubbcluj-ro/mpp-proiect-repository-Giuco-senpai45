using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP.model;
using MPP.repository;

namespace MPP.service
{
    public class ServiceTicket
    {
        IRepoTicket repoTickets;

        public ServiceTicket(IRepoTicket repoTickets)
        {
            this.repoTickets = repoTickets;
        }

        public void saveTicket(int mid, int quantity, String customerName)
        {
            Match match = new Match(mid);
            Ticket ticket = new Ticket(quantity, match, customerName);
            repoTickets.add(ticket);
        }

        public void removeTicket(int id)
        {
            Ticket ticket = new Ticket(id);
            repoTickets.delete(ticket);
        }

        public void updateTicket(int id, int mid, int quantity, String customerName)
        {
            Match match = new Match(mid);
            Ticket ticket = new Ticket(id, quantity, match, customerName);
            repoTickets.update(ticket, id);
        }

        public ICollection<Ticket> getAllTicket()
        {
            return repoTickets.findAll();
        }
    }
}
