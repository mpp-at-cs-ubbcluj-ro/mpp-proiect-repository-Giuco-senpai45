package bask.network.objectprotocol;

import bask.model.Organiser;


public class OrganiserLoggedOutResponse implements UpdateResponse {
    private Organiser organiser;

    public OrganiserLoggedOutResponse(Organiser friend) {
        this.organiser = friend;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
