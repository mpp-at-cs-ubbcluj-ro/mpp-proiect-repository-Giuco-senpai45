using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model2;

namespace networking2
{

	public interface Response
	{
	}

	[Serializable]
	public class OkResponse : Response
	{

	}

	[Serializable]
	public class ErrorResponse : Response
	{
		private string message;

		public ErrorResponse(string message)
		{
			this.message = message;
		}

		public virtual string Message
		{
			get
			{
				return message;
			}
		}
	}

	[Serializable]
	public class GetMatchesResponse : Response
	{
		private Match[] matches;
		public GetMatchesResponse(Match[] matches)
		{
			this.matches = matches;
		}
		public virtual Match[] Matches
		{
			get
			{
				return matches;
			}
		}
	}

	[Serializable]
	public class LoginResponse : Response
	{
		private Organiser user;

		public LoginResponse(Organiser user)
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
	public class LogoutResponse : Response
	{
		private Organiser user;

		public LogoutResponse(Organiser user)
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

	public interface UpdateResponse : Response
	{
	}

	[Serializable]
	public class NewMatchesResponse : UpdateResponse
	{
		private Match[] matches;
		public NewMatchesResponse(Match[] matches)
		{
			this.matches = matches;
		}
		public virtual Match[] Matches
		{
			get
			{
				return matches;
			}
		}
	}
}