using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.Services.Database;
using ETD.Services.Database.Queries;
using ETD.Models.Objects;


namespace ETD.Services
{
    static class DatabaseServices
    {
        private static DBConnection connection;

        static DatabaseServices()
        {
            connection = new DBConnection();
        }

        public static bool createTeam(Team team)
        {
            
            return false;
        }

        public static bool createIntervention(Intervention intervention)
        {
            /*
            CreateInterventionQuery query = new CreateInterventionQuery(intervention.getInterventionNumber(), intervention.ToString());
            try 
            {
                connection.AddIntervention(query);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to add intervention to database.");
            }*/
            return false;
        }

        public static bool createOperation(Operation operation)
        {
            return false;        }
    }
}
