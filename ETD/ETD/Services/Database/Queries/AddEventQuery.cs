using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class AddEventQuery : DBQuery
    {
        public AddEventQuery(string event_Code, string name, DateTime startDate, DateTime endDate, string location)
        {
            sql = "INSERT INTO [Events] (Event_ID, Name, Start_Date, End_Date, Location) VALUES ('" + event_Code + "', '" + name + "', '" + startDate.ToString("yyyyMMdd") + "', '" + endDate.ToString("yyyyMMdd") + "', '" + location + "')";
        }
    }
}
