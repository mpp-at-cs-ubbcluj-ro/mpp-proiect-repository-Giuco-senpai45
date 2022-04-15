using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using MPP.model;
using MPP.repository;
using log4net;
using log4net.Config;
using System.Runtime.InteropServices;
using MPP.service;

namespace MPP
{
    internal static class FormMain
    {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("basketDB"));

            RepoDBOrganiser repoOrganiser = new RepoDBOrganiser(props);
            RepoDBMatch repoMatch = new RepoDBMatch(props);
            RepoDBTicket repoTicket = new RepoDBTicket(props);
            ServiceOrganiser organiserService = new ServiceOrganiser(repoOrganiser);
            ServiceMatch matchService = new ServiceMatch(repoMatch);
            ServiceTicket ticketService = new ServiceTicket(repoTicket);
            MasterService masterService = new MasterService(matchService,organiserService,ticketService);


            var matches = repoMatch.findAll();
            foreach (Match match in matches)
            {
                Console.WriteLine(match);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(masterService));
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