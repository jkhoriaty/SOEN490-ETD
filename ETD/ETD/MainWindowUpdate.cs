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
			TeamSection.Height = caller.MapBorder.ActualHeight;
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
								teamName.Content = caller.getPhoneticLetter(team.getName());
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
					int highestTraining = 0;
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
							if(highestTraining < member.getTrainingLevel())
							{
								highestTraining = member.getTrainingLevel();
							}
							ImageBrush img = new ImageBrush();
							switch(member.getTrainingLevel())
							{
								case 0:
									img.ImageSource = caller.getImage(@"\Icons\First_Aid2.png");
									break;
								case 1:
									img.ImageSource = caller.getImage(@"\Icons\First_Responder2.png");
									break;
								case 2:
									img.ImageSource = caller.getImage(@"\Icons\Medicine2.png");
									break;
							}
							training.Fill = img;
							teamMember.Children.Add(training);
					}
            
					//Displaying the team's training as the highest member training
					Rectangle teamTraining = new Rectangle();
					teamTraining.Width = 35;
					teamTraining.Height = 35;
					ImageBrush teamImg = new ImageBrush();
					switch (highestTraining)
					{
						case 0:
							teamImg.ImageSource = caller.getImage(@"\Icons\First_Aid2.png");
							break;
						case 1:
							teamImg.ImageSource = caller.getImage(@"\Icons\First_Responder2.png");
							break;
						case 2:
							teamImg.ImageSource = caller.getImage(@"\Icons\Medicine2.png");
							break;
					}
					teamTraining.Fill = teamImg;
					equipmentStackPanel.Children.Add(teamTraining);
		}

		public void AddEquipment(Equipment equip, String teamName)
		{
			//TODO: Move to controller
			/*
			Equipment one = new Equipment("ambulance cart");
			team.addEquipment(one);
			 */

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
			if(equip.getEquipmentName() == equipments.ambulanceCart)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\AmbulanceCart3.png");
			}
			else if (equip.getEquipmentName() == equipments.epipen)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\epipen.png");
			}
			else if (equip.getEquipmentName() == equipments.mountedStretcher)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\MountedStretcher3.png");
			}
			else if (equip.getEquipmentName() == equipments.sittingCart)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\SittingCart3.png");
			}
			else if (equip.getEquipmentName() == equipments.transportStretcher)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\TransportStretcher.png");
			}
			else if (equip.getEquipmentName() == equipments.wheelchair)
			{
				equipmentImage.ImageSource = caller.getImage(@"\Icons\WheelChair.png");
			}
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
									//Adding the equipment to the StackPanel
									section.Children.Add(equipment);
								}
							}
						}
					}
				}
			}
		}

        //add or remove equipment. 
        //Clicking on the +/- button will open a form and let the user decide which equipment to add or remove from the team
        public void EquipmentUpdate_Click()
        {


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
