package bask.services;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;

import java.util.List;

public interface IObserver {
    void listUpdated(List<Match> matches) throws  BasketException;
    void ticketSold(Ticket ticket) throws BasketException;
    void organiserLoggedIn(Organiser organiser) throws BasketException;
    void organiserLoggedOut(Organiser organiser) throws BasketException;
}
