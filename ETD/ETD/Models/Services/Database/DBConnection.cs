using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ETD.Models.Objects;
using ETD.Models.Services.Database.Queries;
using System.IO;

namespace ETD.Models.Services.Database
{
    class DBConnection
    {
        static SQLiteConnection m_dbConnection;

        private static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(@".\Resources\EDT.sqlite3");
            m_dbConnection = new SQLiteConnection(@"Data Source='.\Resources\EDT.sqlite3';Version=3;");
            m_dbConnection.Open();
            String query = File.ReadAllText(@".\Resources\db.sql");
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();            
        }

        private static void OpenDatabase()
        {
            if (m_dbConnection == null)
            {
                if (!File.Exists(@".\Resources\EDT.sqlite3"))
                {
                    CreateDatabase();
                }
                else
                {
                    m_dbConnection = new SQLiteConnection(@"Data Source='.\Resources\EDT.sqlite3';Version=3;");
                }
            }
            m_dbConnection.Open();
        }

        private static void CloseConnection()
        {
            m_dbConnection.Close();
        }

        public static void AddVolunteer(AddVolunteerQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public static void AddEvent(AddEventQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public static void AddIntervention(AddInterventionQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public static void AddCall(AddCallQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        private static void QueryDatabse(string query)
        {
            if ((m_dbConnection == null) || (m_dbConnection.State == System.Data.ConnectionState.Closed))
            {
                OpenDatabase();
            }
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            command.ExecuteNonQuery();
            CloseConnection();
        }
    }
}
