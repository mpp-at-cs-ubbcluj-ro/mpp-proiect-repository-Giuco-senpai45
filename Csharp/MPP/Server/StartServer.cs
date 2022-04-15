using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using networking;
using persistance;
using services;

namespace Server
{
    public class StartServer
    {
        static void Main(string[] args)
        {
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("basketDB"));
            Console.WriteLine("Am trecut de conectare");

            RepoDBOrganiser repoOrganiser = new RepoDBOrganiser(props);
            RepoDBMatch repoMatch = new RepoDBMatch(props);
            RepoDBTicket repoTicket = new RepoDBTicket(props);
            ServiceOrganiser organiserService = new ServiceOrganiser(repoOrganiser);
            ServiceMatch matchService = new ServiceMatch(repoMatch);
            ServiceTicket ticketService = new ServiceTicket(repoTicket);
            MasterService masterService = new MasterService(matchService, organiserService, ticketService);

            BasketServiceImpl serviceImpl = new BasketServiceImpl(masterService);
            SerialChatServer server = new SerialChatServer("127.0.0.1", 55556, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();

        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }
    }

    public class SerialChatServer : ConcurrentServer
    {
        private IBasketService server;
        private ClientObjWorker worker;
        public SerialChatServer(string host, int port, IBasketService server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialChatServer...");
        }

        protected override Thread createWorker(TcpClient client)
        {

            worker = new ClientObjWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}
