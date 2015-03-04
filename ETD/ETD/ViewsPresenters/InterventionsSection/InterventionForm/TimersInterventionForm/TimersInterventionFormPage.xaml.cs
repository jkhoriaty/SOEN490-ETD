using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm;
using System.Windows.Threading;
using System.Diagnostics;
using ETD.Models.Objects;

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.TimersInterventionForm
{
	/// <summary>
	/// Interaction logic for TimersInterventionFormPage.xaml
	/// </summary>
	public partial class TimersInterventionFormPage : Page
	{
        private InterventionFormPage interventionForm;
        private Intervention intervention;

        private List<Stopwatch> stopwatchList = new List<Stopwatch>();
		private Dictionary<Stopwatch, bool> stopwatchActive = new Dictionary<Stopwatch, bool>();
		private Dictionary<Stopwatch, Label> stopwatchLabelDictionary = new Dictionary<Stopwatch, Label>();
		private Dictionary<Stopwatch, TimeSpan> stopwatchOffsetDictionary = new Dictionary<Stopwatch, TimeSpan>();
		private Dictionary<Stopwatch, Label> stopwatchStatusDictionary = new Dictionary<Stopwatch, Label>();

		private static int interventionDeadline = 30;
		private static int movingDeadline = 5;

		public TimersInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
            this.intervention = intervention;

            CreateInterventionTimer();
			CreateResourcesTimers();

			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second
			dispatcherTimer.Start();
		}

		//Method runs every second
		public void refresh(object sender, EventArgs e)
		{
			foreach (Stopwatch stopwatch in stopwatchList)
			{
				if(stopwatchActive[stopwatch])
				{
					TimeSpan elapsed = stopwatch.Elapsed + stopwatchOffsetDictionary[stopwatch];
					stopwatchLabelDictionary[stopwatch].Content = elapsed.Minutes + ":";
					if(elapsed.Seconds < 10)
					{
						stopwatchLabelDictionary[stopwatch].Content += "0";
					}
					stopwatchLabelDictionary[stopwatch].Content += elapsed.Seconds.ToString();

					//Setting the status
					if(stopwatchList.ElementAt(0) == stopwatch) //It's the intervention timer
					{
						if(elapsed.Minutes < interventionDeadline) //Is overtime
						{
							setStatus(stopwatchStatusDictionary[stopwatch], "Overtime");
							if(elapsed.Seconds < 15)
							{
								if(elapsed.Seconds % 2 == 0)
								{
									Brush backgroundColor = stopwatchStatusDictionary[stopwatch].Background;
									stopwatchStatusDictionary[stopwatch].Background = stopwatchStatusDictionary[stopwatch].Foreground;
									stopwatchStatusDictionary[stopwatch].Foreground = backgroundColor;
								}
							}
						}
						else
						{
							setStatus(stopwatchStatusDictionary[stopwatch], "Ongoing");
						}
					}
					else if(elapsed.Minutes < movingDeadline) //Is a resource time and is overtime
					{
						setStatus(stopwatchStatusDictionary[stopwatch], "Overtime");
					}
					else
					{
						setStatus(stopwatchStatusDictionary[stopwatch], "Ongoing");
					}
				}
				else
				{
					setStatus(stopwatchStatusDictionary[stopwatch], "Completed");
				}
			}
		}

		//Create the intervention timer
		private void CreateInterventionTimer()
		{
			Thickness border = new Thickness();
			border.Bottom = 1;

            Stopwatch stopwatch = new Stopwatch();
			stopwatchList.Add(stopwatch);
			stopwatch.Start();

			TimeSpan offset;
			if (intervention.isConcluded() == true)
			{
				stopwatchActive.Add(stopwatch, false);
				offset = intervention.getConclusionTime() - intervention.getTimeOfCall();
			}
			else
			{
				stopwatchActive.Add(stopwatch, true);
				offset = DateTime.Now - intervention.getTimeOfCall();
			}
			stopwatchOffsetDictionary.Add(stopwatch, offset);

			Label name = new Label();
			name.Content = "Intervention";
			name.FontWeight = FontWeights.Bold;
			Grid.SetColumn(name, 0);
			Grid.SetColumnSpan(name, 2);
			Grid.SetRow(name, 0);
			name.BorderBrush = new SolidColorBrush(Colors.Black);
			name.BorderThickness = border;
			timersList.Children.Add(name);

			Label timer = new Label();
			timer.FontWeight = FontWeights.Bold;
			timer.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(timer, 2);
			Grid.SetRow(timer, 0);
			timer.BorderBrush = new SolidColorBrush(Colors.Black);
			timer.BorderThickness = border;
			timersList.Children.Add(timer);
			stopwatchLabelDictionary.Add(stopwatch, timer);


			Label status = new Label();
			status.FontWeight = FontWeights.Bold;
			status.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(status, 3);
			Grid.SetRow(status, 0);
			status.BorderBrush = new SolidColorBrush(Colors.Black);
			status.BorderThickness = border;
			timersList.Children.Add(status);
			stopwatchStatusDictionary.Add(stopwatch, status);
		}

		private void CreateResourcesTimers()
		{
			int rowNumber = 1;
			foreach(Resource resource in intervention.getResourceList())
			{
				RowDefinition rowDefinition = new RowDefinition();
				rowDefinition.Height = new GridLength(25);
				timersList.RowDefinitions.Add(rowDefinition);

				Stopwatch stopwatch = new Stopwatch();
				stopwatchList.Add(stopwatch);
				stopwatch.Start();

				TimeSpan offset;
				if(resource.hasArrived() == true)
				{
					stopwatchActive.Add(stopwatch, false);
					offset = resource.getArrivalTime() - resource.getMovingTime();
				}
				else
				{
					stopwatchActive.Add(stopwatch, true);
					offset = DateTime.Now - intervention.getTimeOfCall();
				}
				stopwatchOffsetDictionary.Add(stopwatch, offset);

				Label name = new Label();
				name.Content = resource.getTeam().getName();
				Grid.SetColumn(name, 0);
				Grid.SetRow(name, rowNumber);
				timersList.Children.Add(name);

				Label resourceName = new Label();
				resourceName.Content = resource.getResourceName();
				Grid.SetColumn(resourceName, 1);
				Grid.SetRow(resourceName, rowNumber);
				timersList.Children.Add(resourceName);

				Label timer = new Label();
				timer.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(timer, 2);
				Grid.SetRow(timer, 0);
				timersList.Children.Add(timer);
				stopwatchLabelDictionary.Add(stopwatch, timer);

				Label status = new Label();
				status.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(status, 3);
				Grid.SetRow(status, rowNumber);
				timersList.Children.Add(status);
				stopwatchStatusDictionary.Add(stopwatch, status);

				rowNumber++;
			}
		}

		public void setStatus(Label statusLabel, String status)
		{
			statusLabel.Content = status;
			if(status.Equals("Ongoing"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Yellow);
				statusLabel.Foreground = new SolidColorBrush(Colors.Black);
			}
			else if(status.Equals("Completed"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Green);
				statusLabel.Foreground = new SolidColorBrush(Colors.White);
			}
			else if(status.Equals("Overtime"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Red);
				statusLabel.Foreground = new SolidColorBrush(Colors.White);
			}
		}

		internal static void setInterventionDeadline(int deadline)
		{
			interventionDeadline = deadline;
		}

		internal static void setMovingDeadline(int deadline)
		{
			movingDeadline = deadline;
		}
	}
}
