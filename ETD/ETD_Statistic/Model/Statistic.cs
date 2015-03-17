using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD_Statistic.Model
{
    class Statistic
    {
        DateTime startTime;
        DateTime endTime;
        int numberOfInterventions;
        String teamManager;
        String weatherCondition;
        String client;
        String volunteer;
        int expenses;
        String situations;

        public Statistic(DateTime start, DateTime end)
        {
            startTime = start;
            endTime = end;
            numberOfInterventions = 0;
            teamManager = null;
            weatherCondition = null;
            client = null;
            volunteer = null;
            expenses = 0;
            situations = null;
        }

        public void getInterventionCountChildren(String type)
        {
            
        }

        public void getInterventionCountAdult(String type)
        {

        }

        public void setStartTime()
        {
        }

        public void setEndTime()
        { 
        }


    }
}
