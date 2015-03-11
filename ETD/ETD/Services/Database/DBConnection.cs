using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SQLite;
using ETD.Models.Objects;
using ETD.Services.Database.Queries;
using System.IO;

namespace ETD.Services.Database
{
    class DBConnection
    {
 /*       private SQLiteConnection m_dbConnection;

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
            while (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                //Ensures connection is properly open before continuing
            }
        }

        private void CloseConnection()
        {
            m_dbConnection.Close();
            while (m_dbConnection.State != System.Data.ConnectionState.Closed)
            {
                //Ensures connection is properly open before continuing
            }
        }

        public void AddVolunteer(CreateVolunteerQuery query)
        {
            NonQueryDatabse(query.GetQuery());
        }

        public void AddEvent(CreateEventQuery query)
        {
            NonQueryDatabse(query.GetQuery());
        }

        public void AddIntervention(CreateInterventionQuery query)
        {
            NonQueryDatabse(query.GetQuery());
        }

        public void AddCall(CreateCallQuery query)
        {
            NonQueryDatabse(query.GetQuery());
        }

        private void NonQueryDatabse(string query)
        {
            if ((m_dbConnection == null) || (m_dbConnection.State == System.Data.ConnectionState.Closed))
            {
                OpenDatabase();
            }
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            command.ExecuteNonQuery();
            CloseConnection();
        }

        public void testConnection()
        {
            OpenDatabase();
            if (m_dbConnection.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Connected to database.");
            else
                Console.WriteLine("Can't connect to database.");
            CloseConnection();
        }*/
    }
}
