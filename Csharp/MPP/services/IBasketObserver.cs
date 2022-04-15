using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace services
{
    public interface IBasketObserver
    {
        void listUpdated(List<Match> matches);
        void organiserLoggedIn(Organiser organiser);
        void organiserLoggedOut(Organiser organiser);
    }
}
