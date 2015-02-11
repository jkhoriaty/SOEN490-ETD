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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.TimersInterventionForm
{
	/// <summary>
	/// Interaction logic for TimersInterventionFormPage.xaml
	/// </summary>
	public partial class TimersInterventionFormPage : Page
	{
        private const int TIMER_LIMIT = 13;
        private InterventionFormPage interventionForm;
		private int timerPosition = 1;


        private Label[] teamLabels = new Label[TIMER_LIMIT];
        private Label[] resourceLabels = new Label[TIMER_LIMIT];
        private Label[] timerLabels = new Label[TIMER_LIMIT];
        private Label[] statusLabels = new Label[TIMER_LIMIT];

        private Stopwatch[] stopwatches = new Stopwatch[TIMER_LIMIT];
        private int[] offsets = new int[TIMER_LIMIT];
        private int[] endOffsets = new int[TIMER_LIMIT];

		private static int interventionDeadline = 30;
		private static int movingDeadline = 5;

		public TimersInterventionFormPage(InterventionFormPage interventionForm)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;

			for (int i = 0; i < endOffsets.Length; i++)
			{
				endOffsets[i] = -1;
			}

			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second
			dispatcherTimer.Start();

			CreateOverallTimer(0);
		}

		//Method ran every second
		public void refresh(object sender, EventArgs e)
		{
			for (int i = 0; i < TIMER_LIMIT; i++)
			{
				if (stopwatches[i] != null && (statusLabels[i].Content.Equals("Ongoing") || statusLabels[i].Content.Equals("Overtime")))
				{
					if(endOffsets[i] != -1)
					{
						setStatus(i, "Completed");
					}
					//Update all timers
					TimeSpan elapsed = stopwatches[i].Elapsed;
					timerLabels[i].Content = (elapsed.Minutes + offsets[i]/60) + ":";
                    if (elapsed.Seconds + offsets[i] % 60 < 10)
					{
						timerLabels[i].Content += "0";
					}
                    timerLabels[i].Content += "" + (elapsed.Seconds + offsets[i] % 60);

					//Setting status to overtime of it surpasses the deadline
                    if (statusLabels[i].Content.Equals("Ongoing") && ((i == 0 && (interventionDeadline <= (elapsed.Minutes + offsets[i] / 60))) || (i != 0 && (movingDeadline <= (elapsed.Minutes + offsets[i] / 60)))))
					{
						setStatus(i, "Overtime");
					}

					//Flash 15 seconds every minute if overtime
					if (statusLabels[i].Content.Equals("Overtime"))
					{
						if (elapsed.Seconds <= 15)
						{
							Brush backgroundColor = statusLabels[i].Background;
							statusLabels[i].Background = statusLabels[i].Foreground;
							statusLabels[i].Foreground = backgroundColor;
						}
						else
						{
							setStatus(i, "Overtime");
						}
					}
				}
			}
		}

		//Create the intervention timer
		public void CreateOverallTimer(int offset)
		{
			if (stopwatches[0] != null)
			{
				offsets[0] = offset;
				stopwatches[0].Restart();
			}
			else
			{
				Thickness border = new Thickness();
				border.Bottom = 1;

				Label name = new Label();
				name.Content = "Intervention";
				name.FontWeight = FontWeights.Bold;
				Grid.SetColumn(name, 0);
				Grid.SetColumnSpan(name, 2);
				Grid.SetRow(name, 0);
				name.BorderBrush = new SolidColorBrush(Colors.Black);
				name.BorderThickness = border;

				timersList.Children.Add(name);
				resourceLabels[0] = name;

				Label timer = new Label();
				timer.FontWeight = FontWeights.Bold;
				timer.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(timer, 2);
				Grid.SetRow(timer, 0);
				timer.BorderBrush = new SolidColorBrush(Colors.Black);
				timer.BorderThickness = border;

				timersList.Children.Add(timer);
				timerLabels[0] = timer;

				Label status = new Label();
				status.FontWeight = FontWeights.Bold;
				status.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(status, 3);
				Grid.SetRow(status, 0);
				status.BorderBrush = new SolidColorBrush(Colors.Black);
				status.BorderThickness = border;

				timersList.Children.Add(status);
				statusLabels[0] = status;
				setStatus(0, "Ongoing");

				Stopwatch stopwatch = new Stopwatch();
				stopwatches[0] = stopwatch;
				stopwatch.Start();
			}
		}

		public void CreateTimer(int position, String team, String resource, int offset)
		{
			if(stopwatches[position] != null)
			{
				teamLabels[position].Content = team;
				resourceLabels[position].Content = resource;

				offsets[position] = offset;
				if(offset != 0)
				{
					stopwatches[position].Restart();
				}
			}
			else
			{
				Label teamLabel = new Label();
				teamLabel.Content = team;
				teamLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(teamLabel, 0);
				Grid.SetRow(teamLabel, timerPosition);

				timersList.Children.Add(teamLabel);
				teamLabels[position] = teamLabel;

				Label resourceLabel = new Label();
				resourceLabel.Content = resource;
				Grid.SetColumn(resourceLabel, 1);
				Grid.SetRow(resourceLabel, timerPosition);

				timersList.Children.Add(resourceLabel);
				resourceLabels[position] = resourceLabel;

				Label timerLabel = new Label();
				timerLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(timerLabel, 2);
				Grid.SetRow(timerLabel, timerPosition);

				timersList.Children.Add(timerLabel);
				timerLabels[position] = timerLabel;

				Label statusLabel = new Label();
				statusLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(statusLabel, 3);
				Grid.SetRow(statusLabel, timerPosition);

				timersList.Children.Add(statusLabel);
				statusLabels[position] = statusLabel;
				setStatus(position, "Ongoing");

				offsets[position] = offset;

				Stopwatch stopwatch = new Stopwatch();
				stopwatches[position] = stopwatch;
				stopwatch.Start();

				timerPosition++;
			}
			
		}

        //TODO: Make CloneTimer Method
        public void CloneTimer(int position, String team, String resource, int offset, int original)
        {
            if (stopwatches[original] != null)
            {
                if (stopwatches[position] != null)
                {
                    teamLabels[position].Content = team;
                    resourceLabels[position].Content = resource;

                    offsets[position] = offset + (int)(stopwatches[original].ElapsedMilliseconds/1000);
                    stopwatches[position].Restart();
                }
                else
                {
                    Label teamLabel = new Label();
                    teamLabel.Content = team;
                    teamLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(teamLabel, 0);
                    Grid.SetRow(teamLabel, timerPosition);

                    timersList.Children.Add(teamLabel);
                    teamLabels[position] = teamLabel;

                    Label resourceLabel = new Label();
                    resourceLabel.Content = resource;
                    Grid.SetColumn(resourceLabel, 1);
                    Grid.SetRow(resourceLabel, timerPosition);

                    timersList.Children.Add(resourceLabel);
                    resourceLabels[position] = resourceLabel;

                    Label timerLabel = new Label();
                    timerLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(timerLabel, 2);
                    Grid.SetRow(timerLabel, timerPosition);

                    timersList.Children.Add(timerLabel);
                    timerLabels[position] = timerLabel;

                    Label statusLabel = new Label();
                    statusLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(statusLabel, 3);
                    Grid.SetRow(statusLabel, timerPosition);

                    timersList.Children.Add(statusLabel);
                    statusLabels[position] = statusLabel;
                    setStatus(position, "Ongoing");

                    offsets[position] = offset + (int)(stopwatches[original].ElapsedMilliseconds/1000);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatches[position] = stopwatch;
                    stopwatch.Start();

                    timerPosition++;
                }
            }
        }

		public void setStatus(int position, String status)
		{
			Label statusLabel = statusLabels[position];
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
				stopwatches[position].Stop();
			}
			else if(status.Equals("Overtime"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Red);
				statusLabel.Foreground = new SolidColorBrush(Colors.White);
			}
		}

		public bool StopOverallTimer(int offset)
		{
			int timeFromStart = stopwatches[0].Elapsed.Minutes + offsets[0];
			if (timeFromStart >= offset)
			{
				endOffsets[0] = offset;
				return true;
			}
			else
			{
				MessageBox.Show("The intervention end time is before the start time");
				return false;
			}
		}

		public void StopTimer(int position, int offset)
		{
			int timeFromStart = stopwatches[position].Elapsed.Minutes + offsets[position];
			if(timeFromStart >= offset)
			{
				endOffsets[position] = offset;
			}
			else
			{
				MessageBox.Show("The end time is before the start time");
			}
		}

        public bool IsRunning(int position)
        {
            if (stopwatches[position] != null)
                return stopwatches[position].IsRunning;
            else
                return false;
        }

        public void RenameTimer(int position, String team, String resource)
        {
            if (stopwatches[position] != null)
            {
                teamLabels[position].Content = team;
                resourceLabels[position].Content = resource;
            }
        }

		public static void setInterventionDeadline(int deadline)
		{
			interventionDeadline = deadline;
		}

		public static void setMovingDeadline(int deadline)
		{
			movingDeadline = deadline;
		}
	}
}
