package repository;

import model.Match;

import java.util.Collection;

public interface IRepoMatch extends Repository<Match, Integer> {
    void updateNoOfSeats(Integer quantity,Integer id);
    Collection<Match> getAllDescendingNoOfSeats();
}
