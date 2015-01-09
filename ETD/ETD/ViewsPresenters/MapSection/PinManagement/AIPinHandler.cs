using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ETD.ViewsPresenters.MapSection.PinManagement
{
	class AIPinHandler
	{
		
        private AdditionalInfoPage AIPmap;
		private bool _isRectDragInProg;
		private Grid movingGrid;


        public AIPinHandler(AdditionalInfoPage AIP)
        {
            this.AIPmap = AIP;
        }

		//Setting position of pin
		public void SetPinPosition(Grid g, double X, double Y)
		{
			Canvas.SetLeft(g, (X - (g.Width / 2)));
			Canvas.SetTop(g, (Y - (g.Width / 2)));
		}

		public void DragStart(object sender, MouseButtonEventArgs e)
		{
			Grid g = (Grid)sender;

			_isRectDragInProg = g.CaptureMouse();
			movingGrid = g;
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

			var mousePos = e.GetPosition(AIPmap.AdditionalMap);

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
			var mousePos = e.GetPosition(AIPmap.AdditionalMap);

			//Making sure it is not dragged out of bounds
			if (mousePos.X < (g.Width / 2) || (AIPmap.AdditionalMap.ActualWidth - (g.Width / 2)) < mousePos.X || mousePos.Y < (g.Width / 2) || (AIPmap.AdditionalMap.ActualHeight - (g.Width / 2)) < mousePos.Y)
			{
				return;
			}

			SetPinPosition(g, mousePos.X, mousePos.Y);
		}

		public void DetectCollision(Grid movedPin, double movedPin_X, double movedPin_Y)
		{
			//Replacing item within horizontal bounds
			if (movedPin_X > (AIPmap.AdditionalMap.ActualWidth - (movedPin.Width / 2))) //Right
			{
				movedPin_X = AIPmap.AdditionalMap.ActualWidth - (movedPin.Width / 2);
			}
			else if (movedPin_X < (movedPin.Width / 2)) //Left
			{
				movedPin_X = (movedPin.Width / 2);
			}

			//Replacing item within vertical bounds
			if (movedPin_Y > (AIPmap.AdditionalMap.ActualHeight - (movedPin.Width / 2))) //Bottom
			{
				movedPin_Y = AIPmap.AdditionalMap.ActualHeight - (movedPin.Width / 2);
			}
			else if (movedPin_Y < (movedPin.Width / 2)) //Top
			{
				movedPin_Y = (movedPin.Width / 2);
			}

			bool collisionDetected = true;
			int verificationCount = 0;

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{

				collisionDetected = false;
				verificationCount++;

				//Gathering all grids to search for collision
				var allPins = AIPmap.AdditionalMap.Children.OfType<Grid>().ToList();

				//Iterating throught them
				foreach (var fixedPin in allPins)
				{
					//Skipping collision-detection with itself
					if (fixedPin != movedPin)
					{
						//Getting the position of where the rectangle has been dropped
						double fixedPin_X = Math.Round((((double)Canvas.GetLeft(fixedPin)) + (fixedPin.Width / 2)), 3);
						double fixedPin_Y = Math.Round((((double)Canvas.GetTop(fixedPin)) + (fixedPin.Width / 2)), 3);


						//Checking if the dropped rectangle is within the bounds of any other rectangle
						while (movedPin_X > (fixedPin_X - ((movedPin.Width / 2) + (fixedPin.Width / 2))) && movedPin_X < (fixedPin_X + ((movedPin.Width / 2) + (fixedPin.Width / 2))) && movedPin_Y > (fixedPin_Y - ((movedPin.Width / 2) + (fixedPin.Width / 2))) && movedPin_Y < (fixedPin_Y + ((movedPin.Width / 2) + (fixedPin.Width / 2))))
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
								if ((movedPin.Width / 2) < movedPin_X && movedPin_X < (AIPmap.AdditionalMap.ActualWidth - (movedPin.Width / 2)))
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
								if ((movedPin.Width / 2) < movedPin_Y && movedPin_Y < (AIPmap.AdditionalMap.ActualHeight - (movedPin.Width / 2)))
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
								if(movedPin_Y < (AIPmap.AdditionalMap.ActualHeight / 2))
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
                                if (fixedPin_X < (AIPmap.AdditionalMap.ActualWidth / 2))
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
                                double horizontalToBorder = Math.Min(fixedPin_X, (AIPmap.AdditionalMap.ActualWidth - fixedPin_X));
                                double verticalToBorder = Math.Min(fixedPin_Y, (AIPmap.AdditionalMap.ActualHeight - fixedPin_Y));

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
									if (movedPin_Y <= (movedPin.Width / 2)) //Left
									{
										movedPin_Y = fixedPin_Y + ((movedPin.Width / 2) + (fixedPin.Width / 2));
									}
									else //Right
									{
										movedPin_Y = fixedPin_Y - ((movedPin.Width / 2) + (fixedPin.Width / 2));
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
            var allPins = AIPmap.AdditionalMap.Children.OfType<Grid>().ToList();

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
