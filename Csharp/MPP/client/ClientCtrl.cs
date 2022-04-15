using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;
using services2;

namespace client
{
    public class ClientCtrl : IBasketObserver
    {
        public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IBasketService server;
        private Organiser currentUser;
        public ClientCtrl(IBasketService server)
        {
            this.server = server;
            currentUser = null;
        }

        public Organiser getCurrentUser()
        {
            return currentUser;
        }

        public void login(String userId, String pass)
        {
            Organiser user = new Organiser(userId, pass);
            server.login(user, this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }

        public IList<Match> getMatches()
        {
            IList<Match> matchesList = new List<Match>();
            Match[] matches = server.getMatches();
            foreach (var match in matches)
            {
                matchesList.Add(match);
            }
            return matchesList;
        }

        protected virtual void OnUserEvent(UserEventArgs e)
        {
            Console.WriteLine("Entered onuservent {0}",currentUser);
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called {0}",currentUser);
        }

        public void sendUpdate(Ticket ticket)
        {
            server.sendUpdate(ticket);
        }

        public void listUpdated(List<Match> matches)
        {
            Console.WriteLine("Ctr matches list updated");
            Console.WriteLine("Fac updated in ctrl nu fac update laui");
            foreach (Match match in matches)
            {
                Console.WriteLine(match);
            }
            UserEventArgs userArgs = new UserEventArgs(UserEvent.NewMatchList, matches);
            OnUserEvent(userArgs);
        }

        public void updateReceived(Match[] matches)
        {
            UserEventArgs userArgs = new UserEventArgs(UserEvent.NewMatchList, matches);
            Console.WriteLine("update received in view controller");
            OnUserEvent(userArgs);
        }

        /*public void organiserLoggedIn(Organiser organiser)
        {
            Console.WriteLine("Organiser logged in " + organiser);
            UserEventArgs userArgs = new UserEventArgs(UserEvent.LoggedIn, organiser);
            OnUserEvent(userArgs);
        }

        public void organiserLoggedOut(Organiser organiser)
        {
            Console.WriteLine("Organiser logged out " + organiser);
            UserEventArgs userArgs = new UserEventArgs(UserEvent.LoggedOut, organiser.Id);
            OnUserEvent(userArgs);
        }*/
    }
}
