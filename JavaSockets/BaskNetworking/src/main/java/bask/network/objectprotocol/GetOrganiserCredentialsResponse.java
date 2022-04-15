package bask.network.objectprotocol;

import bask.model.Organiser;

public class GetOrganiserCredentialsResponse implements Response {
    private Organiser organiser;

    public GetOrganiserCredentialsResponse(Organiser organisers) {
        this.organiser = organisers;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
