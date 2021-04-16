using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DecryptSqilteCore2
{
    public class SqlSugarMethod
    {
        private readonly static string key = "123456";
        public User GetUser()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLCipher.db3");

            ConnectionConfig config = new ConnectionConfig()
            {
                DbType = SqlSugar.DbType.Sqlite,
                ConnectionString = $"Data Source={path}",
                IsAutoCloseConnection = false
            };

            SqlSugarClient client = new SqlSugarClient(config);

            client.Open();

            client.Ado.ExecuteCommand($"PRAGMA key ={key}");

            var data = client.Queryable<User>().First();

            client.Close();

            return data;
        }
    }
}
