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
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace ETD
{
	class MainWindowUpdate : IObserver
	{
		private MainWindow caller;
		private static double teamSizeDifference = 0;
		int shapeRadius = 20;

		public MainWindowUpdate(MainWindow caller)
		{
			this.caller = caller;
            LanguageSelector.attach(this);
		}

		//Actual resizing of the team display section to handle resizing of window or different screen sizes
		public void setTeamsHeight()
		{
			if (teamSizeDifference == 0)
			{
				teamSizeDifference = caller.TeamSection.ActualHeight - caller.TeamList.ActualHeight;
			}
			caller.TeamSection.Height = caller.MapBorder.ActualHeight;
			caller.Scroller.MaxHeight = caller.TeamSection.Height - teamSizeDifference;
		}

		//Processing of the selected image and displaying it as a background
		public void LoadMap(BitmapImage coloredImage)
        {
			//Making the picture grayscale
			FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
            grayBitmap.BeginInit();
            grayBitmap.Source = coloredImage;
            grayBitmap.DestinationFormat = PixelFormats.Gray8;
            grayBitmap.EndInit();

			//Displaying the map as the background
            caller.Map.Background = new ImageBrush(grayBitmap);
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
		public void DisplayTeam(Team team)
		{
			//TODO: Need to be moved to model in team creation
            Timer.StartTimer();

			StackPanel TeamList = caller.TeamList;

			//
			// Creation of all the objects needed for the teams display
			//
			Border mainBorder = new Border();
			mainBorder.Name = team.getName();
			mainBorder.BorderBrush = new SolidColorBrush(Colors.Black);
			mainBorder.BorderThickness = new System.Windows.Thickness(1);
			mainBorder.CornerRadius = new System.Windows.CornerRadius(5);
			Thickness topMargin = mainBorder.Margin;
			topMargin.Top = 5;
			mainBorder.Margin = topMargin;
			TeamList.Children.Add(mainBorder); //Adding to the view

				StackPanel mainStackPanel = new StackPanel();
				mainBorder.Child = mainStackPanel;

					Grid teamNameGrid = new Grid();
					teamNameGrid.Background = new SolidColorBrush(Colors.Black);
					teamNameGrid.Name = "teamNameGrid";
					mainStackPanel.Children.Add(teamNameGrid);

						StackPanel teamNameStackPanel = new StackPanel();
						teamNameStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
						teamNameGrid.Children.Add(teamNameStackPanel);

							Label teamName = new Label();
							if (team.getName().Length == 1)
							{
								teamName.Content = Services.getPhoneticLetter(team.getName());
							}
							else
							{
								teamName.Content = team.getName();
							}
							teamName.FontWeight = FontWeights.Bold;
							teamName.FontSize = 20;
							teamName.Foreground = new SolidColorBrush(Colors.White);
							teamNameStackPanel.Children.Add(teamName);

						//This will hold the training of the team as well as all the pieces of equipment that the team carries
						StackPanel equipmentStackPanel = new StackPanel();
						equipmentStackPanel.Name = "equipmentStackPanel";
						equipmentStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
						equipmentStackPanel.Orientation = Orientation.Horizontal;
						equipmentStackPanel.FlowDirection = FlowDirection.RightToLeft;
						teamNameGrid.Children.Add(equipmentStackPanel);

					//Adding all of the members to the list under the team name
					TeamMember member = null;
					int i = 0;
					while((member = team.getMember(i++)) != null)
					{
						Grid teamMember = new Grid();
						mainStackPanel.Children.Add(teamMember);

							Label teamMemberName = new Label();
							teamMemberName.Content = member.getName();
							teamMemberName.HorizontalAlignment = HorizontalAlignment.Left;
							teamMemberName.FontSize = 18;
							teamMember.Children.Add(teamMemberName);

							Rectangle training = new Rectangle();
							training.HorizontalAlignment = HorizontalAlignment.Right;
							training.Width = 30;
							training.Height = 30;
							ImageBrush img = new ImageBrush();
							img.ImageSource = Services.getImage(member.getTrainingLevel());
							training.Fill = img;
							teamMember.Children.Add(training);
					}
            
					//Displaying the team's training as the highest member training
					Rectangle teamTraining = new Rectangle();
					teamTraining.Width = 35;
					teamTraining.Height = 35;
					ImageBrush teamImg = new ImageBrush();
					teamImg.ImageSource = Services.getImage((trainings) team.getHighestLevelOfTraining());
					teamTraining.Fill = teamImg;
					equipmentStackPanel.Children.Add(teamTraining);
		}

		public void AddEquipment(Equipment equip, String teamName)
		{
			//Creating the rectangle in which the equipment is going to reside
			Rectangle equipment = new Rectangle();
			equipment.Width = 27;
			equipment.Height = 27;
			Thickness equipmentMargin = equipment.Margin;
			equipmentMargin.Right = 2;
			equipmentMargin.Left = 2;
			equipment.Margin = equipmentMargin;

			//Getting the background image to the rectangle
			ImageBrush equipmentImage = new ImageBrush();
			equipmentImage.ImageSource = Services.getImage(equip.getEquipmentName());
			equipment.Fill = equipmentImage;

			//Getting the appropriate equipment StackPanel
			foreach (Border teams in caller.TeamList.Children)
			{
				if (teams.Name.Equals(teamName))
				{
					StackPanel sp = (StackPanel)teams.Child;
					foreach (Grid info in sp.Children)
					{
						if (info.Name.Equals("teamNameGrid"))
						{
							foreach (StackPanel section in info.Children)
							{
								if (section.Name.Equals("equipmentStackPanel"))
								{
									if (section.Children.Count < 4)
									{
										//Adding the equipment to the StackPanel
										section.Children.Add(equipment);
									}
									else
									{
										MessageBox.Show("The team cannot hold more than 3 pieces of equipment!");
									}
								}
							}
						}
					}
				}
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
