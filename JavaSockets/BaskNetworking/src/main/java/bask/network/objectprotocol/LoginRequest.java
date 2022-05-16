package bask.network.objectprotocol;


import bask.model.Organiser;

public class LoginRequest implements Request {
    private Organiser organiser;

    public LoginRequest(Organiser user) {
        this.organiser = user;
    }

    public Organiser getOrganiser() {
        return organiser;
    }
}
