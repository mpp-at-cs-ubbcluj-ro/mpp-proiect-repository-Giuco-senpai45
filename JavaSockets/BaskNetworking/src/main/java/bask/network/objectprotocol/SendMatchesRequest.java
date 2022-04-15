package bask.network.objectprotocol;


import bask.model.Match;
import java.util.List;

public class SendMatchesRequest implements Request{
    private List<Match> matchList;

    public SendMatchesRequest(List<Match> message) {
        this.matchList = message;
    }

    public List<Match> getMatchList() {
        return matchList;
    }
}

