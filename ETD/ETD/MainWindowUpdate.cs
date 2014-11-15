using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;

namespace ETD
{
	class MainWindowUpdate
	{
		private MainWindow caller;
		private static double teamSizeDifference = 0;

		public MainWindowUpdate(MainWindow caller)
		{
			this.caller = caller;
		}

		//Actual resizing of the team display section to handle resizing of window or different screen sizes
		public void setTeamsHeight()
		{
			Border TeamSection = caller.TeamSection;
			ScrollViewer Scroller = caller.Scroller;
			StackPanel TeamList = caller.TeamList;

			if (teamSizeDifference == 0)
			{
				teamSizeDifference = TeamSection.ActualHeight - TeamList.ActualHeight;
			}
			TeamSection.Height = caller.ActualHeight - 200;
			Scroller.MaxHeight = TeamSection.Height - teamSizeDifference;
		}

		//Called to show the form to create a new team
		public void DisplayCreateTeamForm()
		{
            Frame frame = new Frame();
            frame.Content = new TeamForm(caller);

            caller.TeamList.Children.Add(frame);
			caller.CreateTeamButton.IsEnabled = false;
		}

		//To be called after submit or cancel of the create team form
		public void HideCreateTeamForm()
		{
			caller.TeamList.Children.RemoveAt(caller.TeamList.Children.Count - 1);
			caller.CreateTeamButton.IsEnabled = true;
		}
		
		//Called to display a created team
		public void DisplayTeam() //TODO: Need to take as input or get the Team Name as well as the name and training level of all members
		{

			//TODO: Need to be moved to model in team creation
            int temp = Globals.currentIntervention;
            Stopwatch interventionTimer = new Stopwatch();
            interventionTimer.Start();

            Globals.timers.Add(temp, interventionTimer);

            Globals.currentIntervention += 1;
			//----------------------------------

			StackPanel TeamList = caller.TeamList;

			//
			// Creation of all the objects needed for the teams display
			//
			Border mainBorder = new Border();
			mainBorder.Name = "Bravo"; //TODO: To be changed to adapt to input
			mainBorder.BorderBrush = new SolidColorBrush(Colors.Black);
			mainBorder.BorderThickness = new System.Windows.Thickness(1);
			mainBorder.CornerRadius = new System.Windows.CornerRadius(5);
			Thickness topMargin = mainBorder.Margin;
			topMargin.Top = 5;
			mainBorder.Margin = topMargin;

				StackPanel mainStackPanel = new StackPanel();

					Grid teamNameGrid = new Grid();
					teamNameGrid.Background = new SolidColorBrush(Colors.Black);

						Label teamName = new Label();
						teamName.Content = "Bravo"; //TODO: To be changed to adapt to input
						teamName.HorizontalAlignment = HorizontalAlignment.Left;
						teamName.FontWeight = FontWeights.Bold;
						teamName.FontSize = 20;
						teamName.Foreground = new SolidColorBrush(Colors.White);

						//This will hold the training of the team as well as all the pieces of equipment that the team carries
						StackPanel equipmentStackPanel = new StackPanel();
						equipmentStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
						equipmentStackPanel.Orientation = Orientation.Horizontal;
						equipmentStackPanel.FlowDirection = FlowDirection.RightToLeft;

					//TODO: Add this to a loop to account for teams of different number of members
					Grid teamMember1 = new Grid();

						Label teamMember1Name = new Label();
						teamMember1Name.Content = "Tarzan"; //TODO: To be changed to adapt to input
						teamMember1Name.HorizontalAlignment = HorizontalAlignment.Left;
						teamMember1Name.FontSize = 18;

						//TODO: Need to add training level

					Grid teamMember2 = new Grid();

						Label teamMember2Name = new Label();
						teamMember2Name.Content = "Rambo"; //TODO: To be changed to adapt to input
						teamMember2Name.HorizontalAlignment = HorizontalAlignment.Left;
						teamMember2Name.FontSize = 18;

						//TODO: Need to add training level
						

			//
			// Linking items together to form appropriate hierarchy
			//
			TeamList.Children.Add(mainBorder);
				mainBorder.Child = mainStackPanel;
					mainStackPanel.Children.Add(teamNameGrid);
						teamNameGrid.Children.Add(teamName);
					mainStackPanel.Children.Add(teamMember1);
						teamMember1.Children.Add(teamMember1Name);
					mainStackPanel.Children.Add(teamMember2);
						teamMember2.Children.Add(teamMember2Name);
		}

        public void TimeTest_MenuItem_Click()
        {
            //MessageBox.Show(dbAccess.OpenConnection());
            //This Method currently returns the timer of the last queue'd timer.
            //Once the intervention creation interface is completed, it will be a trivial
            //matter to select which intervention from the dictionary you would like to retrieve the time from.
            //It is important to note, that in this current function, the timer keeps going even after you poll
            //it for its value. I left this in intentionally to prove that my function for retrieving didn't break
            //the stopwatch's functionality. This could be extremely useful if we wanted to keep a running display of
            //the time taken for each portion of each intervention.
            if (Globals.timers.ContainsKey(Globals.currentIntervention - 1))
            {
                TimeSpan elapsed = Globals.timers[Globals.currentIntervention - 1].Elapsed;
                TextBlock TimerText = caller.timer;
                TimerText.Text = elapsed.TotalSeconds.ToString();                               //Converts the total time on the stopwatch into an amount of seconds.

                Globals.interventionTime.Add(Globals.currentIntervention - 1, elapsed);     //Saves it to a second dictionary, this one only stores the elapsed times, this will be pushed to DB.
            }
        }
	}
}
