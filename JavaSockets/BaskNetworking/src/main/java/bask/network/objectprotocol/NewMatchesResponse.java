package bask.network.objectprotocol;


import bask.model.Match;

import java.util.List;

public class NewMatchesResponse implements UpdateResponse {
    private List<Match> matchList;

    public NewMatchesResponse(List<Match> message) {
        this.matchList = message;
    }

    public List<Match> getMatchList() {
        return matchList;
    }
}
