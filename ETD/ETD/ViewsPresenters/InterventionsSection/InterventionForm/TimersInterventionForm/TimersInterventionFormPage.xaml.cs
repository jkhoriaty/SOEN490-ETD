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
		private Label timer911;
		private Label status911;
		private Label timerAmbulance;
		private Label statusAmbulance;
		private Label timerFR;
		private Label statusFR;

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
			dispatcherTimer.Tick += new EventHandler(Refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second
			dispatcherTimer.Start();
		}

		//Method runs every second
		public void Refresh(object sender, EventArgs e)
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

			if (intervention.getFirstResponderArrivalTime() != DateTime.MinValue || intervention.getAmbulanceArrivalTime() != DateTime.MinValue)
			{
				if(intervention.getFirstResponderArrivalTime() != DateTime.MinValue)
				{
					ongoing = false;
					elapsed = intervention.getFirstResponderArrivalTime() - intervention.getCall911Time();
					UpdateTimer(elapsed, timerFR, statusFR, ongoing, interventionDeadline);
				}
				else
				{
					ongoing = true;
					elapsed = DateTime.Now - intervention.getCall911Time();
					UpdateTimer(elapsed, timerFR, statusFR, ongoing, interventionDeadline);
				}
				if(intervention.getAmbulanceArrivalTime() != DateTime.MinValue)
				{
					ongoing = false;
					elapsed = intervention.getAmbulanceArrivalTime() - intervention.getCall911Time();
					UpdateTimer(elapsed, timerAmbulance, statusAmbulance, ongoing, interventionDeadline);
				}
				else
				{
					ongoing = true;
					elapsed = DateTime.Now - intervention.getCall911Time();
					UpdateTimer(elapsed, timerAmbulance, statusAmbulance, ongoing, interventionDeadline);
				}
			}
			else
			{
				if (intervention.getCall911Time() != DateTime.MinValue)
				{
					ongoing = true;
					elapsed = DateTime.Now - intervention.getCall911Time();
				}
				UpdateTimer(elapsed, timer911, status911, ongoing, interventionDeadline);
			}
			
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
			try 
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
			catch
			{

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
			if (intervention.getCall911Time() != DateTime.MinValue) //if 911 time is set
			{
				if(intervention.getFirstResponderArrivalTime() != DateTime.MinValue || intervention.getAmbulanceArrivalTime() != DateTime.MinValue)
				{
					RowDefinition rowDefFR = new RowDefinition();
					rowDefFR.Height = new GridLength(27);
					timersList.RowDefinitions.Add(rowDefFR);

					Label nameFR = new Label();
					nameFR.Content = "FR";
					Grid.SetColumn(nameFR, 0);
					Grid.SetRow(nameFR, rowNumber);
					timersList.Children.Add(nameFR);

					timerFR = new Label();
					//timerFR.Content = (intervention.getFirstResponderArrivalTime() - intervention.getCall911Time());
					timerFR.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(timerFR, 2);
					Grid.SetRow(timerFR, rowNumber);
					timersList.Children.Add(timerFR);

					statusFR = new Label();
					statusFR.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(statusFR, 3);
					Grid.SetRow(statusFR, rowNumber);
					timersList.Children.Add(statusFR);

					rowNumber++;

					RowDefinition rowDefAmb = new RowDefinition();
					rowDefAmb.Height = new GridLength(27);
					timersList.RowDefinitions.Add(rowDefAmb);

					Label nameAmb = new Label();
					nameAmb.Content = "Ambulance";
					Grid.SetColumn(nameAmb, 0);
					Grid.SetRow(nameAmb, rowNumber);
					timersList.Children.Add(nameAmb);

					timerAmbulance = new Label();
					//timerFR.Content = (intervention.getFirstResponderArrivalTime() - intervention.getCall911Time());
					timerAmbulance.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(timerAmbulance, 2);
					Grid.SetRow(timerAmbulance, rowNumber);
					timersList.Children.Add(timerAmbulance);

					statusAmbulance = new Label();
					statusAmbulance.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(statusAmbulance, 3);
					Grid.SetRow(statusAmbulance, rowNumber);
					timersList.Children.Add(statusAmbulance);
								
				}
				else 
				{
					RowDefinition rowDef911 = new RowDefinition();
					rowDef911.Height = new GridLength(27);
					timersList.RowDefinitions.Add(rowDef911);

					Label name911 = new Label();
					name911.Content = "911 Call";
					Grid.SetColumn(name911, 0);
					Grid.SetRow(name911, rowNumber);
					timersList.Children.Add(name911);

					timer911 = new Label();
					//timer911.Content = (DateTime.Now - intervention.getCall911Time()).Seconds;
					timer911.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(timer911, 2);
					Grid.SetRow(timer911, rowNumber);
					timersList.Children.Add(timer911);

					status911 = new Label();
					status911.HorizontalContentAlignment = HorizontalAlignment.Center;
					Grid.SetColumn(status911, 3);
					Grid.SetRow(status911, rowNumber);
					timersList.Children.Add(status911);
				}
			}

			rowNumber++;
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
