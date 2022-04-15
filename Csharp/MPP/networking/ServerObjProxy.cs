using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using services;
using model;

namespace networking
{
    public class ServerObjProxy : IBasketService
    {
        private string host;
        private int port;

        private IBasketObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public ServerObjProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        private void startReader()
        {
            //Thread tw = new Thread(new ThreadStart(run));
            Thread tw = new Thread(run);
            tw.Start();
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
            }
            catch (Exception e)
            {
                throw new BasketException("Error sending object " + e);
            }
        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        public List<Match> getMatchesList()
        {
            Request req = new Request.Builder().type(RequestType.GET_MATCHES).data(null).build();
            sendRequest(req);
            Response response = readResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new BasketException(err);
            }
            return (List<Match>)response.Data;
        }

        public Organiser getOrganiserByCredentials(Organiser user)
        {
            return null;
        }

        public void login(Organiser user, IBasketObserver client)
        {
            initializeConnection();
            Request req = new Request.Builder().type(RequestType.LOGIN).data(user).build();
            sendRequest(req);
            Response response = readResponse();
            if (response.Type == ResponseType.OK)
            {
                this.client = client;
                return;
            }
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                closeConnection();
                throw new BasketException(err);
            }
        }

        public void logout(Organiser user, IBasketObserver client)
        {
            Request req = new Request.Builder().type(RequestType.LOGOUT).data(user).build();
            sendRequest(req);
            Response response = readResponse();
            closeConnection();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new BasketException(err);
            }
        }

        public void sendUpdatedList(List<Match> matches)
        {
            Request req = new Request.Builder().type(RequestType.UPDATE_MATCHES).data(matches).build();
            sendRequest(req);
            Response response = readResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new BasketException(err);
            }
        }

        public List<Match> ticketSold(Ticket ticket)
        {
            Request req = new Request.Builder().type(RequestType.SELL_TICKET).data(ticket).build();
            sendRequest(req);
            Response response = readResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new BasketException(err);
            }
            return (List<Match>)response.Data;
        }

        private void handleUpdate(Response response)
        {
            if (response.Type == ResponseType.ORG_LOGGED_IN)
            {
                Organiser friend = (Organiser)response.Data;
                Console.WriteLine("Friend logged in " + friend);
                try
                {
                    client.organiserLoggedIn(friend);
                }
                catch (BasketException e)
                {
                    Console.WriteLine(e.StackTrace);

                }
            }
            if (response.Type == ResponseType.ORG_LOGGED_OUT)
            {
                Organiser friend = (Organiser)response.Data;
                Console.WriteLine("Friend logged out " + friend);
                try
                {
                    client.organiserLoggedOut(friend);
                }
                catch (BasketException e)
                {
                    Console.WriteLine(e.StackTrace);

                }
            }

            if (response.Type == ResponseType.NEW_MATCH_LIST)
            {
                List<Match> matches = (List<Match>)response.Data;
                Console.WriteLine("Match list updated");
                try
                {
                    client.listUpdated(matches);
                }
                catch (BasketException e)
                {
                    Console.WriteLine(e.StackTrace);

                }
            }
        }

        private bool isUpdate(Response response)
        {
            return response.Type == ResponseType.ORG_LOGGED_OUT || response.Type == ResponseType.ORG_LOGGED_IN ||
                    response.Type == ResponseType.NEW_MATCH_LIST || response.Type == ResponseType.SOLD_TICKET;
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (isUpdate((Response)response))
                    {
                        handleUpdate((Response)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }
    }
}
