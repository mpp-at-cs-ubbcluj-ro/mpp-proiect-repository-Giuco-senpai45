using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;
using proto = Proto;
using model2;
using Google.Protobuf.WellKnownTypes;

namespace protobuf
{
    static class ProtoUtils
    {
        /*
        public static BasketRequest createLoginRequest(model2.Organiser organiser)
        {
            proto.Organiser userDTO = new proto.Organiser { Id = organiser.Id, Name = organiser.Name, Passwd = organiser.Password };
            BasketRequest request = new BasketRequest { Type = BasketRequest.Types.Type.Login, User = userDTO };

            return request;
        }

        public static BasketRequest createLogoutRequest(model2.Organiser organiser)
        {
            proto.Organiser userDTO = new proto.Organiser { Id = organiser.Id, Name = organiser.Name, Passwd = organiser.Password };
            BasketRequest request = new BasketRequest { Type = BasketRequest.Types.Type.Logout, User = userDTO };

            return request;
        }

        public static BasketRequest createGetMatchesRequest()
        {
            BasketRequest request = new BasketRequest { Type = BasketRequest.Types.Type.GetMatches };
            return request;
        }

        public static BasketRequest createUpdateMatchesRequest(model2.Ticket ticket)
        {

            proto.Match matchDto = new proto.Match { Id = ticket.TicketMatch.Id };
            proto.Ticket ticketDto = new proto.Ticket { Match = matchDto, Quantity = ticket.Quantity, CustomerName = ticket.Name };

            BasketRequest request = new BasketRequest { Type = BasketRequest.Types.Type.UpdateMatches, Ticket = ticketDto };
            return request;
        }
        */

        public static BasketResponse createOkResponse()
        {
            BasketResponse response = new BasketResponse { Type = BasketResponse.Types.Type.Ok };
            return response;
        }


        public static BasketResponse createErrorResponse(String text)
        {
            BasketResponse response = new BasketResponse
            {
                Type = BasketResponse.Types.Type.Error,
                Error = text
            };
            return response;
        }

        public static BasketResponse createLoggedInResponse(model2.Organiser organiser)
        {
            proto.Organiser userDTO = new proto.Organiser { Id = organiser.Id, Name = organiser.Name, Passwd = organiser.Password };
            BasketResponse response = new BasketResponse { Type = BasketResponse.Types.Type.OrgLoggedIn, User = userDTO };

            return response;
        }

        public static BasketResponse createLoggedOutResponse(model2.Organiser organiser)
        {
            proto.Organiser userDTO = new proto.Organiser { Id = organiser.Id, Name = organiser.Name, Passwd = organiser.Password };
            BasketResponse response = new BasketResponse { Type = BasketResponse.Types.Type.OrgLoggedOut, User = userDTO };

            return response;
        }

        public static BasketResponse createGetMatchesResponse(model2.Match[] matches)
        {
            BasketResponse response = new BasketResponse
            {
                Type = BasketResponse.Types.Type.GotMatches
            };

            foreach (model2.Match m in matches)
            {
                DateTime md = DateTime.SpecifyKind(m.Date, DateTimeKind.Utc);
                var tm = Timestamp.FromDateTime(md);
                Proto.Match matchDto = new Proto.Match { Id = m.Id, Team1 = m.Team1, Team2 = m.Team2, Type = m.MatchType, NrOfSeats = m.NrOfSeats, Price = m.Price, Date = tm };


                response.Matches.Add(matchDto);
            }

            return response;
        }

        public static BasketResponse createUpdateMatchesResponse(model2.Match[] matches)
        {
            BasketResponse response = new BasketResponse
            {
                Type = BasketResponse.Types.Type.NewMatchList
            };

            foreach (model2.Match m in matches)
            {
                DateTime md = DateTime.SpecifyKind(m.Date, DateTimeKind.Utc);
                var tm = Timestamp.FromDateTime(md);
                proto.Match matchDto = new proto.Match { Id = m.Id, Team1 = m.Team1, Team2 = m.Team2, Type = m.MatchType, NrOfSeats = m.NrOfSeats, Price = m.Price, Date = tm };


                response.Matches.Add(matchDto);
            }
            return response;
        }

        public static String getError(BasketResponse response)
        {
            String errorMessage = response.Error;
            return errorMessage;
        }

        public static model2.Organiser getUser(BasketRequest request)
        {
            model2.Organiser user = new model2.Organiser(request.User.Id, request.User.Name, request.User.Passwd);
            return user;
        }

        public static model2.Organiser getUser(BasketResponse response)
        {
            model2.Organiser user = new model2.Organiser(response.User.Id, response.User.Name, response.User.Passwd);
            return user;
        }

        public static model2.Match[] getMatches(BasketResponse response)
        {
            model2.Match[] matches = new model2.Match[response.Matches.Count];
            for (int i = 0; i < response.Matches.Count; i++)
            {
                proto.Match m = response.Matches[i];
                DateTime tm = m.Date.ToDateTime();
                model2.Match match = new model2.Match { Id = m.Id, Team1 = m.Team1, Team2 = m.Team2, MatchType = m.Type, NrOfSeats = m.NrOfSeats, Price = m.Price, Date = tm };

                matches[i] = match;
            }
            return matches;
        }

        public static model2.Ticket getTicket(BasketRequest request)
        {
            model2.Match match = new model2.Match { Id = request.Ticket.Match.Id };

            model2.Ticket ticket = new model2.Ticket(request.Ticket.Id, request.Ticket.Quantity, match, request.Ticket.CustomerName);
            return ticket;
        }
    }
}
