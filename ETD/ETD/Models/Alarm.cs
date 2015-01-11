using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models
{
    class Alarm
    {
        int hours;
        int minutes;
        DateTime newDepartTime;
        DateTime preDepartTime;

        public Alarm(DateTime departureTime)
        {
            hours = departureTime.Hour;
            minutes = departureTime.Minute;
        }

        public DateTime checkDepartureTime(DateTime departureTime)
        {
            if (hours < DateTime.Now.Hour)
            {
                newDepartTime = new DateTime(departureTime.Year, departureTime.Month, departureTime.Day+1, departureTime.Hour, departureTime.Minute, departureTime.Second);
                return newDepartTime;
            }
            return departureTime;
        }
    }
}
