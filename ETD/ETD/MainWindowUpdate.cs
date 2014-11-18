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
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

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
		public void DisplayTeam(Team team)
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
					mainStackPanel.Children.Add(teamNameGrid);

						Label teamName = new Label();
						if (team.getName().Length == 1)
						{
							teamName.Content = caller.getLetter(team.getName());
						}
						else
						{
							teamName.Content = team.getName();
						}
						teamName.HorizontalAlignment = HorizontalAlignment.Left;
						teamName.FontWeight = FontWeights.Bold;
						teamName.FontSize = 20;
						teamName.Foreground = new SolidColorBrush(Colors.White);
						teamNameGrid.Children.Add(teamName);

						//This will hold the training of the team as well as all the pieces of equipment that the team carries
						StackPanel equipmentStackPanel = new StackPanel();
						equipmentStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
						equipmentStackPanel.Orientation = Orientation.Horizontal;
						equipmentStackPanel.FlowDirection = FlowDirection.RightToLeft;
                        equipmentStackPanel.Height = 30;
                        equipmentStackPanel.Width = 30;
                        
                        //testing adding equipment
                        // to be moved to equipmentupdate form
                        Equipment one = new Equipment("ambulance cart");
                        team.addEquipment(one);
             
                        Equipment equip = null;
                        Grid Equipment = new Grid();
                        Button EquipmentManipulation = new Button();
                        EquipmentManipulation.Width = 30;
                        EquipmentManipulation.HorizontalAlignment = HorizontalAlignment.Right;
                        EquipmentManipulation.FontWeight = FontWeights.ExtraBlack;
                        EquipmentManipulation.Background = Brushes.White;
                        EquipmentManipulation.Content = "+/-";
                        //EquipmentManipulation.Click += new RoutedEventHandler(EquipmentUpdate_Click);

                        Equipment.Children.Add(EquipmentManipulation);

                        mainStackPanel.Children.Add(Equipment);

                        int j = 0;
                        while ((equip = team.getEquipment(j))!=null)
                        {

                            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
                            myImage.Width = 25;
                            myImage.Height = 23;
                            BitmapImage myBitmapImage = new BitmapImage();
                            myBitmapImage.BeginInit();
                            myBitmapImage.DecodePixelWidth = 25;
                            String path = team.loadEquipment(team, j);                     
                            myBitmapImage.UriSource = caller.getIcon(path);
                            myBitmapImage.EndInit();
                            myImage.Source = myBitmapImage;
                            myImage.HorizontalAlignment = HorizontalAlignment.Left;

                            Equipment.Children.Add(myImage);
                  
                            j++;
                        }

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

        //add or remove equipment. 
        //Clicking on the +/- button will open a form and let the user decide which equipment to add or remove from the team
        public void EquipmentUpdate_Click()
        {


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
