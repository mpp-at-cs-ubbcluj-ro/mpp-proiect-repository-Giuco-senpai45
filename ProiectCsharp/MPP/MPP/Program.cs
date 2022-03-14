using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using MPP.model;
using MPP.repository;
using log4net;
using log4net.Config;
[assembly: XmlConfigurator(Watch = true)]

namespace MPP
{
    class Program
    {
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("basketDB"));

            testRepoOrganiser(props);
            testRepoMatch(props);
            testRepoTicket(props);
        }

        private static void testRepoOrganiser(IDictionary<String, string> props)
        {
            Organiser organiser = new Organiser(2, "Pavel");
            RepoDBOrganiser repoDbOrganiser = new RepoDBOrganiser(props);

            if (repoDbOrganiser.findbyId(2).Name == "Pavel")
            {
                repoDbOrganiser.update(new Organiser(2,"Pavelnou"),2);
                foreach (var org in repoDbOrganiser.findAll())
                {
                    Console.WriteLine(org);
                }
                repoDbOrganiser.delete(new Organiser(2," "));
            }
            else
            {
                repoDbOrganiser.add(organiser);
                foreach (var org in repoDbOrganiser.findAll())
                {
                    Console.WriteLine(org);
                }
            }
        }

        private static void testRepoMatch(IDictionary<String, string> props)
        {
            Match match = new Match(1, "T1", "T2","Normal", 120,12.3);
            RepoDBMatch repoDbMatch = new RepoDBMatch(props);

            Match found = repoDbMatch.findbyId(1);
            if (found.Team1 == "T1")
            {
                repoDbMatch.update(new Match(1, "T1", "T2","Semifinala", 70,12.3),1);
                foreach (var org in repoDbMatch.findAll())
                {
                    Console.WriteLine(org);
                }
                repoDbMatch.delete(new Match(1));
            }
            else
            {
                repoDbMatch.add(match);
                foreach (var org in repoDbMatch.findAll())
                {
                    Console.WriteLine(org);
                }
            }
        }
        
        private static void testRepoTicket(IDictionary<String, string> props)
        {
            Ticket ticket = new Ticket(1, 3, new Match(1),"Andrei");
            RepoDBTicket repoDbTicket = new RepoDBTicket(props);

            Ticket found = repoDbTicket.findbyId(1);
            if (found.Name == "Andrei")
            {
                repoDbTicket.update(new Ticket(1,  1, new Match(1),"Andrei updated"),1);
                foreach (var org in repoDbTicket.findAll())
                {
                    Console.WriteLine(org);
                }
                repoDbTicket.delete(new Ticket(1));
            }
            else
            {
                repoDbTicket.add(ticket);
                foreach (var org in repoDbTicket.findAll())
                {
                    Console.WriteLine(org);
                }
            }
        }
        
        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}