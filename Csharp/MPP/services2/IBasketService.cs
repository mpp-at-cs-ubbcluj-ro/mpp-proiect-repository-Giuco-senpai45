using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;

namespace services2
{
    public interface IBasketService
    {
        void login(Organiser organizer, IBasketObserver client);
        void logout(Organiser organizer, IBasketObserver client);
        Match[] getMatches();
        void sendUpdate(Ticket ticket);
    }
}
