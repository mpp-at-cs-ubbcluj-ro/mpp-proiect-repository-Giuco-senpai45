package bask.network.objectprotocol;

import bask.model.Organiser;

public class GetOrganiserCredentialsRequest implements Request {
    private Organiser organiser;

    public GetOrganiserCredentialsRequest(Organiser user) {
        this.organiser = user;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
