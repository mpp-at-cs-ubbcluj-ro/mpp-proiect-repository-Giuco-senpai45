using System.Data;
using log4net;
using System.Collections.Generic;
using System;
using model2;

namespace persistance2
{
    public class RepoDBTicket : IRepoTicket
    {
        //private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        private static readonly ILog log = LogManager.GetLogger("RepoDBTicket");

        IDictionary<string, string> props;

        public RepoDBTicket(IDictionary<string, string> props)
        {
            log.Info("Creating RepoTicketDB");
            this.props = props;
        }

        public void add(Ticket entity)
        {
            log.InfoFormat("saving ticket {0}", entity);
            var con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Ticket(Mid,Quantity,CustomerName)  values (@Mid, @Quantity, @CustomerName)";
                /*var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);*/

                var paramMid = comm.CreateParameter();
                paramMid.ParameterName = "@Mid";
                paramMid.Value = entity.TicketMatch.Id;
                comm.Parameters.Add(paramMid);

                var paramQuantity = comm.CreateParameter();
                paramQuantity.ParameterName = "@Quantity";
                paramQuantity.Value = entity.Quantity;
                comm.Parameters.Add(paramQuantity);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@CustomerName";
                paramName.Value = entity.Name;
                comm.Parameters.Add(paramName);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} instance", result);
                if (result == 0)
                {
                    log.Error("No ticket added !");
                    throw new Exception("No ticket added !");
                }
            }
        }

        public void delete(Ticket entity)
        {
            log.InfoFormat("deleting ticket {0}", entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Ticket where Id=@Id";

                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No ticket deleted !");
                    throw new Exception("No ticket deleted !");
                }
            }
        }

        public void update(Ticket entity, int id)
        {
            log.InfoFormat("updating ticket {0} with {1}", id, entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update Ticket set Mid=@Mid, Quantity=@Quantity, CustomerName=@CustomerName  where Id=@Id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var paramMid = comm.CreateParameter();
                paramMid.ParameterName = "@Mid";
                paramMid.Value = entity.TicketMatch.Id;
                comm.Parameters.Add(paramMid);

                var paramQuantity = comm.CreateParameter();
                paramQuantity.ParameterName = "@Quantity";
                paramQuantity.Value = entity.Id;
                comm.Parameters.Add(paramQuantity);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@CustomerName";
                paramName.Value = entity.Name;
                comm.Parameters.Add(paramName);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No ticket updated !");
                    throw new Exception("No ticket updated !");
                }
            }
        }

        public Ticket findbyId(int id)
        {
            log.InfoFormat("finding one ticket {0}", id);
            var con = DBUtils.getConnection(props);
            Ticket ticket = new Ticket();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Mid,Quantity,CustomerName from Ticket where Id=@Id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        int mid = dataR.GetInt32(1);
                        int quantity = dataR.GetInt32(2);
                        string name = dataR.GetString(3);
                        ticket.Id = idb;
                        Match match = new Match(mid);
                        ticket.TicketMatch = match;
                        ticket.Quantity = quantity;
                        ticket.Name = name;
                    }
                }
            }
            log.InfoFormat("Found {0} instance", ticket);
            return ticket;
        }

        public ICollection<Ticket> findAll()
        {
            log.InfoFormat("finding all tickets");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Ticket> tickets = new List<Ticket>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Mid,Quantity,CustomerName from Ticket";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        int mid = dataR.GetInt32(1);
                        int quantity = dataR.GetInt32(2);
                        string name = dataR.GetString(3);
                        Ticket ticket = new Ticket();
                        ticket.Id = idb;
                        Match match = new Match(mid);
                        ticket.TicketMatch = match;
                        ticket.Quantity = quantity;
                        ticket.Name = name;
                        tickets.Add(ticket);
                    }
                }
            }
            log.InfoFormat("found {0} organisers", tickets.Count);

            return tickets;
        }
    }
}
