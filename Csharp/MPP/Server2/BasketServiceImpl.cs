using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistance2;
using networking2;
using model2;
using services2;

namespace Server2
{
    public class BasketServiceImpl : IBasketService
    {
        private MasterService masterService;
        private readonly IDictionary<int, IBasketObserver> loggedOrganisers;

        public BasketServiceImpl(MasterService masterService)
        {
            this.masterService = masterService;
            loggedOrganisers = new Dictionary<int, IBasketObserver>();
        }

        public void login(Organiser user, IBasketObserver client)
        {
            Organiser orgR = masterService.OrganiserService.findOrganiserByLogin(user.Name, user.Password);

            Console.WriteLine("This is the logged user " + orgR.Id + " " + orgR.Name + " " + orgR.Password);
            if (orgR != null)
            {
                if (loggedOrganisers.Count == 0)
                {
                    loggedOrganisers.Add(orgR.Id, client);
                    Console.WriteLine("No one is logged");
                    //notifyOrganisersLoggedIn(orgR);
                }
                else
                {
                    if (loggedOrganisers.ContainsKey(orgR.Id))
                    {
                        throw new BasketException("Organiser already logged in");
                    }
                    loggedOrganisers.Add(orgR.Id, client);
                    //notifyOrganisersLoggedIn(orgR);
                }
            }
            else
            {
                throw new BasketException("Authentication failed.");
            }
        }

        public void logout(Organiser user, IBasketObserver client)
        {
            Organiser orgN = masterService.OrganiserService.findOrganiserByLogin(user.Name, user.Password);

            Console.WriteLine("This is the user that wants to logout");
            Console.WriteLine(orgN);
            IBasketObserver localClient = loggedOrganisers[orgN.Id];
            if (localClient == null)
                throw new BasketException("User " + orgN.Id + " is not logged in.");
            loggedOrganisers.Remove(orgN.Id);
            Console.WriteLine("Done logout in implementation");
            //notifyOrganisersLoggedOut(orgN);
        }

        public Organiser getOrganiserByCredentials(Organiser user)
        {
            Console.WriteLine("Getting organisers credentials " + user);
            return masterService.OrganiserService.findOrganiserByLogin(user.Name, user.Password);
        }

        public List<Match> getMatchesList()
        {
            List<Match> rm = masterService.MatchService.getAllMatches();
            return rm;
        }

        public Match[] getMatches()
        {
            IEnumerable<Match> matches = masterService.MatchService.getAllMatches();
            IList<Match> result = new List<Match>();
            Console.WriteLine("get matches");
            foreach (Match match in matches)
            {
                result.Add(match);
                Console.WriteLine("added match: " + match);
            }
            Console.WriteLine("Size " + result.Count);
            return result.ToArray();
        }

        public void sendUpdate(Ticket ticket)
        {
            masterService.sellTicketForMatch(ticket.TicketMatch.Id, ticket.Quantity, ticket.Name);

            Match[] matches = getMatches();
            Console.WriteLine("Clientu din implementare");
            foreach (IBasketObserver client in loggedOrganisers.Values)
            {
                Console.WriteLine(client);
                if (client != null)
                {
                    try
                    {
                        client.updateReceived(matches);
                    }
                    catch (BasketException ex)
                    {
                        Console.WriteLine("erro notifying client " + ex.Message);
                    }
                }
            }
        }
    }
}
