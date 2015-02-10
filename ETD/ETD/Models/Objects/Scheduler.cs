using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    class Scheduler
    {
        private List<PatrolShift> ActivePatrols;
        private List<PatrolShift> InactivePatrols;

        public Scheduler()
        {
            ActivePatrols = new List<PatrolShift>();
            InactivePatrols = new List<PatrolShift>();
        }

        public void AddShift(string name, Team assignTo, int hour, int minutes)
        {
            ActivePatrols.Add(new PatrolShift(name, assignTo));
        }

        public void EndShift(string name, Team assignedTo, int hour, int minutes)
        {
            foreach(PatrolShift ps in ActivePatrols)
            {
                if((ps.GetSector().Equals(name)) && (ps.GetTeam().getName() == assignedTo.getName()) && (ps.GetStartTime().Hour == hour) && ps.GetStartTime().Minute == minutes)
                {
                    ps.finish();
                    ActivePatrols.Remove(ps);
                    InactivePatrols.Add(ps);
                }
            }
        }

        public List<String> Suggest(int amount)
        {
            List<String> suggestions = new List<string>();
            return suggestions;
        }
    }
}
