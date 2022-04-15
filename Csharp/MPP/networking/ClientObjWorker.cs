using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using model2;
using services2;

namespace networking2
{
    public class ClientObjWorker : IBasketObserver
    {
        private IBasketService server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public ClientObjWorker(IBasketService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();
        private Response handleRequest(Request request)
        {
            Response response = null;
            if (request.Type == RequestType.LOGIN)
            {
                Console.WriteLine("Login request ..." + request.Type);
                Organiser user = (Organiser)request.Data;
                try
                {
                    server.login(user, this);
                    return okResponse;
                }
                catch (BasketException e)
                {
                    connected = false;
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }
            if (request.Type == RequestType.LOGOUT)
            {
                Console.WriteLine("Logout request");
                Organiser user = (Organiser)request.Data;
                try
                {
                    server.logout(user, this);
                    connected = false;
                    return okResponse;

                }
                catch (BasketException e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }
            if (request.Type == RequestType.UPDATE_MATCHES)
            {
                Console.WriteLine("Update matches list request ...");
                List<Match> matches = (List<Match>)request.Data;
                try
                {
                    server.sendUpdatedList(matches);
                    return new Response.Builder().type(ResponseType.NEW_MATCH_LIST).data(matches).build();

                }
                catch (BasketException e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            if (request.Type == RequestType.SELL_TICKET)
            {
                Console.WriteLine("Selling ticket request ..." + request.Type);
                Ticket ticket = (Ticket)request.Data;
                try
                {
                    List<Match> matches = server.ticketSold(ticket);
                    return new Response.Builder().type(ResponseType.NEW_MATCH_LIST).data(matches).build();
                }
                catch (BasketException e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            if (request.Type == RequestType.GET_MATCHES)
            {
                Console.WriteLine("Getting matches request ..." + request.Type);
                try
                {
                    List<Match> matches = server.getMatchesList();
                    return new Response.Builder().type(ResponseType.GOT_MATCHES).data(matches).build();
                }
                catch (BasketException e)
                {
                    return new Response.Builder().type(ResponseType.ERROR).data(e.Message).build();
                }
            }

            return response;
        }

        public virtual void listUpdated(List<Match> matches)
        {
            Response resp = new Response.Builder().type(ResponseType.NEW_MATCH_LIST).data(matches).build();
            Console.WriteLine("List updated" + matches);
            try
            {
                sendResponse(resp);
            }
            catch (IOException e)
            {
                throw new BasketException("Sending error: " + e);
            }
        }

        public virtual void organiserLoggedIn(Organiser organiser)
        {
            Response resp = new Response.Builder().type(ResponseType.ORG_LOGGED_IN).data(organiser).build();
            Console.WriteLine("Organiser logged in " + organiser);
            try
            {
                sendResponse(resp);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void organiserLoggedOut(Organiser organiser)
        {
            Response resp = new Response.Builder().type(ResponseType.ORG_LOGGED_OUT).data(organiser).build();
            Console.WriteLine("Organiser logged out " + organiser);
            try
            {
                sendResponse(resp);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }
        }
    }
}
