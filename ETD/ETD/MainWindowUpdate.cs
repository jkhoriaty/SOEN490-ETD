using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Threading;

namespace ETD
{
	class MainWindowUpdate
	{
		private static double teamSizeDifference = 0;
		private MainWindow caller;

		public MainWindowUpdate(MainWindow caller)
		{
			this.caller = caller;
		}

		//Actual resizing of the team display section to handle resizing of window or different screen sizes
		public void setTeamsHeight()
		{
			System.Windows.Controls.Border TeamSection = caller.getTeamSection();
			System.Windows.Controls.ScrollViewer Scroller = caller.getScroller();
			System.Windows.Controls.StackPanel TeamList = caller.getTeamList();

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
			System.Windows.Controls.StackPanel TeamList = caller.getTeamList();

			System.Windows.Controls.Border mainBorder = new System.Windows.Controls.Border();
			mainBorder.Name = "newTeam";
			mainBorder.BorderBrush = new SolidColorBrush(Colors.Black); ;
			mainBorder.BorderThickness = new System.Windows.Thickness(1);
			mainBorder.CornerRadius = new System.Windows.CornerRadius(5);
			Thickness topMargin = mainBorder.Margin;
			topMargin.Top = 5;
			mainBorder.Margin = topMargin;

				System.Windows.Controls.StackPanel mainStackPanel = new System.Windows.Controls.StackPanel();
				mainBorder.Name = "newTeamList";

					System.Windows.Controls.TextBox teamName = new System.Windows.Controls.TextBox();
					teamName.Name = "teamName";
					teamName.Text = "Team Name";
					teamName.FontWeight = FontWeights.Bold;
					teamName.FontSize = 20;

					System.Windows.Controls.Border line1 = createLine();

					//TODO: Move to add team member and have this method call the add teamMember method
					System.Windows.Controls.TextBox teamMember = new System.Windows.Controls.TextBox();
					teamMember.Name = "teamMember1";
					teamMember.Text = "Team Member Name";
					teamMember.FontSize = 18;

					System.Windows.Controls.Border line2 = createLine();
					
					System.Windows.Controls.Grid timeGrid = new System.Windows.Controls.Grid();

						System.Windows.Controls.Label timeText = new System.Windows.Controls.Label();
						timeText.Content = "Time of departure:";
						timeText.HorizontalAlignment = HorizontalAlignment.Left;

			//
			// Linking items together to form appropriate hierarchy
			//
			TeamList.Children.Add(mainBorder);
				mainBorder.Child = mainStackPanel;
					mainStackPanel.Children.Add(teamName);
					mainStackPanel.Children.Add(line1);
					mainStackPanel.Children.Add(teamMember);
					mainStackPanel.Children.Add(line2);
					mainStackPanel.Children.Add(timeGrid);
						timeGrid.Children.Add(timeText);
		}

		private System.Windows.Controls.Border createLine()
		{
			System.Windows.Controls.Border line = new System.Windows.Controls.Border();
			line.BorderBrush = new SolidColorBrush(Colors.Black); ;
			line.BorderThickness = new System.Windows.Thickness(1);
			Thickness topMargin = line.Margin;
			topMargin.Top = 2;
			topMargin.Bottom = 2;
			line.Margin = topMargin;
			return line;
		}
		
		//Called to display a created team
		public void DisplayTeam()
		{
            int temp = Globals.currentIntervention;
            Stopwatch interventionTimer = new Stopwatch();
            interventionTimer.Start();

            Globals.timers.Add(temp, interventionTimer);

            Globals.currentIntervention += 1;

			System.Windows.Controls.StackPanel TeamList = caller.getTeamList();

			//
			// Creation of all the objects needed for the teams display
			//
			System.Windows.Controls.Border mainBorder = new System.Windows.Controls.Border();
			mainBorder.Name = "Bravo"; //TODO: To be changed to adapt to input
			mainBorder.BorderBrush = new SolidColorBrush(Colors.Black); ;
			mainBorder.BorderThickness = new System.Windows.Thickness(1);
			mainBorder.CornerRadius = new System.Windows.CornerRadius(5);
			Thickness topMargin = mainBorder.Margin;
			topMargin.Top = 5;
			mainBorder.Margin = topMargin;

				System.Windows.Controls.StackPanel mainStackPanel = new System.Windows.Controls.StackPanel();

					System.Windows.Controls.Grid teamNameGrid = new System.Windows.Controls.Grid();
					teamNameGrid.Background = new SolidColorBrush(Colors.Black);

						System.Windows.Controls.Label teamName = new System.Windows.Controls.Label();
						teamName.Content = "Bravo"; //TODO: To be changed to adapt to input
						teamName.HorizontalAlignment = HorizontalAlignment.Left;
						teamName.FontWeight = FontWeights.Bold;
						teamName.FontSize = 20;
						teamName.Foreground = new SolidColorBrush(Colors.White);

						//This will hold the training of the team as well as all the pieces of equipment that the team carries
						System.Windows.Controls.StackPanel equipmentStackPanel = new System.Windows.Controls.StackPanel();
						equipmentStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
						equipmentStackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
						equipmentStackPanel.FlowDirection = FlowDirection.RightToLeft;

					//TODO: Add this to a loop to account for teams of different number of members
					System.Windows.Controls.Grid teamMember1 = new System.Windows.Controls.Grid();

						System.Windows.Controls.Label teamMember1Name = new System.Windows.Controls.Label();
						teamMember1Name.Content = "Tarzan"; //TODO: To be changed to adapt to input
						teamMember1Name.HorizontalAlignment = HorizontalAlignment.Left;
						teamMember1Name.FontSize = 18;

						//TODO: Need to add training level

					System.Windows.Controls.Grid teamMember2 = new System.Windows.Controls.Grid();

						System.Windows.Controls.Label teamMember2Name = new System.Windows.Controls.Label();
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
                System.Windows.Controls.TextBlock TimerText = caller.getTimer();
                TimerText.Text = elapsed.TotalSeconds.ToString();                               //Converts the total time on the stopwatch into an amount of seconds.

                Globals.interventionTime.Add(Globals.currentIntervention - 1, elapsed);     //Saves it to a second dictionary, this one only stores the elapsed times, this will be pushed to DB.
            }
        }
	}
}
