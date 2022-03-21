package service;

import model.Match;

public class MasterService {
    private ServiceMatch matchService;
    private ServiceOrganiser organiserService;
    private ServiceTicket ticketService;

    public MasterService(ServiceMatch matchService, ServiceOrganiser organiserService, ServiceTicket ticketService) {
        this.matchService = matchService;
        this.organiserService = organiserService;
        this.ticketService = ticketService;
    }

    public void sellTicketForMatch(Integer mid,Integer quantity,String customerName){
        Match match = matchService.findMatchById(mid);
        int oldNoOfSeats = match.getNrOfSeats();
        if(quantity <= oldNoOfSeats) {
            Integer qt = oldNoOfSeats - quantity;
            matchService.updateMatchNoOfSeats(qt,mid);
            ticketService.saveTicket(mid,quantity,customerName);
        }
    }

    public ServiceMatch getMatchService() {
        return matchService;
    }

    public ServiceOrganiser getOrganiserService() {
        return organiserService;
    }

    public ServiceTicket getTicketService() {
        return ticketService;
    }
}
