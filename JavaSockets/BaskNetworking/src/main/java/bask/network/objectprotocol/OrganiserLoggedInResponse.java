package bask.network.objectprotocol;

import bask.model.Organiser;


public class OrganiserLoggedInResponse implements UpdateResponse{
    private Organiser organiser;

    public OrganiserLoggedInResponse(Organiser friend) {
        this.organiser = friend;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
