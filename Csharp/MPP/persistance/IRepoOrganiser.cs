using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace persistance
{
    public interface IRepoOrganiser : Repository<Organiser, int>
    {
        Organiser findByNameAndPassword(string name, string password);
    }
}
