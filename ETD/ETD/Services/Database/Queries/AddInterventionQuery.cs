using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class AddInterventionQuery : DBQuery
    {
        public AddInterventionQuery(int ID, string event_Name)
        {
            sql = "INSERT INTO [Interventions] (Intervention_ID, Event_ID, Start_Time) VALUES ('" + ID + "', '" + event_Name + "', CURRENT_TIME)";
        }
    }
}
