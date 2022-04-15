using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using services2;
using model2;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

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

        public void updateReceived(Match[] matches)
        {
			Console.WriteLine("update received(worker)  ");
			try
			{
				sendResponse(new NewMatchesResponse(matches));
			}
			catch (Exception e)
			{
				throw new BasketException("Sending error: " + e);
			}
		}

        private object handleRequest(Request request)
        {
			Response response = null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq = (LoginRequest)request;
				Organiser user = logReq.User;
				try
				{
					lock (server)
					{
						server.login(user, this);
					}
					return new OkResponse();
				}
				catch (BasketException e)
				{
					connected = false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq = (LogoutRequest)request;
				Organiser user = logReq.User;
				try
				{
					lock (server)
					{
						server.logout(user, this);
					}
					connected = false;
					return new OkResponse();

				}
				catch (BasketException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is GetMatches)
			{
				Console.WriteLine("get matches Request ...");
				GetMatches getReq = (GetMatches)request;
				try
				{
					Match[] matches;
					lock (server)
					{
						matches = server.getMatches();
					}
					return new GetMatchesResponse(matches);
				}
				catch (BasketException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is SendUpdateRequest)
			{
				Console.Write("send update request");
				SendUpdateRequest sendReq = (SendUpdateRequest)request;
				Ticket ticket = sendReq.Ticket;
				try
				{
					lock (server)
					{
						server.sendUpdate(ticket);
					}
					return new OkResponse();
				}
				catch (BasketException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			return response;
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
