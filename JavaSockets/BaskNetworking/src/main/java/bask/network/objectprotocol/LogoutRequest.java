package bask.network.objectprotocol;

import bask.model.Organiser;


public class LogoutRequest implements Request {
    private Organiser organiser;

    public LogoutRequest(Organiser user) {
        this.organiser = user;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
