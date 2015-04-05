using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.Services.Database;
using System.Data.SQLite;


namespace ETD_Statistic.Model
{
    class VolunteerStatisticMapper
    {
        private List<VolunteerStatistic> volunteerStatisticList = new List<VolunteerStatistic>();
        String dbQuery = "SELECT Volunteers.Name as Name, Operation_ID, Joined, Departure FROM Team_Members JOIN Teams ON Teams.Team_ID = Team_Members.Team_ID JOIN Volunteers ON Team_Members.Volunteer_ID = Volunteers.Volunteer_ID WHERE Operation_ID IN " + Statistic.getOperationID();

        public VolunteerStatisticMapper()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase(dbQuery);
            while (reader.Read())
            {
                String volunteer = reader["Name"].ToString();
                DateTime start = Convert.ToDateTime(reader["Joined"].ToString());
                DateTime end = Convert.ToDateTime(reader["Departure"].ToString());
                String operationID = reader["Operation_ID"].ToString();

                //Creating volunteerStatistic
                VolunteerStatistic vs = new VolunteerStatistic(volunteer, start, end, operationID);
                volunteerStatisticList.Add(vs);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<VolunteerStatistic> getList()
        {
            orderList();
            return volunteerStatisticList;
        }

        public void ClearList()
        {
            volunteerStatisticList.Clear();
        }

        public void RemoveFromList(VolunteerStatistic vs)
        {
            volunteerStatisticList.Remove(vs);
        }

        private void orderList()
        {
            volunteerStatisticList = volunteerStatisticList.OrderBy(VolunteerStatistic => VolunteerStatistic.getName()).ToList();
        }

    }
}
