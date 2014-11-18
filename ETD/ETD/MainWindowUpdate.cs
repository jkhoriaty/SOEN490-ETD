using Microsoft.Win32;
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
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ETD
{
	class MainWindowUpdate : IObserver
	{
		private MainWindow caller;
		private static double teamSizeDifference = 0;

		public MainWindowUpdate(MainWindow caller)
		{
			this.caller = caller;
            LanguageSelector.attach(this);
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
            Timer.StartTimer();
			//TODO: Need to be moved to model in team creation
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

        public static void LoadMap(MainWindow caller)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg; *.png; *.gif";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                System.IO.FileInfo File = new System.IO.FileInfo(openFileDialog.FileName);
                BitmapImage coloredImage = new BitmapImage(new Uri(openFileDialog.FileName));

                FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
                grayBitmap.BeginInit();
                grayBitmap.Source = coloredImage;
                grayBitmap.DestinationFormat = PixelFormats.Gray8;
                grayBitmap.EndInit();

                System.Windows.Controls.Canvas Map = caller.Map;
                Map.Background = new ImageBrush(grayBitmap);

                //System.Windows.Controls.TextBlock TimerText = caller.getTimer();
			}
		}

        // Updates all text fields when a language change is observed
        public void update()
        {
            //Example
            //control.text = LanguageSelector.getString(control.name)
        }
	}
}
