using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;

namespace networking2
{
	public interface Request
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private Organiser user;

		public LoginRequest(Organiser user)
		{
			this.user = user;
		}

		public virtual Organiser User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class LogoutRequest : Request
	{
		private Organiser user;

		public LogoutRequest(Organiser user)
		{
			this.user = user;
		}

		public virtual Organiser User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class GetMatches : Request
	{
		public GetMatches()
		{
		}
	}

	[Serializable]
	public class SendUpdateRequest : Request
	{
		private Ticket ticket;
		public SendUpdateRequest(Ticket ticket)
		{
			this.ticket = ticket;
		}
		public virtual Ticket Ticket
		{
			get
			{
				return ticket;
			}
		}
	}
}
