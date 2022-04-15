using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using services2;
using model2;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace networking2
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

		public ServerObjProxy(string host,int port)
        {
			this.host = host;
			this.port = port;
			responses = new Queue<Response>();
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

		public virtual void login(Organiser user, IBasketObserver client)
		{
			initializeConnection();
			sendRequest(new LoginRequest(user));
			Response response = readResponse();
			if (response is OkResponse)
			{
				this.client = client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				closeConnection();
				throw new BasketException(err.Message);
			}
		}

		public virtual void logout(Organiser user, IBasketObserver client)
		{
			sendRequest(new LogoutRequest(user));
			Response response = readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new BasketException(err.Message);
			}
		}

		private void sendRequest(Request request)
		{
			try
			{
				formatter.Serialize(stream, request);
				stream.Flush();
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
				_waitHandle.WaitOne(); //TODO: aici se blocheaza
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
		private void startReader()
		{
			Thread tw = new Thread(run);
			tw.Start();
		}

		private void handleUpdate(UpdateResponse update)
		{
			if (update is NewMatchesResponse)
			{
				NewMatchesResponse matchUpd = (NewMatchesResponse)update;
				Match[] matches = matchUpd.Matches;
				Console.WriteLine("Primesc atatea meciuri in proxy");
				Console.WriteLine(matches.Length);
				try
				{
					client.updateReceived(matches);
				}
				catch (BasketException e)
				{
					Console.WriteLine(e.StackTrace);
				}
			}
		} 
		public virtual void run()
		{
			while (!finished)
			{
				try
				{
					object response = formatter.Deserialize(stream);
					Console.WriteLine("response received " + response);
					if (response is UpdateResponse)
					{
						handleUpdate((UpdateResponse)response);
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

        public Organiser getOrganiserByCredentials(Organiser user)
        {
            throw new NotImplementedException();
        }

        public Match[] getMatches()
        {
			sendRequest(new GetMatches());
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new BasketException(err.Message);
			}
			GetMatchesResponse resp = (GetMatchesResponse)response;
			Match[] matches = resp.Matches;
			return matches;
		}

        public void sendUpdate(Ticket ticket)
        {
			sendRequest(new SendUpdateRequest(ticket));
			Response response = readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err = (ErrorResponse)response;
				throw new BasketException(err.Message);
			}
		}
    }
}
