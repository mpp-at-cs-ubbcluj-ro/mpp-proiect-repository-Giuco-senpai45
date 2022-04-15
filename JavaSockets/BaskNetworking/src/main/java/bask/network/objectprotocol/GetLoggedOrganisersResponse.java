package bask.network.objectprotocol;

import bask.model.Organiser;


public class GetLoggedOrganisersResponse implements Response {
    private Organiser[] organisers;

    public GetLoggedOrganisersResponse(Organiser[] organisers) {
        this.organisers = organisers;
    }

    public Organiser[] getOrganisers() {
        return organisers;
    }
}
