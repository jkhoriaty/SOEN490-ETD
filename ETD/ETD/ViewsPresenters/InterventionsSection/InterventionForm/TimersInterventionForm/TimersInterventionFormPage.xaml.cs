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
		private InterventionFormPage interventionForm;
		private DispatcherTimer dispatcherTimer = new DispatcherTimer();
		private int timerPosition = 1;
		private Dictionary<String, Label> timerLabels = new Dictionary<String, Label>();
		private Dictionary<String, Stopwatch> stopwatches = new Dictionary<String, Stopwatch>();
		private Dictionary<String, Label> statusLabels = new Dictionary<String, Label>();
		private int deadline = 15;

		public TimersInterventionFormPage(InterventionFormPage interventionForm)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;

			dispatcherTimer.Tick += new EventHandler(refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second
			dispatcherTimer.Start();

			createOverallTimer();
		}

		public void refresh(object sender, EventArgs e)
		{
			foreach(KeyValuePair<String, Stopwatch> stopwatch in stopwatches)
			{
				if (statusLabels[stopwatch.Key].Content.Equals("Ongoing") || statusLabels[stopwatch.Key].Content.Equals("Overtime"))
				{
					TimeSpan elapsed = stopwatch.Value.Elapsed;
					timerLabels[stopwatch.Key].Content = elapsed.Minutes + ":";
					if(elapsed.Seconds < 10)
					{
						timerLabels[stopwatch.Key].Content += "0";
					}
					timerLabels[stopwatch.Key].Content += "" + elapsed.Seconds;
					if(statusLabels[stopwatch.Key].Content.Equals("Ongoing") && deadline <= elapsed.Minutes)
					{
						setStatus(stopwatch.Key, "Overtime");
					}
				}
			}
		}

		public void createOverallTimer()
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
			timerLabels.Add("Intervention", timer);

			Label status = new Label();
			status.FontWeight = FontWeights.Bold;
			status.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(status, 3);
			Grid.SetRow(status, 0);
			status.BorderBrush = new SolidColorBrush(Colors.Black);
			status.BorderThickness = border;
			timersList.Children.Add(status);
			statusLabels.Add("Intervention", status);
			setStatus("Intervention", "Ongoing");

			Stopwatch stopwatch = new Stopwatch();
			stopwatches.Add("Intervention", stopwatch);
			stopwatch.Start();
		}

		public void setStatus(String timer, String status)
		{
			Label statusLabel = statusLabels[timer];
			statusLabel.Content = status;
			if(status.Equals("Ongoing"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Yellow);
			}
			else if(status.Equals("Completed"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Green);
				statusLabel.Foreground = new SolidColorBrush(Colors.Black);
				stopwatches[timer].Stop();
			}
			else if(status.Equals("Overtime"))
			{
				statusLabel.Background = new SolidColorBrush(Colors.Red);
				statusLabel.Foreground = new SolidColorBrush(Colors.White);
			}
		}

		public void CreateTimer(String team, String resource)
		{
			Label teamLabel = new Label();
			teamLabel.Content = team;
			teamLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(teamLabel, 0);
			Grid.SetRow(teamLabel, timerPosition);
			timersList.Children.Add(teamLabel);

			Label name = new Label();
			name.Content = resource;
			Grid.SetColumn(name, 1);
			Grid.SetRow(name, timerPosition);
			timersList.Children.Add(name);

			Label timer = new Label();
			timer.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(timer, 2);
			Grid.SetRow(timer, timerPosition);
			timersList.Children.Add(timer);
			timerLabels.Add(resource, timer);

			Label status = new Label();
			status.HorizontalContentAlignment = HorizontalAlignment.Center;
			Grid.SetColumn(status, 3);
			Grid.SetRow(status, timerPosition);
			timersList.Children.Add(status);
			statusLabels.Add(resource, status);
			setStatus(resource, "Ongoing");

			Stopwatch stopwatch = new Stopwatch();
			stopwatches.Add(resource, stopwatch);
			stopwatch.Start();

			timerPosition++;
		}
	}
}
