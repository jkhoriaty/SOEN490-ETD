using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ETD_Statistic.Model;
using ETD.Services.Database;
using System.Collections;

//class to store volunteer statistic objects and basic setters and getters

namespace ETD_Statistic.Model
{
    public class VolunteerStatistic
    {
        String volunteerName;
        DateTime startTime;
        DateTime endTime;
        TimeSpan timeDiff;
        String operationID;
        public VolunteerStatistic(String name, DateTime start, DateTime end, String opID)
        {
            volunteerName = name;
            startTime = start;
            endTime = end;
            timeDiff = getDateDifference(start,end);
            operationID = opID;
        }

        public void setName(String name)
        {
            volunteerName = name;
        }

        public String getName()
        {
            return volunteerName;
        }

        public void setStart(DateTime start)
        {
            startTime = start;
        }

        public DateTime getStart()
        {
            return startTime;
        }

        public void setEnd(DateTime end)
        {
            endTime = end;
        }

        public DateTime getEnd()
        {
            return endTime;
        }

        public TimeSpan getTimeDiff()
        {
            return timeDiff;
        }

        public String getOperationID()
        {
            return operationID;
        }

        public TimeSpan getDateDifference(DateTime start, DateTime end)
        {
            TimeSpan timeDifference = end.Subtract(start);
            return timeDifference;
        }

    }
}
