using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;

namespace persistance2
{
    public interface IRepoMatch : Repository<Match, int>
    {
        void updateNoOfSeats(int quantity, int id);
        ICollection<Match> getAllDescendingNoOfSeats();
    }
}
