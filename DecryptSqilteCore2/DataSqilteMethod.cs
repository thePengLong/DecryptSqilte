using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DecryptSqilteCore2
{
    public class DataSqilteMethod
    {
        private readonly static string key = "123456";

        public User GetUser()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSQLite.db3");
            
            SQLiteConnection clinet = new SQLiteConnection($"Data Source={path}");
            
            clinet.Open();

            SQLiteCommand command = clinet.CreateCommand();

            command.CommandText = "PRAGMA key = " + key;

            command.ExecuteNonQuery();

            command.CommandText = "select Name from User";

            DataTable dt = new DataTable();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            adapter.Fill(dt);

            clinet.Close();

            return new User { Name = dt.Rows[0][0].ToString() };
        }
    }
}
