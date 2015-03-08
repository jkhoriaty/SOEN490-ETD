using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ETD.Models.Objects;
using ETD.Services.Database.Queries;
using System.IO;

namespace ETD.Services.Database
{
    class DBConnection
    {
        private SQLiteConnection m_dbConnection;

        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile(@".\Resources\EDT.sqlite3");
            m_dbConnection = new SQLiteConnection(@"Data Source='.\Resources\EDT.sqlite3';Version=3;");
            m_dbConnection.Open();
            String query = File.ReadAllText(@".\Resources\db.sql");
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();            
        }

        private void OpenDatabase()
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

        private void CloseConnection()
        {
            m_dbConnection.Close();
        }

        public void AddVolunteer(AddVolunteerQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public void AddEvent(AddEventQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public void AddIntervention(AddInterventionQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        public void AddCall(AddCallQuery query)
        {
            QueryDatabse(query.GetQuery());
        }

        private void QueryDatabse(string query)
        {
            if ((m_dbConnection == null) || (m_dbConnection.State == System.Data.ConnectionState.Closed))
            {
                OpenDatabase();
                while(m_dbConnection.State != System.Data.ConnectionState.Open)
                {
                    //Ensures connection is properly open before continuing
                }
            }
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public void testConnection()
        {
            OpenDatabase();
            if (m_dbConnection.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Database Connected.");
            else
                Console.WriteLine("Database Not Connected.");
            CloseConnection();
        }
    }
}
