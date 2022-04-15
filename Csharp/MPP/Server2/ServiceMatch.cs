using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;
using persistance2;

namespace Server2
{
    public class ServiceMatch
    {
        IRepoMatch repoMatch;

        public ServiceMatch(IRepoMatch repoMatch)
        {
            this.repoMatch = repoMatch;
        }

        public void saveMatch(string t1, string t2, string type, int noOfSeats, double price, DateTime date)
        {
            Match match = new Match(t1, t2, type, noOfSeats, price, date);
            repoMatch.add(match);
        }

        public void removeMatch(int id)
        {
            Match match = new Match(id);
            repoMatch.delete(match);
        }

        public void updateMatch(int id, string t1, string t2, string type, int noOfSeats, double price, DateTime date)
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

        public List<Match> getAllMatches()
        {
            return repoMatch.findAll().ToList();
        }

        public ICollection<Match> getDescdendingMatchesNoOfSeats()
        {
            return repoMatch.getAllDescendingNoOfSeats();
        }
    }
}
