using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MPP.utils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string, string> props)
        {
            String connectionString = "Data Source=D://Proiecte Java//Proiect MPP//mpp-proiect-repository-Giuco-senpai45//Csharp//MPP//MPP//databases//basket.db";
            return new SqliteConnection(connectionString);
        }
    }
}