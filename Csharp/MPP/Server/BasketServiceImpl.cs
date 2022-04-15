using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistance;
using networking;
using model;
using services;

namespace Server
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
                }
                else
                {
                    if (loggedOrganisers.ContainsKey(orgR.Id) != null)
                    {
                        throw new BasketException("Organiser already logged in");
                    }
                    loggedOrganisers.Add(orgR.Id, client);
                    notifyOrganisersLoggedIn(orgR);
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

            IBasketObserver localClient = loggedOrganisers[user.Id];
            if (localClient == null)
                throw new BasketException("User " + orgN.Id + " is not logged in.");
            loggedOrganisers.Remove(user.Id);
            notifyOrganisersLoggedOut(orgN);
        }

        private void notifyOrganisersLoggedIn(Organiser user)
        {
            IEnumerable<Organiser> organisers = masterService.OrganiserService.getAllOrganisers();
            Console.WriteLine("Logged " + organisers);
            foreach (Organiser us in organisers)
            {
                IBasketObserver basketClient = loggedOrganisers[us.Id];
                Task.Run(() => basketClient.organiserLoggedIn(user));
            }
        }

        private void notifyOrganisersLoggedOut(Organiser user)
        {
            IEnumerable<Organiser> organisers = masterService.OrganiserService.getAllOrganisers();
            Console.WriteLine("Logged " + organisers);
            foreach (Organiser us in organisers)
            {
                IBasketObserver basketClient = loggedOrganisers[us.Id];
                Task.Run(() => basketClient.organiserLoggedIn(user));
            }
        }

        public Organiser getOrganiserByCredentials(Organiser user)
        {
            Console.WriteLine("Getting organisers credentials " + user);
            return masterService.OrganiserService.findOrganiserByLogin(user.Name, user.Password);
        }

        public List<Match> ticketSold(Ticket ticket)
        {
            masterService.sellTicketForMatch(ticket.TicketMatch.Id, ticket.Quantity, ticket.Name);
            IEnumerable<Match> rm = masterService.MatchService.getAllMatches();
            List<Match> repoMatches = rm.ToList();
            return repoMatches;
        }

        public void sendUpdatedList(List<Match> matches)
        {
            if (loggedOrganisers.Count == 0)
            {
                throw new BasketException("There are no other organisers logged in");
            }
            //IEnumerable<Match> rm = masterService.MatchService.getAllMatches();
            //List<Match> repoMatches = rm.ToList();
            foreach (IBasketObserver obs in loggedOrganisers.Values)
            {
                obs.listUpdated(matches);
            }
        }

        public List<Match> getMatchesList()
        {
            IEnumerable<Match> rm = masterService.MatchService.getAllMatches();
            List<Match> repoMatches = rm.ToList();
            return repoMatches;
        }
    }
}
