using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    internal class Ticket : Identifiable<int>
    {
        private int _id;
        private int _seat;
        private long _price;
        private int _matchId;

        public Ticket(int seat, long price, int matchId)
        {
            Seat = seat;
            Price = price;
            MatchId = matchId;
        }

        public Ticket(int id, int seat, long price, int matchId)
        {
            Id = id;
            Seat = seat;
            Price = price;
            MatchId = matchId;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Seat
        {
            get { return _seat; }
            set { _seat = value; }

        }
        public long Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int MatchId
        {
            get { return _matchId; }
            set { _matchId = value; }
        }
    }
}
