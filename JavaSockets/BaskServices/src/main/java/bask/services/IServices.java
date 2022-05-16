package bask.services;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;

import java.util.List;

public interface IServices {
    void login(Organiser user, IObserver client) throws BasketException;
    void logout(Organiser user, IObserver client) throws BasketException;
    Organiser getOrganiserByCredentials(Organiser user) throws BasketException;
    List<Match> getMatchesList() throws BasketException;
    void sendUpdatedList(Ticket ticket) throws BasketException;
}
