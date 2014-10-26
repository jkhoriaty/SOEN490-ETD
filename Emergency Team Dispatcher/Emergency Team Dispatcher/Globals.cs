using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Emergency_Team_Dispatcher
{
    public class Globals
    {
        
            public static Dictionary<int, Stopwatch> timers = new Dictionary<int, Stopwatch>();
            public static Dictionary<int, TimeSpan> interventionTime = new Dictionary<int, TimeSpan>();
            public static Dictionary<int, Team> listOfTeams = new Dictionary<int, Team>();
            public static int currentIntervention = 0;
            public static int currentTeam = 0;
    }
}
