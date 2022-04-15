using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP.model;
using MPP.repository;

namespace MPP.service
{
    public class ServiceMatch
    {
        IRepoMatch repoMatch;

        public ServiceMatch(IRepoMatch repoMatch)
        {
            this.repoMatch = repoMatch;
        }

        public void saveMatch(String t1, String t2, String type, int noOfSeats, Double price, DateTime date)
        {
            Match match = new Match(t1, t2, type, noOfSeats, price, date);
            repoMatch.add(match);
        }

        public void removeMatch(int id)
        {
            Match match = new Match(id);
            repoMatch.delete(match);
        }

        public void updateMatch(int id, String t1, String t2, String type, int noOfSeats, Double price, DateTime date)
        {
            Match match = new Match(id, t1, t2, type, noOfSeats, price, date);
            repoMatch.update(match, id);
        }

        public void updateMatchNoOfSeats(int quantity, int id)
        {
            repoMatch.updateNoOfSeats(quantity, id);
        }

        public Match findMatchById(int id)
        {
            return repoMatch.findbyId(id);
        }

        public ICollection<Match> getAllMatches()
        {
            return repoMatch.findAll();
        }

        public ICollection<Match> getDescdendingMatchesNoOfSeats()
        {
            return repoMatch.getAllDescendingNoOfSeats();
        }
    }
}
