using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.Services.Database;
using System.Data.SQLite;

namespace ETD_Statistic.Model
{
    class OperationStatisticMapper
    {
        private List<OperationStatistic> operationStatisticList = new List<OperationStatistic>();


        String dbQuery = "SELECT * FROM Operations WHERE Operation_ID IN " + Statistic.getOperationID();
        String dbQueryVolunteerCount = "SELECT count(*) as Num FROM (Volunteers JOIN Team_Members ON Volunteers.Volunteer_ID = Team_Members.Volunteer_ID) JOIN Teams ON Team_Members.Team_ID = Teams.Team_ID WHERE Operation_ID IN " + Statistic.getOperationID();
        String dbQueryTeamCount = "SELECT count(*) as Count FROM Teams WHERE Operation_ID IN " + Statistic.getOperationID();

        public OperationStatisticMapper()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase(dbQuery);
            while (reader.Read())
            {
                DateTime startDate = Convert.ToDateTime(reader["Shift_Start"].ToString());
                DateTime endDate = Convert.ToDateTime(reader["Shift_End"].ToString());
                String volunteerFollowUpText = reader["VolunteerFollowUp"].ToString();
                String financeText = reader["Finance"].ToString();
                String vehicleText = reader["Vehicle"].ToString();
                String particularSituationText = reader["ParticularSituation"].ToString();
                String organizationFollowUpText = reader["OrganizationFollowUp"].ToString();
                String supervisorFollowUpText = reader["SupervisorFollowUp"].ToString();
                String eventName = reader["Name"].ToString();
                String dispatcherName = reader["Dispatcher"].ToString();

                OperationStatistic os = new OperationStatistic(startDate, endDate, volunteerFollowUpText, financeText, vehicleText, particularSituationText, organizationFollowUpText, supervisorFollowUpText, eventName, dispatcherName);

                operationStatisticList.Add(os);
            }
            StaticDBConnection.CloseConnection();

        }

        public List<OperationStatistic> getList()
        {
            return operationStatisticList;
        }

        public void ClearList()
        {
            operationStatisticList.Clear();
        }

        public int getTeamCountFromDB()
        {
            int teamCount = 0;
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase(dbQueryTeamCount);
            while (reader.Read())
            {
                teamCount = int.Parse(reader["Count"].ToString());
            }
            StaticDBConnection.CloseConnection();
            return teamCount;
 
        }

        public int getVolunteerCountFromDB()
        {
            int volunteerCount = 0;
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase(dbQueryVolunteerCount);
            while (reader.Read())
            {
                volunteerCount = int.Parse(reader["Num"].ToString());
            }
            StaticDBConnection.CloseConnection();
            return volunteerCount;
 
        }

    }
}
