using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace services
{
    public interface IBasketService
    {
        void login(Organiser user, IBasketObserver client);
        Organiser getOrganiserByCredentials(Organiser user);
        List<Match> ticketSold(Ticket ticket);
        void logout(Organiser user, IBasketObserver client);
        void sendUpdatedList(List<Match> matches);
        List<Match> getMatchesList();
    }
}
