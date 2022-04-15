using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace persistance.utils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string, string> props)
        {
            string connectionString = "D://Proiecte Java//Proiect MPP//mpp-proiect-repository-Giuco-senpai45//databases//basket.db";
            return new SqliteConnection(connectionString);
        }
    }
}