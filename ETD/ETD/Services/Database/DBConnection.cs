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

       public DBConnection()
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

        private int NonQueryDatabase(string query)
        {
            if(m_dbConnection.State == System.Data.ConnectionState.Broken)
            {
                CloseConnection();
            }
            if (m_dbConnection.State == System.Data.ConnectionState.Closed)
            {
                OpenDatabase();
            }
            
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            int results = command.ExecuteNonQuery();
            CloseConnection();
            return results;
        }

        private SQLiteDataReader QueryDatabase(string query)
        {
            if (m_dbConnection.State == System.Data.ConnectionState.Broken)
            {
                CloseConnection();
            }
            if (m_dbConnection.State == System.Data.ConnectionState.Closed)
            {
                OpenDatabase();
            }
            SQLiteCommand command = new SQLiteCommand(query, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            CloseConnection();
            return reader;
        }
        public int CreateABC(CreateABCQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateAdditionalInformation(CreateAdditionalInformationQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateAssignedEquipment(CreateAssignedEquipmentQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateAvailableEquipment(CreateAvailableEquipmentQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateIntervention(CreateInterventionQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateOperation(CreateOperationQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateResource(CreateResourceQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateTeamMember(CreateTeamMemberQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateTeam(CreateTeamQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }
        public int CreateVolunteer(CreateVolunteerQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }

        public SQLiteDataReader GetABC(GetABCQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetAdditionalInformation(GetAdditionalInformationQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetAssignedEquipment(GetAssignedEquipmentQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetAvailableEquipment(GetAvailableEquipmentQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetIntervention(GetInterventionQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetOperation(GetOperationQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetResource(GetResourceQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetTeamMember(GetTeamMemberQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetTeam(GetTeamQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }
        public SQLiteDataReader GetVolunteer(GetVolunteerQuery query)
        {
            return QueryDatabase(query.GetQuery());
        }

        public int UpdateOperation(UpdateOperationQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }

        

        

        public int UpdateABC(UpdateABCQuery query)
        {
            return NonQueryDatabase(query.GetQuery());
        }



        public void testConnection()
        {
            OpenDatabase();
            if (m_dbConnection.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Connected to database.");
            else
                Console.WriteLine("Can't connect to database.");
            CloseConnection();
        }
    }
}
