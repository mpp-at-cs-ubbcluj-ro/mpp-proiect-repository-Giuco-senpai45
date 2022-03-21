package service;

import model.Match;
import model.Organiser;
import repository.IRepoMatch;
import repository.RepoDBMatch;

import java.sql.Timestamp;
import java.util.Collection;

public class ServiceMatch {
    IRepoMatch repoMatch;
    private Integer  currentMatchID = 0;

    public ServiceMatch(IRepoMatch repoMatch) {
        this.repoMatch = repoMatch;
    }

    public void saveMatch(String t1, String t2, String type, Integer noOfSeats, Double price, Timestamp date)
    {
        Match match = new Match(t1,t2,type,noOfSeats,price,date);
        repoMatch.add(match);
    }

    public void removeMatch(Integer id){
        Match match = new Match(id);
        repoMatch.delete(match);
    }

    public void updateMatch(Integer id,String t1,String t2,String type,Integer noOfSeats,Double price, Timestamp date)
    {
        Match match = new Match(id,t1,t2,type,noOfSeats,price,date);
        repoMatch.update(match,id);
    }

    public void updateMatchNoOfSeats(Integer quantity,Integer id){
        repoMatch.updateNoOfSeats(quantity,id);
    }

    public Match findMatchById(Integer id)
    {
        return repoMatch.findById(id);
    }

    public Collection<Match> getAllMatches(){
        return repoMatch.getAll();
    }

    public Collection<Match> getDescdendingMatchesNoOfSeats(){
        return repoMatch.getAllDescendingNoOfSeats();
    }
}
