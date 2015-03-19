using ETD.CustomObjects.PopupForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	class Statistics
	{
		public TimeSpan averageResponseTime;
		public TimeSpan averageDuration;
		public int numberOfOngoingInterventions;
		public int numberOfCompletedInterventions;
		public int interventionsPerTeam;
		public int numberOfInterventionsPerClassification;

		public Statistics()
        {
			ETD.CustomObjects.PopupForms.Statistics statisticsPage = new ETD.CustomObjects.PopupForms.Statistics();
			setNumberOfInterventions();
			setAverageResponseTime();
			setAverageInterventionDuration();
			setInterventionsPerTeam();
			statisticsPage.Label_ongoingInterventions.Content = numberOfOngoingInterventions.ToString();
			statisticsPage.Label_completedInterventions.Content = numberOfCompletedInterventions.ToString();
			statisticsPage.Label_avgResponseTime.Content = averageResponseTime.ToString();
			statisticsPage.Label_avgDurationTime.Content = averageDuration.ToString();
			statisticsPage.Label_interventionsPerTeam.Content = interventionsPerTeam.ToString();
			FormPopup statisticsForm = new FormPopup(statisticsPage);
        }

		public void setNumberOfInterventions()
		{
			numberOfOngoingInterventions = Intervention.getActiveInterventionList().Count;
			numberOfCompletedInterventions = Intervention.getCompletedInterventionList().Count;
		}

		public void setAverageResponseTime()
		{
			List<TimeSpan> responseTimes = new List<TimeSpan>();
			foreach (Intervention i in Intervention.getActiveInterventionList())
			{
				if(i.getFirstTeamArrivalTime() != DateTime.MinValue)
				{
					responseTimes.Add(i.getFirstTeamArrivalTime() - i.getTimeOfCall());
				}
			}
			try
			{ 
				double doubleAverageTicks = responseTimes.Average(timeSpan => timeSpan.Ticks);
				long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

				this.averageResponseTime = new TimeSpan(longAverageTicks);
			}
			catch { }
		}

		public void setAverageInterventionDuration()
		{
			List<TimeSpan> durationTimes = new List<TimeSpan>();
			foreach (Intervention i in Intervention.getCompletedInterventionList())
			{
				durationTimes.Add(i.getConclusionTime() - i.getTimeOfCall());	
			}
			try
			{ 
				double doubleAverageTicks = durationTimes.Average(timeSpan => timeSpan.Ticks);
				long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

				this.averageDuration = new TimeSpan(longAverageTicks);
			}
			catch { }
		}

		public void setInterventionsPerTeam()
		{
			int total = 0;
			foreach(Team team in Team.getTeamList())
			{
				total += team.getInterventionCount();
			}
			total = total / Team.getTeamList().Count;
			interventionsPerTeam = total;
		}
	}
}
