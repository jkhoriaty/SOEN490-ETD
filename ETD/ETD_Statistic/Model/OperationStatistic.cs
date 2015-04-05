using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD_Statistic.Model
{
    class OperationStatistic
    {
        DateTime startDate;
        DateTime endDate;
        String volunteerFollowup;
        String finance;
        String vehicle;
        String particularSituation;
        String organizationFollowup;
        String supervisorFollowup;
        String eventName;
        String dispatcherName;

        public OperationStatistic(DateTime start, DateTime end, String volunteer, String fina, String vehi, String particular, String organizer, String supervisor, String events, String dispatch)
        {
            startDate = start;
            endDate = end;
            volunteerFollowup = volunteer;
            finance = fina;
            vehicle = vehi;
            particularSituation = particular;
            organizationFollowup = organizer;
            supervisorFollowup = supervisor;
            eventName = events;
            dispatcherName = dispatch;
        }

        public DateTime getStartDate()
        {
            return startDate;
        }

        public DateTime getEndDate()
        {
            return endDate;
        }

        public String getVolunteerFollowup()
        {
            return volunteerFollowup;
        }

        public String getFinance()
        {
            return finance;
        }

        public String getVehicle()
        {
            return vehicle;
        }

        public String getParticularSituation()
        {
            return particularSituation;
        }

        public String getOrganizationFollowup()
        {
            return organizationFollowup;
        }

        public String getSupervisorFollowup()
        {
            return supervisorFollowup;
        }

        public String getEventName()
        {
            return eventName;
        }

        public String getDispatcherName()
        {
            return dispatcherName;
        }
    }
}
