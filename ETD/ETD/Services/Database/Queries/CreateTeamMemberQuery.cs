using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateTeamMemberQuery : DBQuery
    {
        public CreateTeamMemberQuery(int teamID, int volunteerID, DateTime departure)
        {
            sql = "INSERT INTO [Team_Members] (Team_ID, Volunteer_ID, Departure, Joined) VALUES (" + teamID + ", " + volunteerID + ", '" + DateTimeSQLite(departure) + ", '" + DateTimeSQLite(DateTime.Now) + "')";
        }
    }
}
