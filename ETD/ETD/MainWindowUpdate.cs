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
	static class MainWindowUpdate
	{
		private static double teamSizeDifference = 0;

		//Actual resizing of the team display section to handle resizing of window or different screen sizes
		public static void setTeamsHeight(MainWindow caller)
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

		//Called to display a created team
		public static void DisplayTeam(MainWindow caller)
		{
            int temp = Globals.currentIntervention;
            Stopwatch interventionTimer = new Stopwatch();
            interventionTimer.Start();

            Globals.timers.Add(temp, interventionTimer);

            Globals.currentIntervention += 1;

			System.Windows.Controls.ScrollViewer Scroller = caller.getScroller();
			System.Windows.Controls.StackPanel TeamList = caller.getTeamList();
			System.Windows.Controls.Border TeamSection = caller.getTeamSection();

			//
			// Creation of all the objects needed for the teams display
			//
			System.Windows.Shapes.Rectangle seperator = new System.Windows.Shapes.Rectangle();
			seperator.Height = 5;
			
			System.Windows.Controls.Border mainBorder = new System.Windows.Controls.Border();
			mainBorder.BorderBrush = new SolidColorBrush(Colors.Black); ;
			mainBorder.BorderThickness = new System.Windows.Thickness(1);
			mainBorder.CornerRadius = new System.Windows.CornerRadius(5);

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

						//TODO: Have to make the pieces of equipment clickable so that they can be removed from the team

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
			TeamList.Children.Add(seperator);
			TeamList.Children.Add(mainBorder);
				mainBorder.Child = mainStackPanel;
					mainStackPanel.Children.Add(teamNameGrid);
						teamNameGrid.Children.Add(teamName);
						teamNameGrid.Children.Add(equipmentStackPanel);
					mainStackPanel.Children.Add(teamMember1);
						teamMember1.Children.Add(teamMember1Name);
					mainStackPanel.Children.Add(teamMember2);
						teamMember2.Children.Add(teamMember2Name);
		}

        public static void TimeTest_MenuItem_Click(MainWindow caller)
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
