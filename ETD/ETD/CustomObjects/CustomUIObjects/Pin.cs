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

namespace ETD.CustomObjects.CustomUIObjects
{
	class Pin : Grid
	{
		internal static List<Pin> pinList = new List<Pin>(); //Contains all pins, used for collision detection
		private static Dictionary<object, double[]> pinPositionList = new Dictionary<object, double[]>(); //Used to recover previous pin position after Update callback that clears the whole map

		private static Pin draggedPin;

		private object relatedObject;

		//Creating regular pin
		public Pin(object relatedObject, MapSectionPage mapSection, int size) : base()
		{
			//Setting relatedObject, used for position recovery
			this.relatedObject = relatedObject;

			//Initializing grid attibutes
			this.Width = size;
			this.Height = size;
			this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart_MouseLeftButtonDown);
			this.MouseMove += new MouseEventHandler(mapSection.DragMove_MouseMove);
			this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop_MouseLeftButtonUp);
			this.ContextMenu = mapSection.Resources["ContextMenu"] as ContextMenu;
			this.ContextMenuOpening += new ContextMenuEventHandler(mapSection.CheckRight);
			(this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

			//Adding the pin to the list of all pins
			pinList.Add(this);
		}

		//Constructor for Creating border pin
		public Pin(InterventionPin interventionPin)
		{
			this.relatedObject = interventionPin;

			//Adding the border pin to the list of all pins
			pinList.Add(this);
		}

		//Setting the background image to the passed image
		public void setImage(BitmapImage image)
		{
			Rectangle imageRectangle = new Rectangle();
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

		//If a pin already exists when the map is being rendered, return this position so it can be set to it
		public static double[] getPreviousPinPosition(object pinObject)
		{
			if (pinPositionList.ContainsKey(pinObject))
			{
				return pinPositionList[pinObject];
			}
			else
			{
				return null;
			}
		}

		//Setting the pin position to X and Y coordinates
		public void setPinPosition(double X, double Y)
		{
			Canvas.SetLeft(this, X - (this.Width / 2));
			Canvas.SetTop(this, Y - (this.Height / 2));

			//Updating the pinPositionList so that it reflects the actual last position of a pin
			UpdatePinPosition(X, Y);
		}

		//Updating or creating the last position of a pin
		private void UpdatePinPosition(double X, double Y)
		{
			if (pinPositionList.ContainsKey(relatedObject))
			{
				pinPositionList[relatedObject] = new double[] { X, Y };
			}
			else
			{
				pinPositionList.Add(relatedObject, new double[] { X, Y });
			}
		}

		//Get the X value of the center of the grid
		internal double getX()
		{
			return Canvas.GetLeft(this) + (this.Width / 2);
		}

		//Get the Y value of the center of the grid
		internal double getY()
		{
			return Canvas.GetTop(this) + (this.Height / 2);
		}

		//Clearing all pins off of the map and clearing the list of all pins
		public static void ClearAllPins(Canvas Canvas_map)
		{
			Canvas_map.Children.Clear();
			pinList.Clear();
		}

		internal bool IsOfType(String type)
		{
			return this.GetType().ToString().Equals("ETD.CustomObjects.CustomUIObjects." + type);
		}

		//Drag starting (when mouse left button is clicked)
		public virtual void DragStart()
		{
			draggedPin = this;
		}

		//Drag in progress
		public virtual void DragMove(Canvas Canvas_map, MouseEventArgs e)
		{
			//Get the position of the mouse relative to the Canvas
			var mousePosition = e.GetPosition(Canvas_map);

			//Keep the object within bounds even when user tries to drag it outside of them
			if (mousePosition.X < (this.Width / 2) || (Canvas_map.ActualWidth - (this.Width / 2)) < mousePosition.X || mousePosition.Y < (this.Height / 2) || (Canvas_map.ActualHeight - (this.Height / 2)) < mousePosition.Y)
			{
				return;
			}

			//If all conditions are met, place the object at the mouse position
			this.setPinPosition(mousePosition.X, mousePosition.Y);
		}

		//Object dropped (Left mouse button released)
		public virtual void DragStop(Canvas Canvas_map, MouseButtonEventArgs e)
		{
			//Handling situation where method is called on the pin that was not being dragged
			if (this != draggedPin)
			{
				return;
			}
			this.ReleaseMouseCapture();

			CollisionDetectionAndResolution(Canvas_map);
		}

		//Returns true if this pin (the moved pin) overlaps the fixed pin passed as an argument more than 25%, false otherwise
		internal bool SufficientOverlap(Pin fixedPin)
		{
			if (this.getX() > (fixedPin.getX() - (this.Width / 2)) && this.getX() < (fixedPin.getX() + (this.Width / 2)) && this.getY() > (fixedPin.getY() - (this.Height / 2)) && this.getY() < (fixedPin.getY() + (this.Height / 2)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		internal void CollisionDetectionAndResolution(Canvas Canvas_map)
		{
			CollisionDetectionAndResolution(Canvas_map, false);
		}

		//Collision detection and resolution on the pin
		internal void CollisionDetectionAndResolution(Canvas Canvas_map, bool defaultPosition)
		{
			double movedPin_X = this.getX();
			double movedPin_Y = this.getY();

			bool collisionDetected = true;
			int verificationCount = 0;

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{
				collisionDetected = false;
				verificationCount++;

				List<Pin> pinListCopy = new List<Pin>(pinList);
				//Iterating throught all pins
				foreach (Pin fixedPin in pinListCopy)
				{
					//Skipping collision-detection with itself
					if (fixedPin == this)
					{
						continue;
					}

					//Handling all special cases no matter what the movedPin is except if it just got added
					if (!defaultPosition)
					{
						bool skipCollisionDetection = HandleSpecialCollisions(fixedPin);

						if (skipCollisionDetection)
						{
							continue;
						}
					}

					//Getting the position of where the rectangle has been dropped (center point)
					double fixedPin_X = fixedPin.getX();
					double fixedPin_Y = fixedPin.getY();

					//If equipment is dropped on team and it overlaps more than 25% (assumption: not by mistake)
					/*if (this.GetType().Equals("ETD.Models.CustomUIObjects.EquipmentPin") && fixedPin.GetType().Equals("ETD.Models.CustomUIObjects.TeamPin") && movedPin_X > (fixedPin_X - (this.Width / 2)) && movedPin_X < (fixedPin_X + (this.Width / 2)) && movedPin_Y > (fixedPin_Y - (this.Height / 2)) && movedPin_Y < (fixedPin_Y + (this.Height / 2)))
					{
						//mapSection.AddTeamEquipment(this.Name, fixedPin.Name);
						Canvas parent = (Canvas)this.Parent;
						parent.Children.Remove(this);
						return;
					}*/

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

						//Handling situation where object is dropped between two others and is just bouncing around, moving it left or right depending on the part of the map the pin is on
						if (verificationCount > 100)
						{
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

			//Trigger after collision detection special tasks
			AfterCollisionDetection(Canvas_map);
		}
		
		//Handling special cases of collision detection, default: nothing has been handled
		internal virtual bool HandleSpecialCollisions(Pin fixedPin)
		{
			return false;
		}

		//Handling cases where some pins need to trigger actions for the correct placement of all pins, default: do nothing
		internal virtual void AfterCollisionDetection(Canvas Canvas_map)
		{
			return;
		}
	}
}
