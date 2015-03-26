using ETD.CustomObjects.PopupForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ETD.Models.Objects
{
	class Statistics
	{
		ETD.CustomObjects.PopupForms.Statistics statisticsPage;
		public TimeSpan averageResponseTime;
		public TimeSpan averageDuration;
		public int numberOfOngoingInterventions;
		public int numberOfCompletedInterventions;

		Dictionary<string, int> numberOfInterventionsPerClassification = new Dictionary<string, int>();


		public Statistics()
        {
			statisticsPage = new ETD.CustomObjects.PopupForms.Statistics();
			setNumberOfInterventions();
			setAverageResponseTime();
			setAverageInterventionDuration();
			setInterventionsPerTeam();
			setNumberOfInterventionsPerClassification();
			statisticsPage.Label_ongoingInterventions.Content = numberOfOngoingInterventions.ToString();
			statisticsPage.Label_completedInterventions.Content = numberOfCompletedInterventions.ToString();
			statisticsPage.Label_avgResponseTime.Content = averageResponseTime.ToString().Substring(0,8);
			statisticsPage.Label_avgDurationTime.Content = averageDuration.ToString().Substring(0, 8);
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
				durationTimes.Add(i.getConclusionTime() - i.getFirstTeamArrivalTime());	
			}
			try
			{ 
				double doubleAverageTicks = durationTimes.Average(timeSpan => timeSpan.Ticks);
				long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

				this.averageDuration = new TimeSpan(longAverageTicks);
			}
			catch { }
		}

		public void setNumberOfInterventionsPerClassification()
		{
			foreach (Intervention i in Intervention.getActiveInterventionList())
			{
				if(i.getChiefComplaint() != null)
				{ 
					if (numberOfInterventionsPerClassification.ContainsKey(i.getChiefComplaint()))
					{
						numberOfInterventionsPerClassification[i.getChiefComplaint()]++;
					}
					else
					{
						numberOfInterventionsPerClassification.Add(i.getChiefComplaint(), 1);
					}
				}
			}
			try
			{
				foreach (KeyValuePair<string, int> pair in numberOfInterventionsPerClassification)
				{
					Label lbl = new Label();
					lbl.Content = pair.Key + ": ";
					statisticsPage.LabelStackPanel.Children.Add(lbl);

					Label lblValue = new Label();
					lblValue.Content = pair.Value;
					statisticsPage.InfoStackPanel.Children.Add(lblValue);
				}
			}
			catch { }
		}

		public void setInterventionsPerTeam()
		{
			foreach (Intervention i in Intervention.getActiveInterventionList())
			{
				foreach(Team team in i.getInterveningTeamList())
				{
					if(i.getCode() == 1)
					{
						team.IncrementCode1();
					}
					if(i.getCode() == 2)
					{
						team.IncrementCode2();
					}
				}
			}

			foreach (Intervention i in Intervention.getCompletedInterventionList())
			{
				foreach (Team team in i.getInterveningTeamList())
				{
					if (i.getCode() == 1)
					{
						team.IncrementCode1();
					}
					if (i.getCode() == 2)
					{
						team.IncrementCode2();
					}
				}
			}
			
			foreach(Team team in Team.getTeamList())
			{
				if(team.getCode1Count() != 0)
				{ 
					Label lblCode1 = new Label();
					lblCode1.Content = "Team " + team.getName() + " Code 1: ";
					statisticsPage.LabelStackPanel.Children.Add(lblCode1);

					Label infoLbl1 = new Label();
					infoLbl1.Content = team.getCode1Count();
					statisticsPage.InfoStackPanel.Children.Add(infoLbl1);
				}
				if(team.getCode2Count() != 0)
				{ 
					Label lblCode2 = new Label();
					lblCode2.Content = "Team " + team.getName() + " Code 2: ";
					statisticsPage.LabelStackPanel.Children.Add(lblCode2);

					Label infoLbl2 = new Label();
					infoLbl2.Content = team.getCode2Count();
					statisticsPage.InfoStackPanel.Children.Add(infoLbl2);
				}
				team.ResetCodeCount();
			}
		}
	}
}
