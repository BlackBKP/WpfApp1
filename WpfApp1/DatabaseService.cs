using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WpfApp1
{
    public class DatabaseService : IDatabase
    {
        public SQLiteConnection Connect()
        {
            SQLiteConnection Connection = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            Connection.Open();
            return Connection;
        }

        public void CreateTable(SQLiteConnection Connection)
        {
            SQLiteCommand sqlite_cmd;
            string string_cmd = "CREATE TABLE if not exists lots(Lot_ID TEXT NOT NULL, No INTEGER NOT NULL,Date TEXT NOT NULL, WEIGHT INTEGER NOT NULL)";
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = string_cmd;
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
