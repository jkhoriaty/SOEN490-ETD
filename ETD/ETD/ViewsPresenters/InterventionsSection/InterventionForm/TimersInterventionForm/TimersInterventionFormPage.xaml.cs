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
		private Label interventionTimer;
		private Label interventionStatus;

		private List<Resource> resourceList = new List<Resource>();
		private Dictionary<Resource, Label> resourceTimerDictionary = new Dictionary<Resource, Label>();
		private Dictionary<Resource, Label> resourceStatusDictionary = new Dictionary<Resource, Label>();

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
			TimeSpan elapsed;
			bool ongoing = true;
			if(intervention.IsCompleted())
			{
				elapsed = intervention.getConclusionTime() - intervention.getTimeOfCall();
				ongoing = false;
			}
			else
			{
				elapsed = DateTime.Now - intervention.getTimeOfCall();
			}
			UpdateTimer(elapsed, interventionTimer, interventionStatus, ongoing, interventionDeadline);

			foreach (Resource resource in resourceList)
			{
				ongoing = true;
				if(resource.hasArrived())
				{
					elapsed = resource.getArrivalTime() - resource.getMovingTime();
					ongoing = false;
				}
				else
				{
					elapsed = DateTime.Now - resource.getMovingTime();
				}
				UpdateTimer(elapsed, resourceTimerDictionary[resource], resourceStatusDictionary[resource], ongoing, movingDeadline);
			}
		}

		//Changes the textbox
		private void UpdateTimer(TimeSpan elapsed, Label timer, Label status, bool ongoing, int deadline)
		{
			timer.Content = "";
			if(elapsed.Hours >= 1)
			{
				timer.Content += elapsed.Hours + ":";
				if(elapsed.Minutes < 10)
				{
					timer.Content += "0";
				}
			}
			timer.Content += elapsed.Minutes + ":";
			if (elapsed.Seconds < 10)
			{
				timer.Content += "0";
			}
			timer.Content += elapsed.Seconds.ToString();

			if (ongoing && elapsed.TotalMinutes > deadline)
			{
				setStatus(status, "Overtime");
				if (elapsed.Seconds < 15 && elapsed.Seconds % 2 == 0)
				{
					Brush backgroundColor = status.Background;
					status.Background = status.Foreground;
					status.Foreground = backgroundColor;
				}
			}
			else if(ongoing)
			{
				setStatus(status, "Ongoing");
			}
			else
			{
				setStatus(status, "Completed");
			}
		}

		//Create the intervention timer
		private void CreateInterventionTimer()
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

			Label timer = new Label();
			timer.FontWeight = FontWeights.Bold;
			timer.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(timer, 2);
			Grid.SetRow(timer, 0);
			timer.BorderBrush = new SolidColorBrush(Colors.Black);
			timer.BorderThickness = border;
			timersList.Children.Add(timer);
			interventionTimer = timer;


			Label status = new Label();
			status.FontWeight = FontWeights.Bold;
			status.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(status, 3);
			Grid.SetRow(status, 0);
			status.BorderBrush = new SolidColorBrush(Colors.Black);
			status.BorderThickness = border;
			timersList.Children.Add(status);
			interventionStatus = status;
		}

		private void CreateResourcesTimers()
		{
			int rowNumber = 1;
			foreach(Resource resource in intervention.getResourceList())
			{
				resourceList.Add(resource);

				RowDefinition rowDefinition = new RowDefinition();
				rowDefinition.Height = new GridLength(27);
				timersList.RowDefinitions.Add(rowDefinition);

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
				Grid.SetRow(timer, rowNumber);
				timersList.Children.Add(timer);
				resourceTimerDictionary.Add(resource, timer);

				Label status = new Label();
				status.HorizontalContentAlignment = HorizontalAlignment.Center;
				Grid.SetColumn(status, 3);
				Grid.SetRow(status, rowNumber);
				timersList.Children.Add(status);
				resourceStatusDictionary.Add(resource, status);

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
