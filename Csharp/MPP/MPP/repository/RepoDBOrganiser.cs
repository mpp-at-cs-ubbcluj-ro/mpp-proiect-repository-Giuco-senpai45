using System.Data;
using MPP.model;
using System;
using log4net;
using System.Collections.Generic;

namespace MPP.repository
{
    public class RepoDBOrganiser : IRepoOrganiser
    {

        //private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        private static readonly ILog log = LogManager.GetLogger("RepoDBOrganiser");


        IDictionary<String, string> props;

        public RepoDBOrganiser(IDictionary<String, string> props)
        {
            log.Info("Creating RepoOrganiserDB");
            this.props = props;
        }

        public void add(Organiser entity)
        {
            log.InfoFormat("saving organiser {0}", entity);
            var con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Organiser  values (@Id, @Name,@Password)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = entity.Name;
                comm.Parameters.Add(paramName);

                var paramPass = comm.CreateParameter();
                paramPass.ParameterName = "@Password";
                paramPass.Value = entity.Password;
                comm.Parameters.Add(paramPass);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved {0} instance", result);
                if (result == 0)
                {
                    log.Error("No organiser added !");
                    throw new Exception("No organiser added !");
                }
            }

        }

        public void delete(Organiser entity)
        {
            log.InfoFormat("deleting organiser {0}", entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Organiser where Id=@Id";

                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No organiser deleted!");
                    throw new Exception("No organiser deleted!");
                }
            }
        }

        public void update(Organiser entity, int id)
        {
            log.InfoFormat("updating organiser {0} with {1}", id, entity);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update Organiser set Name=@Name  where Id=@Id";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = entity.Name;
                comm.Parameters.Add(paramName);

                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Updated {0} instance", dataR);
                if (dataR == 0)
                {
                    log.Error("No organiser  updated!");
                    throw new Exception("No organiser updated!");
                }
            }
        }

        public Organiser findbyId(int id)
        {
            log.InfoFormat("finding one organiser {0}", id);
            var con = DBUtils.getConnection(props);
            Organiser organiser = new Organiser();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Name,Password from Organiser where Id=@Id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string pass = dataR.GetString(2);
                        organiser.Id = idb;
                        organiser.Name = name;
                        organiser.Password = pass;
                    }
                }
            }
            log.InfoFormat("Found {0} instance", organiser);
            return organiser;
        }

        public ICollection<Organiser> findAll()
        {
            log.InfoFormat("finding all organisers");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Organiser> organisers = new List<Organiser>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Name,Password from Organiser";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        String name = dataR.GetString(1);
                        String pass = dataR.GetString(2);
                        Organiser organiser = new Organiser(id, name, pass);
                        organisers.Add(organiser);
                    }
                }
            }
            log.InfoFormat("found {0} organisers", organisers.Count);
            return organisers;
        }

        public Organiser findByNameAndPassword(string name, string password)
        {
            log.InfoFormat("finding organiser by name {0}", name, password);
            var con = DBUtils.getConnection(props);
            Organiser organiser = new Organiser();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select Id,Name,Password from Organiser where Name=@Name and Password=@Password";
                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = name;
                comm.Parameters.Add(paramName);

                var paramPass = comm.CreateParameter();
                paramPass.ParameterName = "@Password";
                paramPass.Value = password;
                comm.Parameters.Add(paramPass);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idb = dataR.GetInt32(0);
                        string orgName = dataR.GetString(1);
                        string pass = dataR.GetString(2);
                        organiser.Id = idb;
                        organiser.Name = orgName;
                        organiser.Password = pass;
                    }
                }
            }
            log.InfoFormat("Found {0} instance", organiser);
            return organiser;
        }
    }
}

