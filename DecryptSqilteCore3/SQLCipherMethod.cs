using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace DecryptSqilteCore3
{
    public class SQLCipherMethod
    {
        private readonly static string key = "123456";
        public User GetUser()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLCipher.db3");

            SqliteConnection client = new SqliteConnection
            {
                ConnectionString = new SqliteConnectionStringBuilder($"Data Source={path}") { Password = key, Mode = SqliteOpenMode.ReadWriteCreate }.ToString()
            };

            SQLitePCL.raw.SetProvider(new SQLitePCL.sql());

            client.Open();

            SqliteCommand command = client.CreateCommand();

            command.CommandText = "select Name from User";

            DataTable dt = new DataTable();

            #region 使用sqlsugar的SqliteDataAdapter
            //SqlSugar.SqliteDataAdapter adapter = new SqlSugar.SqliteDataAdapter(command);

            //adapter.Fill(dt); 
            #endregion

            //sqlsugar的SqliteDataAdapter的Fill的具体实现如下,代替上面部分
            #region 

            DataColumnCollection columns = dt.Columns;

            DataRowCollection rows = dt.Rows;

            using (SqliteDataReader sqliteDataReader = command.ExecuteReader())
            {
                for (int i = 0; i < sqliteDataReader.FieldCount; i++)
                {
                    string text = sqliteDataReader.GetName(i).Trim();
                    if (!columns.Contains(text))
                    {
                        columns.Add(new DataColumn(text, sqliteDataReader.GetFieldType(i)));
                    }
                }
                while (sqliteDataReader.Read())
                {
                    DataRow dataRow = dt.NewRow();
                    for (int j = 0; j < columns.Count; j++)
                    {
                        dataRow[columns[j].ColumnName] = sqliteDataReader.GetValue(j);
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            #endregion

            client.Close();

            return new User { Name = dt.Rows[0][0].ToString() };
        }
    }
}
