using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace Server
{
    public class MasterService
    {
        public MasterService(ServiceMatch matchService, ServiceOrganiser organiserService, ServiceTicket ticketService)
        {
            MatchService = matchService;
            OrganiserService = organiserService;
            TicketService = ticketService;
        }

        public void sellTicketForMatch(int mid, int quantity, string customerName)
        {
            Match match = MatchService.findMatchById(mid);
            int oldNoOfSeats = match.NrOfSeats;
            if (quantity <= oldNoOfSeats)
            {
                int qt = oldNoOfSeats - quantity;
                MatchService.updateMatchNoOfSeats(qt, mid);
                TicketService.saveTicket(mid, quantity, customerName);
            }
        }

        public ServiceMatch MatchService
        {
            get;
            set;
        }

        public ServiceOrganiser OrganiserService
        {
            get;
            set;
        }

        public ServiceTicket TicketService
        {
            get;
            set;
        }
    }
}
