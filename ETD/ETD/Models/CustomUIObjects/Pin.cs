using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;

namespace ETD.Models.CustomUIObjects
{
	class Pin : Grid
	{
		static List<Pin> pinList = new List<Pin>();

		Rectangle imageRectangle;

		//Creating Grid with passed parameters
		public Pin(MapSectionPage mapSection, int size) : base()
		{
			this.Width = size;
			this.Height = size;
			this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart);
			this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop);
			this.MouseMove += new MouseEventHandler(mapSection.DragMove);
			this.ContextMenu = mapSection.Resources["ContextMenu"] as ContextMenu;
			this.ContextMenuOpening += new ContextMenuEventHandler(mapSection.CheckRight);
			(this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

			pinList.Add(this);
		}

		//Setting the background image to the passed image
		public void setImage(BitmapImage image)
		{
			imageRectangle = new Rectangle();
			imageRectangle.Width = this.Width;
			imageRectangle.Height = this.Height;
			ImageBrush img = new ImageBrush();
			img.ImageSource = image;
			imageRectangle.Fill = img;
			this.Children.Add(imageRectangle);
		}

		//Adding text to the pin for interventions and teams
		public void setText(String text)
		{
			Viewbox viewbox = new Viewbox();
			viewbox.Width = this.Width;
			viewbox.Height = this.Height;
			viewbox.HorizontalAlignment = HorizontalAlignment.Center;
			viewbox.VerticalAlignment = VerticalAlignment.Center;
			this.Children.Add(viewbox);

			TextBlock nameLabel = new TextBlock();
			nameLabel.Text = text;
			nameLabel.FontWeight = FontWeights.DemiBold;
			nameLabel.IsHitTestVisible = false;

			viewbox.Child = nameLabel;
		}

		//Setting the pin position to X and Y coordinates
		public void setPinPosition(double X, double Y)
		{
			Canvas.SetLeft(this, X - (this.Width / 2));
			Canvas.SetTop(this, Y - (this.Height / 2));
		}

		private double getX()
		{
			return Canvas.GetLeft(this) + (this.Width / 2);
		}

		private double getY()
		{
			return Canvas.GetTop(this) + (this.Height / 2);
		}

		public void DetectCollision(Canvas Canvas_map)
		{
			double movedPin_X = this.getX();
			double movedPin_Y = this.getY();

			//Replacing item within horizontal bounds
			if (movedPin_X > (Canvas_map.ActualWidth - (this.Width / 2))) //Right
			{
				movedPin_X = Canvas_map.ActualWidth - (this.Width / 2);
			}
			else if (movedPin_X < (this.Width / 2)) //Left
			{
				movedPin_X = (this.Width / 2);
			}

			//Replacing item within vertical bounds
			if (movedPin_Y > (Canvas_map.ActualHeight - (this.Height / 2))) //Bottom
			{
				movedPin_Y = Canvas_map.ActualHeight - (this.Height / 2);
			}
			else if (movedPin_Y < (this.Height / 2)) //Top
			{
				movedPin_Y = (this.Height / 2);
			}

			bool collisionDetected = true;
			int verificationCount = 0;
			Grid intervention = null;

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{
				collisionDetected = false;
				verificationCount++;

				//Iterating throught all pins
				foreach (Pin fixedPin in pinList)
				{
					//Skipping collision-detection with itself
					if (fixedPin == this)
					{
						continue;
					}

					//If collision detection is being done on an intervention border, ignore collision with all pins related to it
					bool related = false;
					/*if (this.GetType().Equals("ETD.Models.CustomUIObjects.InterventionContainer"))
					{
						foreach (KeyValuePair<Grid, BorderGrid> interventionBorderPair in interventionBorders)
						{
							if (interventionBorderPair.Value == this && (activeTeams[interventionBorderPair.Key].Contains(fixedPin) || fixedPin == interventionBorderPair.Key))
							{
								intervention = interventionBorderPair.Key;
								related = true;
							}
						}
					}

					if (this.GetType().Equals("ETD.Models.CustomUIObjects.InterventionPin") && fixedPin.GetType().Equals("ETD.Models.CustomUIObjects.InterventionContainer")  && interventionBorders.ContainsKey(this) && interventionBorders[this] == fixedPin)
					{
						related = true;
					}*/

					if (related)
					{
						continue;
					}

					//Getting the position of where the rectangle has been dropped (center point)
					double fixedPin_X = fixedPin.getX();
					double fixedPin_Y = fixedPin.getY();

					//If equipment is dropped on team and it overlaps more than 25% (assumption: not by mistake)
					if (this.GetType().Equals("ETD.Models.CustomUIObjects.EquipmentPin") && fixedPin.GetType().Equals("ETD.Models.CustomUIObjects.TeamPin") && movedPin_X > (fixedPin_X - (this.Width / 2)) && movedPin_X < (fixedPin_X + (this.Width / 2)) && movedPin_Y > (fixedPin_Y - (this.Height / 2)) && movedPin_Y < (fixedPin_Y + (this.Height / 2)))
					{
						//mapSection.AddTeamEquipment(this.Name, fixedPin.Name);
						Canvas parent = (Canvas)this.Parent;
						parent.Children.Remove(this);
						return;
					}

					//If a team is dropped on an intervention and it overlaps more than 25% of the moved pin
					if (this.GetType().Equals("ETD.Models.CustomUIObjects.TeamPin") && fixedPin.GetType().Equals("ETD.Models.CustomUIObjects.InterventionPin") && movedPin_X > (fixedPin_X - (fixedPin.Width / 2)) && movedPin_X < (fixedPin_X + (fixedPin.Width / 2)) && movedPin_Y > (fixedPin_Y - (fixedPin.Height / 2)) && movedPin_Y < (fixedPin_Y + (fixedPin.Height / 2)))
					{/*
						//Add the team to the intervention, create or change the intervention border appropriately
						if (!interventionBorders.ContainsKey(fixedPin))
						{
							BorderGrid borderGrid = new BorderGrid(fixedPin.Name, fixedPin.Width, fixedPin.Height + this.Height);
							Canvas_map.Children.Add(borderGrid);
							interventionBorders.Add(fixedPin, borderGrid);
							activeTeams.Add(fixedPin, new List<Grid>());
						}
						activeTeams[fixedPin].Add(this);
						DrawInterventionBorder(fixedPin);
						mapSection.AddResource(this.Name, fixedPin.Name);
						return;*/
					}

					//Checking if the dropped rectangle is within the bounds of any other rectangle
					while (movedPin_X > (fixedPin_X - ((this.Width / 2) + (fixedPin.Width / 2))) && movedPin_X < (fixedPin_X + ((this.Width / 2) + (fixedPin.Width / 2))) && movedPin_Y > (fixedPin_Y - ((this.Height / 2) + (fixedPin.Height / 2))) && movedPin_Y < (fixedPin_Y + ((this.Height / 2) + (fixedPin.Height / 2))))
					{
						//Collision detected, resolution by shifting the rectangle in the same direction that it has been dropped
						collisionDetected = true;

						//Rounding values to avoid ratio division by tiny number creating a huge ratio
						movedPin_X = Math.Round(movedPin_X, 3);
						movedPin_Y = Math.Round(movedPin_Y, 3);

						//Finding out how much is the dropped rectangle covering the fixed one
						double horizontalDifference = Math.Round((movedPin_X - fixedPin_X), 3);
						double verticalDifference = Math.Round((movedPin_Y - fixedPin_Y), 3);
						double differenceRatio = 0.1;
						bool moved = false;

						//Don't move horizontally if there are no difference and avoiding division by 0
						if (horizontalDifference != 0)
						{
							//Finding the ratio at which we should increase the values to put the objects side by side but in the same direction as it was dropped
							differenceRatio = Math.Round(((Math.Abs(verticalDifference) / Math.Abs(horizontalDifference)) / 10), 3);

							//Shifting horizontally in the correct direction, if not at the border
							if ((this.Width / 2) < movedPin_X && movedPin_X < (Canvas_map.ActualWidth - (this.Width / 2)))
							{
								if (horizontalDifference < 0)
								{
									movedPin_X -= 0.1;
									moved = true;
								}
								else
								{
									movedPin_X += 0.1;
									moved = true;
								}
							}
						}

						//Don't move vertically if there are no difference
						if (verticalDifference != 0)
						{
							//Shifting vertically in the correct direction
							if ((this.Height / 2) < movedPin_Y && movedPin_Y < (Canvas_map.ActualHeight - (this.Height / 2)))
							{
								if (verticalDifference < 0)
								{
									movedPin_Y -= differenceRatio;
									moved = true;
								}
								else
								{
									movedPin_Y += differenceRatio;
									moved = true;
								}
							}
						}

						//Handling situation where object is dropped between two others and is just bouncing around, placing object in the middle
						if (verificationCount > 100)
						{
							MessageBox.Show("The dropped object is dropped between two objects and is bouncing around with no progress. Resetting it.");
							if (movedPin_Y < (Canvas_map.ActualHeight / 2))
							{
								movedPin_Y += fixedPin.Height;
							}
							else
							{
								movedPin_Y -= fixedPin.Height;
							}
							verificationCount = 0;
							moved = true;
						}

						//Handling case of perfect superposition and placing right of fixed item if in the left half of the map, otherwise place left
						if (movedPin_X == fixedPin_X && movedPin_Y == fixedPin_Y)
						{
							if (fixedPin_X < (Canvas_map.ActualWidth / 2))
							{
								movedPin_X = movedPin_X + ((this.Width / 2) + (fixedPin.Width / 2));
							}
							else
							{
								movedPin_X = movedPin_X - ((this.Width / 2) + (fixedPin.Width / 2));
							}
							moved = true;
						}

						//Handling corner situation
						if (moved == false)
						{
							double horizontalToBorder = Math.Min(fixedPin_X, (Canvas_map.ActualWidth - fixedPin_X));
							double verticalToBorder = Math.Min(fixedPin_Y, (Canvas_map.ActualHeight - fixedPin_Y));

							if (horizontalToBorder <= verticalToBorder) //Need horizontal movement
							{
								if (movedPin_X <= (this.Width / 2)) //Left
								{
									movedPin_X = fixedPin_X + ((this.Width / 2) + (fixedPin.Width / 2));
								}
								else //Right
								{
									movedPin_X = fixedPin_X - ((this.Width / 2) + (fixedPin.Width / 2));
								}
							}
							else //Need vertical movement
							{
								if (movedPin_Y <= (this.Height / 2)) //Left
								{
									movedPin_Y = fixedPin_Y + ((this.Height / 2) + (fixedPin.Height / 2));
								}
								else //Right
								{
									movedPin_Y = fixedPin_Y - ((this.Height / 2) + (fixedPin.Height / 2));
								}
							}
						}
					}

				}
			}

			//Drop the rectangle if there are not collision or after resolution of collision
			setPinPosition(movedPin_X, movedPin_Y);
			/*
			//If the border has moved, replace all items back in the border
			if (this.GetType().Equals("ETD.Models.CustomUIObjects.InterventionContainer") && intervention != null)
			{
				setPinPosition(intervention, movedPin_X, movedPin_Y - (this.Height / 2) + (intervention.Height / 2));
				PlaceInterventionPins(intervention);
			}

			if (interventionBorders.ContainsKey(this))
			{
				DetectCollision(interventionBorders[this], movedPin_X, movedPin_Y - (this.Height / 2) + (interventionBorders[this].Height / 2));
			}*/
		}
	}
}
