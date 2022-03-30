using System.Collections.Generic;
using System.Data;
using log4net;
using MPP.model;
using System;

namespace MPP.repository
{
    public class RepoDBMatch : IRepoMatch
    {
        private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");

        IDictionary<string, string> props;

        public RepoDBMatch(IDictionary<string, string> props)
        {
            log.Info("Creating RepoTicketDB");
            this.props = props;
        }

        public void add(Match entity)
        {
            log.InfoFormat("saving match {0}", entity);
            var con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Match  values (@Id, @Team1, @Team2, @Type, @NrOfSeats, @Price, @Date)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramT1 = comm.CreateParameter();
                paramT1.ParameterName = "@Team1";
                paramT1.Value = entity.Team1;
                comm.Parameters.Add(paramT1);

                var paramT2 = comm.CreateParameter();
                paramT2.ParameterName = "@Team2";
                paramT2.Value = entity.Team2;
                comm.Parameters.Add(paramT2);

                var paramType = comm.CreateParameter();
                paramType.ParameterName = "@Type";
                paramType.Value = entity.MatchType;
                comm.Parameters.Add(paramType);

                var paramNrOfSeats = comm.CreateParameter();
                paramNrOfSeats.ParameterName = "@NrOfSeats";
                paramNrOfSeats.Value = entity.NrOfSeats;
                comm.Parameters.Add(paramNrOfSeats);

                var paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@Price";
                paramPrice.Value = entity.Price;
                comm.Parameters.Add(paramPrice);

                var paramDate = comm.CreateParameter();
                paramDate.ParameterName = "@Date";
                paramDate.Value = entity.Date;
                comm.Parameters.Add(paramDate);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} instance", result);
                if (result == 0)
                {
                    log.Error("No match added !");
                    throw new Exception("No match added !");
                }
            }
        }

        public void delete(Match entity)
        {
            log.InfoFormat("deleting match {0}", entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Match where Id=@Id";

                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No match deleted!");
                    throw new Exception("No match deleted!");
                }
            }
        }

        public void update(Match entity, int id)
        {
            log.InfoFormat("updating match {0} with {1}", id, entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update Match set Team1=@Team1,Team2=@Team2,Type=@Type,NrOfSeats=@NrOfSeats,Date=@Date where Id=@Id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var paramT1 = comm.CreateParameter();
                paramT1.ParameterName = "@Team1";
                paramT1.Value = entity.Team1;
                comm.Parameters.Add(paramT1);

                var paramT2 = comm.CreateParameter();
                paramT2.ParameterName = "@Team2";
                paramT2.Value = entity.Team2;
                comm.Parameters.Add(paramT2);

                var paramType = comm.CreateParameter();
                paramType.ParameterName = "@Type";
                paramType.Value = entity.MatchType;
                comm.Parameters.Add(paramType);

                var paramNrOfSeats = comm.CreateParameter();
                paramNrOfSeats.ParameterName = "@NrOfSeats";
                paramNrOfSeats.Value = entity.NrOfSeats;
                comm.Parameters.Add(paramNrOfSeats);

                var paramDate = comm.CreateParameter();
                paramDate.ParameterName = "@Date";
                paramDate.Value = entity.Date;
                comm.Parameters.Add(paramDate);

                var paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@Price";
                paramPrice.Value = entity.Price;
                comm.Parameters.Add(paramPrice);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No match  updated!");
                    throw new Exception("No match updated!");
                }
            }
        }

        public Match findbyId(int id)
        {
            log.InfoFormat("finding one match {0}", id);
            var con = DBUtils.getConnection(props);
            Match match = new Match();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Team1,Team2,Type,NrOfSeats,Price,Date from Match where Id = @Id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        string t1 = dataR.GetString(1);
                        string t2 = dataR.GetString(2);
                        string type = dataR.GetString(3);
                        int nrs = dataR.GetInt32(4);
                        double price = dataR.GetDouble(5);
                        DateTime date = DateTime.ParseExact(dataR.GetString(6), "yyyy-MM-dd HH:mm", null);
                        match.Id = idb;
                        match.Team1 = t1;
                        match.Team2 = t2;
                        match.MatchType = type;
                        match.NrOfSeats = nrs;
                        match.Price = price;
                        match.Date = date;
                    }
                }
            }
            log.InfoFormat("Found {0} instance", match);
            return match;
        }

        public ICollection<Match> findAll()
        {
            log.InfoFormat("finding all matches");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Match> matches = new List<Match>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Team1,Team2,Type,NrOfSeats,Price,Date from Match";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        string t1 = dataR.GetString(1);
                        string t2 = dataR.GetString(2);
                        string type = dataR.GetString(3);
                        int nrs = dataR.GetInt32(4);
                        double price = dataR.GetDouble(5);
                        DateTime date = DateTime.ParseExact(dataR.GetString(6), "yyyy-MM-dd HH:mm", null);
                        Match match = new Match(idb, t1, t2, type, nrs, price, date);
                        matches.Add(match);
                    }
                }
            }
            log.InfoFormat("found {0} matches", matches.Count);
            return matches;
        }

        public void updateNoOfSeats(int noOfSeats, int id)
        {
            log.InfoFormat("updating quantity for match {0} with {1}", id, noOfSeats);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update Match set NrOfSeats=@NrOfSeats where Id=@Id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var paramNrOfSeats = comm.CreateParameter();
                paramNrOfSeats.ParameterName = "@NrOfSeats";
                paramNrOfSeats.Value = noOfSeats;
                comm.Parameters.Add(paramNrOfSeats);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No match  updated!");
                    throw new Exception("No match updated!");
                }
            }
        }

        public ICollection<Match> getAllDescendingNoOfSeats()
        {
            log.InfoFormat("finding all matches");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Match> matches = new List<Match>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Team1,Team2,Type,NrOfSeats,Price,Date from Match order by NrOfSeats desc";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        string t1 = dataR.GetString(1);
                        string t2 = dataR.GetString(2);
                        string type = dataR.GetString(3);
                        int nrs = dataR.GetInt32(4);
                        double price = dataR.GetDouble(5);
                        DateTime date = DateTime.ParseExact(dataR.GetString(6), "yyyy-MM-dd HH:mm", null);
                        Match match = new Match(idb, t1, t2, type, nrs, price, date);
                        matches.Add(match);
                    }
                }
            }
            log.InfoFormat("found {0} matches", matches.Count);
            return matches;
        }
    }
}

