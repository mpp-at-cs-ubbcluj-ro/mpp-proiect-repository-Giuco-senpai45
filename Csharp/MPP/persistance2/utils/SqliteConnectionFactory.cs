using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace persistance2.utils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string, string> props)
        {
            string connectionString = props["ConnectionString"];
            //string connectionString = "D://Proiecte Java//Proiect MPP//mpp-proiect-repository-Giuco-senpai45//databases//basket.db";
            return new SQLiteConnection(connectionString); 
        }
    }
}