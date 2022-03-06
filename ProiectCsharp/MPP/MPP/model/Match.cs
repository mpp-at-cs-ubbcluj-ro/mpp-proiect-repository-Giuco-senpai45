using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    internal class Match : Identifiable<int>
    {
        private int _id;
        private string _team1;
        private string _team2;
        private string _type;
        private int _nrOfSeats;

        public Match(int id, string team1, string team2, string type, int nrOfSeats)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            Type = type;
            NrOfSeats = nrOfSeats;
        }

        public Match(string team1, string team2, string type, int nrOfSeats)
        {
            Team1 = team1;
            Team2 = team2;
            Type = type;
            NrOfSeats = nrOfSeats;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Team1
        {
            get { return _team1; }
            set { _team1 = value; }
        }

        public string Team2
        {
            get { return _team2; }
            set { _team2 = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int NrOfSeats
        {
            get { return _nrOfSeats; }
            set { _nrOfSeats = value; }
        }
    }
}
