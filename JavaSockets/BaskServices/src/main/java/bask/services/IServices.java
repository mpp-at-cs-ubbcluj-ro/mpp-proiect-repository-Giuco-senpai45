package bask.services;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;

import java.util.List;

public interface IServices {
    void login(Organiser user, IObserver client) throws BasketException;
    Organiser getOrganiserByCredentials(Organiser user) throws BasketException;
    void ticketSold(Ticket ticket) throws BasketException;
    void logout(Organiser user, IObserver client) throws BasketException;
    void sendUpdatedList(List<Match> matches) throws BasketException;
    List<Match> getMatchesList() throws BasketException;
    //Organiser[] getLoggedOrganisers(Organiser user) throws BasketException;
}
