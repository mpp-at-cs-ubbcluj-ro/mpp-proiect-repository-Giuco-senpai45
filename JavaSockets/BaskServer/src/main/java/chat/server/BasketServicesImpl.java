package chat.server;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;
import bask.services.BasketException;
import bask.services.IObserver;
import bask.services.IServices;

import java.util.*;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class BasketServicesImpl implements IServices {

    private MasterService masterService;
    private Map<Integer, IObserver> loggedOrganisers;
    private final int defaultThreadsNo=5;


    public BasketServicesImpl(MasterService masterService) {
        this.masterService = masterService;
        loggedOrganisers=new ConcurrentHashMap<>();;
    }

    @Override
    public synchronized void login(Organiser user, IObserver client) throws BasketException {
        Organiser orgR = masterService.getOrganiserService().findOrganiserByLogin(user.getName(), user.getPassword());

        System.out.println("This is the logged user " + orgR.getId() + " " + orgR.getName() + " " + orgR.getPassword());
        if(orgR != null){
            if(loggedOrganisers.isEmpty()) {
                loggedOrganisers.put(orgR.getId(), client);
                System.out.println("No one is logged");
            }
            else {
                if(loggedOrganisers.get(orgR.getId())!=null){
                    throw new BasketException("Organiser already logged in");
                }
                loggedOrganisers.put(orgR.getId(), client);
                notifyOrganisersLoggedIn(orgR);
            }
        }
        else {
            throw new BasketException("Authentication failed.");
        }
    }

    @Override
    public synchronized Organiser getOrganiserByCredentials(Organiser user) throws BasketException {
        System.out.println("Getting organisers credentials " + user);
        return masterService.getOrganiserService().findOrganiserByLogin(user.getName(), user.getPassword());
    }

    @Override
    public synchronized void logout(Organiser user, IObserver client) throws BasketException {
        Organiser orgN = masterService.getOrganiserService().findOrganiserByLogin(user.getName(), user.getPassword());

        IObserver localClient = loggedOrganisers.remove(orgN.getId());
        if (localClient==null)
            throw new BasketException("User "+orgN.getId()+" is not logged in.");
        notifyOrganisersLoggedOut(orgN);
    }

    @Override
    public synchronized void sendUpdatedList(Ticket ticket) throws BasketException {
        if(loggedOrganisers.isEmpty()){
            throw new BasketException("There are no other organisers logged in");
        }
        masterService.sellTicketForMatch(ticket.getMatch().getId(),ticket.getQuantity(),ticket.getCustomerName());
        Iterable<Match> rm = masterService.getMatchService().getAllMatches();
        List<Match> repoMatches = new ArrayList<Match>();
        rm.forEach(repoMatches::add);
        //TODO: baga asta in sold Ticket si sa trim lista innapoi
        for(IObserver obs : loggedOrganisers.values()){
            obs.listUpdated(repoMatches);
        }
    }

    @Override
    public synchronized List<Match> getMatchesList() throws BasketException {
        Iterable<Match> rm = masterService.getMatchService().getAllMatches();
        List<Match> repoMatches = new ArrayList<Match>();
        rm.forEach(repoMatches::add);
        return repoMatches;
    }

    /*@Override
    public synchronized Organiser[] getLoggedOrganisers(Organiser user) throws BasketException {
        Iterable<Organiser> organisers= masterService.getOrganiserService().getAllOrganisers();

        Set<Organiser> result = new TreeSet<Organiser>();
        System.out.println("Logged organisers for: "+user.getID());
        for (Organiser friend : organisers){
            if (loggedOrganisers.containsKey(friend.getID())){    //the friend is logged in
                result.add(new Organiser(friend.getID()));
                System.out.println("+"+friend.getID());
            }
        }
        System.out.println("Size "+result.size());
        return result.toArray(new Organiser[result.size()]);
    }*/

    private void notifyOrganisersLoggedIn(Organiser user) throws BasketException {
        Iterable<Organiser> organisers= masterService.getOrganiserService().getAllOrganisers();
        System.out.println("Logged " + organisers);

        ExecutorService executor= Executors.newFixedThreadPool(defaultThreadsNo);
        for(Organiser us :organisers){
            IObserver basketClient = loggedOrganisers.get(us.getId());
            if (basketClient!=null)
                executor.execute(() -> {
                    try {
                        System.out.println("Notifying [" + us.getId()+ "] organiser ["+user.getId()+"] logged in.");
                        basketClient.organiserLoggedIn(user);
                    } catch (BasketException e) {
                        System.err.println("Error notifying organisers " + e);
                    }
                });
        }
        executor.shutdown();
    }

    private void notifyOrganisersLoggedOut(Organiser user)throws BasketException {
        Iterable<Organiser> organisers= masterService.getOrganiserService().getAllOrganisers();

        ExecutorService executor= Executors.newFixedThreadPool(defaultThreadsNo);
        for(Organiser us :organisers){
            IObserver basketClient = loggedOrganisers.get(us.getId());
            if (basketClient!=null)
                executor.execute(() -> {
                    try {
                        System.out.println("Notifying ["+us.getId()+"] friend ["+user.getId()+"] logged out.");
                        basketClient.organiserLoggedOut(user);
                    } catch (BasketException e) {
                        System.out.println("Error notifying friend " + e);
                    }
                });
        }
        executor.shutdown();
    }

}
