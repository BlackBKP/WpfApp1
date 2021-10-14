using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface IDatabase
    {
        SQLiteConnection Connect();
        void CreateTable(SQLiteConnection Connection);
    }
}
