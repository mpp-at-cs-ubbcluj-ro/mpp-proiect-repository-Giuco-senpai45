package chat.server;

import bask.model.Match;
import bask.model.Ticket;
import bask.repos.IRepoTicket;

import java.util.Collection;

public class ServiceTicket {
    IRepoTicket repoTickets;
    private Integer currentTicketID = 0;

    public ServiceTicket(IRepoTicket repoTickets) {
        this.repoTickets = repoTickets;
    }

    public void saveTicket(Integer mid,Integer quantity,String customerName)
    {
        Match match = new Match(mid);
        Ticket ticket = new Ticket(quantity,match,customerName);
        repoTickets.add(ticket);
    }

    public void removeTicket(Integer id){
        Ticket ticket = new Ticket(id);
        repoTickets.delete(ticket);
    }

    public void updateTicket(Integer id,Integer mid,Integer quantity,String customerName)
    {
        Match match = new Match(mid);
        Ticket ticket = new Ticket(id,quantity,match,customerName);
        repoTickets.update(ticket,id);
    }

    public Collection<Ticket> getAllTicket(){
        return repoTickets.getAll();
    }
}
