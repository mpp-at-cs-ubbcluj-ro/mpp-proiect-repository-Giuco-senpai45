using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    public class Match : Identifiable<int>
    {
        public int Id { get; set; }

        public string Team1
        {
            get;
            set;
        }
        public string Team2
        {
            get;
            set;
        }

        public string MatchType
        {
            get;
            set;
        }

        public int NrOfSeats
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public Match(int id, string team1, string team2, string matchType, int nrOfSeats, double price)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            MatchType = matchType;
            NrOfSeats = nrOfSeats;
            Price = price;
        }

        public Match(string team1, string team2, string matchType, int nrOfSeats, double price)
        {
            Team1 = team1;
            Team2 = team2;
            MatchType = matchType;
            NrOfSeats = nrOfSeats;
            Price = price;
        }

        public Match(string team1, string team2, string matchType, int nrOfSeats, double price, DateTime date)
        {
            Team1 = team1;
            Team2 = team2;
            MatchType = matchType;
            NrOfSeats = nrOfSeats;
            Price = price;
            Date = date;
        }

        public Match(int id, string team1, string team2, string matchType, int nrOfSeats, double price, DateTime date)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            MatchType = matchType;
            NrOfSeats = nrOfSeats;
            Price = price;
            Date = date;
        }

        public Match(int id)
        {
            Id = id;
        }
        
        public Match()
        {
            
        }

        public override string ToString()
        {
            return Id + " " + Team1 + " " + Team2 + " " + NrOfSeats + " " + Price;
        }
    }
}
