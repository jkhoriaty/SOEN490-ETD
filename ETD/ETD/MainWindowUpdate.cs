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
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace ETD
{
	class MainWindowUpdate : IObserver
	{
		private MainWindow caller;
		private static double teamSizeDifference = 0;
		public int shapeRadius = 20;
		Dictionary<Rectangle, Label> relatedLabel = new Dictionary<Rectangle,Label>();

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

		//Collision detection amongst rectangles (i.e. any item) on the map
		public void collisionDetection(Grid g, double horizontalDropped, double verticalDropped)
		{
			//Replacing item within horizontal bounds
			if (horizontalDropped > (caller.Map.ActualWidth - shapeRadius)) //Right
			{
				horizontalDropped = caller.Map.ActualWidth - shapeRadius;
			}
			else if (horizontalDropped < shapeRadius) //Left
			{
				horizontalDropped = shapeRadius;
			}

			//Replacing item within vertical bounds
			if (verticalDropped > (caller.Map.ActualHeight - shapeRadius)) //Bottom
			{
				verticalDropped = caller.Map.ActualHeight - shapeRadius;
			}
			else if (verticalDropped < shapeRadius) //Top
			{
				verticalDropped = shapeRadius;
			}

			bool collisionDetected = true;
			int verificationCount = 0;

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{

				collisionDetected = false;
				verificationCount++;

				//Gathering all grids to search for collision
				var grids = caller.Map.Children.OfType<Grid>().ToList();

				//Iterating throught them
				foreach (var grid in grids)
				{
					//Skipping collision-detection with itself
					if (grid != g)
					{
						//Getting the position of where the rectangle has been dropped
						double horizontalFixed = Math.Round((((double)Canvas.GetLeft(grid)) + shapeRadius), 3);
						double verticalFixed = Math.Round((((double)Canvas.GetTop(grid)) + shapeRadius), 3);

						//Checking if the dropped rectangle is within the bounds of any other rectangle
						while (horizontalDropped > (horizontalFixed - (shapeRadius * 2)) && horizontalDropped < (horizontalFixed + (shapeRadius * 2)) && verticalDropped > (verticalFixed - (shapeRadius * 2)) && verticalDropped < (verticalFixed + (shapeRadius * 2)))
						{
							//Collision detected, resolution by shifting the rectangle in the same direction that it has been dropped
							collisionDetected = true;

							//Rounding values to avoid ratio division by tiny number creating a huge ratio
							horizontalDropped = Math.Round(horizontalDropped, 3);
							verticalDropped = Math.Round(verticalDropped, 3);

							//Finding out how much is the dropped rectangle covering the fixed one
							double horizontalDifference = Math.Round((horizontalDropped - horizontalFixed), 3);
							double verticalDifference = Math.Round((verticalDropped - verticalFixed), 3);
							double differenceRatio = 0.1;
							bool moved = false;

							//Don't move horizontally if there are no difference and avoiding division by 0
							if (horizontalDifference != 0)
							{
								//Finding the ratio at which we should increase the values to put the objects side by side but in the same direction as it was dropped
								differenceRatio = Math.Round(((Math.Abs(verticalDifference) / Math.Abs(horizontalDifference)) / 10), 3);

								//Shifting horizontally in the correct direction, if not at the border
								if (shapeRadius < horizontalDropped && horizontalDropped < (caller.Map.ActualWidth - shapeRadius))
								{
									if (horizontalDifference < 0)
									{
										horizontalDropped -= 0.1;
										moved = true;
									}
									else
									{
										horizontalDropped += 0.1;
										moved = true;
									}
								}
							}

							//Don't move vertically if there are no difference
							if (verticalDifference != 0)
							{
								//Shifting vertically in the correct direction
								if (shapeRadius < verticalDropped && verticalDropped < (caller.Map.ActualHeight - shapeRadius))
								{
									if (verticalDifference < 0)
									{
										verticalDropped -= differenceRatio;
										moved = true;
									}
									else
									{
										verticalDropped += differenceRatio;
										moved = true;
									}
								}
							}

							//Handling situation where object is dropped between two others and is just bouncing around, placing object in the middle
							if (verificationCount > 100)
							{

								MessageBox.Show("The dropped object is dropped between two objects and is bouncing around with no progress. Resetting it.");
								horizontalDropped = (caller.Map.ActualWidth / 2);
								verticalDropped = (caller.Map.ActualHeight / 2);
								verificationCount = 0;
							}

							//Handling case of perfect superposition and placing right of fixed item
							if (horizontalDropped == horizontalFixed && verticalDropped == verticalFixed)
							{
								horizontalDropped = horizontalDropped + (2 * shapeRadius);
								moved = true;
							}

							//Handling corner situation, placing object back in the middle
							if (moved == false)
							{
								MessageBox.Show("There's not enough space in the corner for this item. Replacing it in the center for you to replace it elsewhere.");

								double horizontalToBorder = Math.Min(horizontalFixed, (caller.Map.ActualWidth - horizontalFixed));
								double verticalToBorder = Math.Min(verticalFixed, (caller.Map.ActualHeight - verticalFixed));

								if (horizontalToBorder <= verticalToBorder) //Need horizontal mvoement
								{
									if (horizontalDropped <= shapeRadius) //Left
									{
										horizontalDropped = horizontalFixed + (shapeRadius * 2);
									}
									else //Right
									{
										horizontalDropped = horizontalFixed - (shapeRadius * 2);
									}
								}
								else //Need vertical mvoement
								{
									if (verticalDropped <= shapeRadius) //Left
									{
										verticalDropped = verticalFixed + (shapeRadius * 2);
									}
									else //Right
									{
										verticalDropped = verticalFixed - (shapeRadius * 2);
									}
								}
							}
						}
					}
				}
			}

			//Drop the rectangle if there are not collision or after resolution of collision
			setPosition(g, horizontalDropped, verticalDropped);
		}

		public void setPosition(Grid g, double horizontalDropped, double verticalDropped)
		{
			Canvas.SetLeft(g, (horizontalDropped - shapeRadius));
			Canvas.SetTop(g, (verticalDropped - shapeRadius));
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
		
		//Called to display a created team information in the Team list
		public void DisplayTeamInfo(Team team)
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
					teamImg.ImageSource = Services.getImage(team.getHighestLevelOfTraining());
					teamTraining.Fill = teamImg;
					equipmentStackPanel.Children.Add(teamTraining);
		}

		public void DisplayTeamPin(Team team)
		{
			Grid grid = new Grid();
			grid.Name = team.getName();
			grid.Tag = "team";
			grid.Width = shapeRadius * 2;
			grid.Height = shapeRadius * 2;
			grid.MouseLeftButtonDown += new MouseButtonEventHandler(caller.grid_MouseLeftButtonDown);
			grid.MouseLeftButtonUp += new MouseButtonEventHandler(caller.grid_MouseLeftButtonUp);
			grid.MouseMove += new MouseEventHandler(caller.grid_MouseMove);

				Rectangle r = new Rectangle();
				r.Width = shapeRadius * 2;
				r.Height = shapeRadius * 2;
				ImageBrush img = new ImageBrush();
				img.ImageSource = Services.getImage(team, statuses.available);
				r.Fill = img;
				r.Width = shapeRadius * 2;
				r.Height = shapeRadius * 2;

				Label l = new Label();
				l.Width = shapeRadius * 2;
				l.Height = shapeRadius * 2;
				l.FontWeight = FontWeights.DemiBold;
				DropShadowEffect shadow = new DropShadowEffect();
				shadow.ShadowDepth = 3;
				shadow.Direction = 315;
				shadow.Opacity = 1.0;
				shadow.BlurRadius = 3;
				shadow.Color = Colors.White;
				l.Effect = shadow;

				Thickness lMargin = l.Margin;//Hack magic needed to make text vertically centered
				l.Content = team.getName();
				switch (l.Content.ToString().Length)
				{
					case 1:
						l.FontSize = 28;
						lMargin.Top = -10; 
						break;
					case 2:
						l.FontSize = 24;
						lMargin.Top = -6; 
						break;
					case 3:
						l.FontSize = 20;
						lMargin.Top = -3; 
						break;
					case 4:
					default:
						l.FontSize = 16;
						lMargin.Top = -2; 
						break;
					case 5:
					case 6:
						l.FontSize = 12;
						lMargin.Top = -1; 
						break;
				}
				l.Margin = lMargin;
				l.HorizontalContentAlignment = HorizontalAlignment.Center;
				l.VerticalContentAlignment = VerticalAlignment.Center;
				l.IsHitTestVisible = false;

			caller.Map.Children.Add(grid);
				grid.Children.Add(r);
				grid.Children.Add(l);

			setPosition(grid, shapeRadius, shapeRadius); //Setting it top corner
			collisionDetection(grid, shapeRadius, shapeRadius);	
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
