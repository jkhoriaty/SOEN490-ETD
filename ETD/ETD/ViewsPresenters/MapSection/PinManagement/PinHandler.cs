using ETD.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ETD.ViewsPresenters.MapSection.PinManagement
{
	class PinHandler
	{
		private MapSectionPage mapSection;
        private AdditionalInfoPage AIPmap;
		private bool _isRectDragInProg;
		private Grid movingGrid;
		private Dictionary<Grid, Grid[]> activeInterventions = new Dictionary<Grid, Grid[]>();
		private Dictionary<Grid, double[]> offsets = new Dictionary<Grid, double[]>();

		public PinHandler(MapSectionPage mapSection)
		{
			this.mapSection = mapSection;
		}

        public PinHandler(AdditionalInfoPage AIP)
        {
            this.AIPmap = AIP;
        }

		//Setting position of pin
		public void SetPinPosition(Grid g, double X, double Y)
		{
			Canvas.SetLeft(g, (X - (g.Width / 2)));
			Canvas.SetTop(g, (Y - (g.Height / 2)));
		}

		public void SetBorderPosition(Border b, double X, double Y)
		{
			Canvas.SetLeft(b, (X - (b.Width / 2)));
			Canvas.SetTop(b, (Y - (b.Height / 2)));
		}

		private Grid getActualContainer(Grid grid)
		{
			StackPanel stack = (StackPanel)grid.Parent;
			Border border = (Border)stack.Parent;
			return (Grid)border.Parent;
		}

		public void DragStart(object sender, MouseButtonEventArgs e)
		{
			Grid g = (Grid)sender;

			_isRectDragInProg = g.CaptureMouse();
			movingGrid = g;

			if (activeInterventions.ContainsKey(g))
			{
				foreach(Grid interveningGrid in activeInterventions[g])
				{
					double X = (double)Canvas.GetLeft(g) - Canvas.GetLeft(interveningGrid);
					double Y = (double)Canvas.GetTop(g) - Canvas.GetTop(interveningGrid);
				}
			}
		}

		//Left Mouse Button Up: Any pin
		internal void DragStop(object sender, MouseButtonEventArgs e)
		{
			Grid g = (Grid)sender;

			//Avoid in having method called on object being collided with
			if (g != movingGrid)
			{
				return;
			}

			g.ReleaseMouseCapture();
			_isRectDragInProg = false;

			var mousePos = e.GetPosition(mapSection.Map);

			//Calling collision detection and resolution for the dropped object
			DetectCollision(g, mousePos.X, mousePos.Y);
		}

		//Mouse Move
		internal void DragMove(object sender, MouseEventArgs e)
		{
			//If no rectangle are clicked, exit method
			if (!_isRectDragInProg) return;

			Grid g = (Grid)sender;

			//Handling behaviour where fixed rectangle gets moved when another rectangle is dropped on it
			if (g != movingGrid)
			{
				return;
			}

			//Get the position of the mouse relative to the Canvas
			var mousePos = e.GetPosition(mapSection.Map);

			//Making sure it is not dragged out of bounds
			if (mousePos.X < (g.Width / 2) || (mapSection.Map.ActualWidth - (g.Width / 2)) < mousePos.X || mousePos.Y < (g.Height / 2) || (mapSection.Map.ActualHeight - (g.Height / 2)) < mousePos.Y)
			{
				return;
			}

			SetPinPosition(g, mousePos.X, mousePos.Y);
			if(activeInterventions.ContainsKey(g))
			{
				double border_Y = g.Height;
				switch(activeInterventions[g].Count())
				{
					case 6:
					case 5:
						border_Y += activeInterventions[g][0].Height;
						goto case 4;
					case 4:
					case 3:
						border_Y += activeInterventions[g][0].Height;
						goto case 2;
					case 2:
					case 1:
						border_Y += activeInterventions[g][0].Height;
						break;
				}
			}
		}

		public void DetectCollision(Grid movedPin, double movedPin_X, double movedPin_Y)
		{
			//Replacing item within horizontal bounds
			if (movedPin_X > (mapSection.Map.ActualWidth - (movedPin.Width / 2))) //Right
			{
				movedPin_X = mapSection.Map.ActualWidth - (movedPin.Width / 2);
			}
			else if (movedPin_X < (movedPin.Width / 2)) //Left
			{
				movedPin_X = (movedPin.Width / 2);
			}

			//Replacing item within vertical bounds
			if (movedPin_Y > (mapSection.Map.ActualHeight - (movedPin.Height / 2))) //Bottom
			{
				movedPin_Y = mapSection.Map.ActualHeight - (movedPin.Height / 2);
			}
			else if (movedPin_Y < (movedPin.Height / 2)) //Top
			{
				movedPin_Y = (movedPin.Height / 2);
			}

			bool collisionDetected = true;
			int verificationCount = 0;

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{

				collisionDetected = false;
				verificationCount++;

				//Gathering all grids to search for collision
				var allPins = mapSection.Map.Children.OfType<Grid>().ToList();

				//Iterating throught them
				foreach (var fixedPin in allPins)
				{
					//Skipping collision-detection with itself
					if (fixedPin != movedPin && !fixedPin.Tag.Equals("border"))
					{
						//Getting the position of where the rectangle has been dropped
						double fixedPin_X = Math.Round((((double)Canvas.GetLeft(fixedPin)) + (fixedPin.Width / 2)), 3);
						double fixedPin_Y = Math.Round((((double)Canvas.GetTop(fixedPin)) + (fixedPin.Height / 2)), 3);

						//If equipment is dropped on team and it overlaps more than 25% (assumption: not by mistake)
						if (movedPin.Tag.Equals("equipment") && fixedPin.Tag.Equals("team") && movedPin_X > (fixedPin_X - (movedPin.Width / 2)) && movedPin_X < (fixedPin_X + (movedPin.Width / 2)) && movedPin_Y > (fixedPin_Y - (movedPin.Height / 2)) && movedPin_Y < (fixedPin_Y + (movedPin.Height / 2)))
						{
							mapSection.AddTeamEquipment(movedPin.Name, fixedPin.Name);
							Canvas parent = (Canvas)movedPin.Parent;
							parent.Children.Remove(movedPin);
							return;
						}

						//If a team is dropped on an intervention and it overlaps more than 25% of the moved pin
						if (movedPin.Tag.Equals("team") && fixedPin.Tag.Equals("intervention") && movedPin_X > (fixedPin_X - (fixedPin.Width / 2)) && movedPin_X < (fixedPin_X + (fixedPin.Width / 2)) && movedPin_Y > (fixedPin_Y - (fixedPin.Height / 2)) && movedPin_Y < (fixedPin_Y + (fixedPin.Height / 2)))
						{
							BorderGrid borderGrid = new BorderGrid(fixedPin.Name, fixedPin.Width, fixedPin.Height + movedPin.Height);
							mapSection.Map.Children.Add(borderGrid);

							double verticalDifference = (fixedPin.Height / 2) + (movedPin.Height / 2);
							if ((fixedPin_Y + verticalDifference) > (mapSection.Map.ActualHeight - (movedPin.Height / 2))) //Bottom
							{
								SetPinPosition(fixedPin, fixedPin_X, (mapSection.Map.ActualHeight - ((fixedPin.Height / 2) + movedPin.Height) - 3));
								SetPinPosition(movedPin, fixedPin_X, (mapSection.Map.ActualHeight - (movedPin.Height / 2) - 3));
								SetPinPosition(borderGrid, fixedPin_X, (mapSection.Map.ActualHeight - (borderGrid.Height / 2) - 3));
							}
							else
							{
								SetPinPosition(movedPin, fixedPin_X, (fixedPin_Y + verticalDifference));
								SetPinPosition(borderGrid, fixedPin_X, (fixedPin_Y + (verticalDifference / 2)));
							}

							return;
						}

						//Checking if the dropped rectangle is within the bounds of any other rectangle
						while (movedPin_X > (fixedPin_X - ((movedPin.Width / 2) + (fixedPin.Width / 2))) && movedPin_X < (fixedPin_X + ((movedPin.Width / 2) + (fixedPin.Width / 2))) && movedPin_Y > (fixedPin_Y - ((movedPin.Height / 2) + (fixedPin.Height / 2))) && movedPin_Y < (fixedPin_Y + ((movedPin.Height / 2) + (fixedPin.Height / 2))))
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
								if ((movedPin.Width / 2) < movedPin_X && movedPin_X < (mapSection.Map.ActualWidth - (movedPin.Width / 2)))
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
								if ((movedPin.Height / 2) < movedPin_Y && movedPin_Y < (mapSection.Map.ActualHeight - (movedPin.Height / 2)))
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
								if(movedPin_Y < (mapSection.Map.ActualHeight / 2))
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
								if(fixedPin_X < (mapSection.Map.ActualWidth /2))
								{
									movedPin_X = movedPin_X + ((movedPin.Width / 2) + (fixedPin.Width / 2));
								}
								else
								{
									movedPin_X = movedPin_X - ((movedPin.Width / 2) + (fixedPin.Width / 2));
								}
								moved = true;
							}

							//Handling corner situation
							if (moved == false)
							{
								double horizontalToBorder = Math.Min(fixedPin_X, (mapSection.Map.ActualWidth - fixedPin_X));
								double verticalToBorder = Math.Min(fixedPin_Y, (mapSection.Map.ActualHeight - fixedPin_Y));

								if (horizontalToBorder <= verticalToBorder) //Need horizontal movement
								{
									if (movedPin_X <= (movedPin.Width / 2)) //Left
									{
										movedPin_X = fixedPin_X + ((movedPin.Width / 2) + (fixedPin.Width / 2));
									}
									else //Right
									{
										movedPin_X = fixedPin_X - ((movedPin.Width / 2) + (fixedPin.Width / 2));
									}
								}
								else //Need vertical movement
								{
									if (movedPin_Y <= (movedPin.Height / 2)) //Left
									{
										movedPin_Y = fixedPin_Y + ((movedPin.Height / 2) + (fixedPin.Height / 2));
									}
									else //Right
									{
										movedPin_Y = fixedPin_Y - ((movedPin.Height / 2) + (fixedPin.Height / 2));
									}
								}
							}
						}
					}
				}
			}

			//Drop the rectangle if there are not collision or after resolution of collision
			SetPinPosition(movedPin, movedPin_X, movedPin_Y);
		}

		public void movePins(double widthRatio, double heightRatio)
		{
			var allPins = mapSection.Map.Children.OfType<Grid>().ToList();

			foreach (var pin in allPins)
			{
				double movedPin_X = widthRatio * ((double)Canvas.GetLeft(pin) + (pin.Width / 2));
				double movedPin_Y = heightRatio * ((double)Canvas.GetTop(pin) + (pin.Height / 2));

				SetPinPosition(pin, movedPin_X, movedPin_Y);
				DetectCollision(pin, movedPin_X, movedPin_Y);
			}
		}
	}
}
