using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Proto;
using model2;
using services2;

namespace protobuf
{
    public class ProtoBaskWorker : IBasketObserver
    {
        private IBasketService server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool connected;

        public ProtoBaskWorker(IBasketService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
				Console.WriteLine("ASta ii streamu");
				Console.WriteLine(stream);
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
					Console.WriteLine("Getting request");
					Console.WriteLine(stream.ToString());
					BasketRequest request = BasketRequest.Parser.ParseDelimitedFrom(stream);
					Console.WriteLine("Not passing request");
					BasketResponse response = handleRequest(request);
					if (response != null)
					{
						sendResponse(response);
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

        private BasketResponse handleRequest(BasketRequest request)
        {
			BasketResponse response = null;
			BasketRequest.Types.Type reqType = request.Type;
			switch (reqType)
			{
				case BasketRequest.Types.Type.Login:
					{
						Console.WriteLine("Login request ...");
						model2.Organiser user = ProtoUtils.getUser(request);
						try
						{
							lock (server)
							{
								server.login(user, this);
							}
							return ProtoUtils.createOkResponse();
						}
						catch (BasketException e)
						{
							connected = false;
							return ProtoUtils.createErrorResponse(e.Message);
						}
					}
				case BasketRequest.Types.Type.Logout:
					{
						Console.WriteLine("Logout request");
						model2.Organiser user = ProtoUtils.getUser(request);
						try
						{
							lock (server)
							{

								server.logout(user, this);
							}
							connected = false;
							return ProtoUtils.createOkResponse();

						}
						catch (BasketException e)
						{
							return ProtoUtils.createErrorResponse(e.Message);
						}
					}
				case BasketRequest.Types.Type.GetMatches:
					{
						Console.WriteLine("Get matches request ...");
						try
						{
							model2.Match[] matches;
							lock (server)
							{
								matches = server.getMatches();
							}
							return ProtoUtils.createGetMatchesResponse(matches);
						}
						catch (BasketException e)
						{
							return ProtoUtils.createErrorResponse(e.Message);
						}
					}

				case BasketRequest.Types.Type.UpdateMatches:
					{
						Console.WriteLine("Updating matches request ...");
						model2.Ticket ticket = ProtoUtils.getTicket(request);  //DTOUtils.getFromDTO(udto);
						try
						{
							model2.Match[] matches;
							lock (server)
							{

								server.sendUpdate(ticket);
							}
							return ProtoUtils.createOkResponse();
						}
						catch (BasketException e)
						{
							return ProtoUtils.createErrorResponse(e.Message);
						}
					}
			}
			return response;
		}

        private void sendResponse(BasketResponse response)
        {
			Console.WriteLine("sending response " + response);
			lock (stream)
			{
				response.WriteDelimitedTo(stream);
				stream.Flush();
			}
		}

        public void updateReceived(model2.Match[] matches)
        {
			Console.WriteLine("Match list updated");
			try
			{
				sendResponse(ProtoUtils.createUpdateMatchesResponse(matches));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
    }
}
