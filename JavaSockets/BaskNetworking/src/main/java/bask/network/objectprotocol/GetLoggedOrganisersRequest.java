package bask.network.objectprotocol;

import bask.model.Organiser;


public class GetLoggedOrganisersRequest implements Request {
    private Organiser organiser;

    public GetLoggedOrganisersRequest(Organiser user) {
        this.organiser = user;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}

