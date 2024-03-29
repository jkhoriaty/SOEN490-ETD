﻿using ETD.Services;
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
using ETD.Models.Objects;

namespace ETD.CustomObjects.CustomUIObjects
{
	public class Pin : Grid
	{
		internal static List<Pin> pinList = new List<Pin>(); //Contains all pins, used for collision detection
		private static Dictionary<object, double[]> pinPositionList = new Dictionary<object, double[]>(); //Used to recover previous pin position after Update callback that clears the whole map
		
		internal static Pin draggedPin;//Pin used for collision detection, detects whether a pin is currently being moved on the map, is also used as a lock to disallow the GPS to move the pin while it's being dragged

		internal object relatedObject;//Pointer to object used for position recovery
		
        internal MapSectionPage mapSection;//Page on which team, intervention and equipment pins can be added

		//Variables used to draw arrow if item is tracked by GPS
		internal double startX = -1;
		internal double startY = -1;
		internal static Dictionary<object, Arrow> destinationArrowDictionnary = new Dictionary<object, Arrow>();

		//Creating regular pin
		public Pin(object relatedObject, MapSectionPage mapSection, int size) : base()
		{
			//Setting relatedObject, used for position recovery
			this.relatedObject = relatedObject;
			this.mapSection = mapSection;

			//Initializing grid attibutes
			this.Width = size;
			this.Height = size;
			this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart_MouseLeftButtonDown);
			this.MouseMove += new MouseEventHandler(mapSection.DragMove_MouseMove);
			this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop_MouseLeftButtonUp);

			//Adding the pin to the list of all pins
			pinList.Add(this);
		}

		//Constructor for Creating border pin
		public Pin(InterventionPin interventionPin, MapSectionPage mapSection)
		{
			this.relatedObject = interventionPin;
			this.mapSection = mapSection;

			//Adding the border pin to the list of all pins
			pinList.Add(this);
		}

        //Creating map modification pins
        public Pin(object relatedAiObject, AdditionalInfoPage aiSection, double width, double height)
        {
            //Setting relatedObject, used for position recovery
            relatedObject = relatedAiObject;

            //Initializing grid attibutes
            this.Width = width;
            this.Height = height;

            //Initializing grid attibutes
            this.MouseLeftButtonDown += new MouseButtonEventHandler(aiSection.DrawingStart);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(aiSection.DrawingStop);
            this.MouseUp += new MouseButtonEventHandler(aiSection.DrawingMove);
            this.MouseMove += new MouseEventHandler(aiSection.Move);
            this.MouseWheel += new MouseWheelEventHandler(aiSection.ChangeColor);


            //Adding the pin to the list of all pins
            pinList.Add(this);
        }
    
        //Returns the list of pins
		public static List<Pin> getPinList()
		{
			return pinList;
		}

		//Used for redrawing arrows after the whole map is redrawn
		internal static Arrow getPinArrow(object pinRelatedObject)
		{
			if(destinationArrowDictionnary.ContainsKey(pinRelatedObject))
			{
				return destinationArrowDictionnary[pinRelatedObject];
			}
			else
			{
				return null;
			}
		}

		//Setting the pins' background image to the passed image
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

        //Returns the type of the pin
		public object getRelatedObject()
		{
			return relatedObject;
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

		//If a pin already exists when the map is being rendered, return it's last known position position so it can be set to it
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
		public double getX()
		{
			return Canvas.GetLeft(this) + (this.Width / 2);
		}

		//Get the Y value of the center of the grid
		public double getY()
		{
			return Canvas.GetTop(this) + (this.Height / 2);
		}

		//Clearing all pins off of the map and clearing the list of all pins
		public static void ClearAllPins(Canvas Canvas_map)
		{
			Canvas_map.Children.Clear();

			//To avoid having vestigial observers on which update is called while they have been destroyed
			foreach(Pin pin in pinList)
			{
				pin.DeregisterPinFromObserver();
				if((pin.IsOfType("TeamPin") && ((TeamPin)pin).gpsLocation != null))
				{
					((TeamPin)pin).gpsLocation.DeregisterInstanceObserver((TeamPin)pin);
				}
				else if ((pin.IsOfType("InterventionPin") && ((InterventionPin)pin).gpsLocation != null))
				{
					((InterventionPin)pin).gpsLocation.DeregisterInstanceObserver((InterventionPin)pin);
				}
			}

			pinList.Clear();
		}

		//Used in order to avoid having vestigial observers
		internal virtual void DeregisterPinFromObserver()
		{
			return;
		}

		//Move all pins when the window gets resized so that they stay in the same position as they were before the resizing
		public static void MoveAllPins(double widthRatio, double heightRatio) //The ratios are the difference in percentage between the old screen size and the new one
		{
			foreach (Pin pin in pinList)
			{
				pin.MovePin(widthRatio, heightRatio);
			}
		}

		internal virtual void MovePin(double widthRatio, double heightRatio)
		{
			double movedPin_X = widthRatio * getX();
			double movedPin_Y = heightRatio * getY(); ;

			setPinPosition(movedPin_X, movedPin_Y);
			if(IsOfType("InterventionPin"))
			{
				CollisionDetectionAndResolution(false); //false to avoid the InterventionPin from colliding with it's own border causing the group to shift at each resizing of the window
			}
			else
			{
				CollisionDetectionAndResolution(true); //true to avoid having the resize add a team to an intervention or an equipment to a team
			}
		
		}

		//Checking the type of the pin; used mostly for special collisions
		internal bool IsOfType(String type)
		{
			return this.GetType().ToString().Equals("ETD.CustomObjects.CustomUIObjects." + type);
		}

		//Drag starting (when mouse left button is clicked)
		public virtual void DragStart()
		{
			draggedPin = this;

			//Keeping track of the start position of the pin being dragged in case the pin is tracked by GPS
			startX = getX();
			startY = getY();
		}

		//Drag in progress (when mouse moves and a pin is clicked)
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

			CollisionDetectionAndResolution(false);
			draggedPin = null;
		}

		//Returns true if this pin (the moved pin) overlaps the fixed pin passed as an argument more than 25%, false otherwise
		internal bool SufficientOverlap(Pin fixedPin)
		{
			if (this.getX() > (fixedPin.getX() - (fixedPin.Width / 2)) && this.getX() < (fixedPin.getX() + (fixedPin.Width / 2)) && this.getY() > (fixedPin.getY() - (fixedPin.Height / 2)) && this.getY() < (fixedPin.getY() + (fixedPin.Height / 2)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//Handling case when a pin tracked by GPS is dropped
		internal void GPSPinDrop(bool tracked)
		{
			//Create and draw the arrow to the destination point
			if (!destinationArrowDictionnary.ContainsKey(relatedObject))
			{
				destinationArrowDictionnary.Add(relatedObject, null); //Create the entry if inexistant
			}
			else if (destinationArrowDictionnary[relatedObject] != null)
			{
				destinationArrowDictionnary[relatedObject].HideArrow(); //Clear the arrow that it contains if it does exist
			}
			destinationArrowDictionnary[relatedObject] = new Arrow(mapSection.Canvas_map, startX, startY, getX(), getY());

			//Showing the line only if the team is tracked by GPS, i.e. if the phone is not offline or the connection to the server failed
			if(tracked)
			{
				destinationArrowDictionnary[relatedObject].DisplayArrow();
				
				//Replacing pin at the start point and ensuring it doesn't get added to an intervention by mistake
				setPinPosition(startX, startY);
				CollisionDetectionAndResolution(true);
			}
		}

		internal void RemoveArrow()
		{
			if (destinationArrowDictionnary.ContainsKey(relatedObject) && destinationArrowDictionnary[relatedObject] != null)
			{
				destinationArrowDictionnary[relatedObject].HideArrow();
				destinationArrowDictionnary[relatedObject] = null;
			}
		}

		//Collision detection and resolution on the pin
		internal void CollisionDetectionAndResolution(bool ignoreSpecialCollisions)
		{
			bool collisionDetected = true;
			int verificationCount = 0; //Counter so that the program will be able to recognize when an object is stuck and bouncing around endlessly

			//Replacing pin within bounds when it is dragged or moved by GPS outside of the map area
			if(getX() < this.Width / 2)
			{
				setPinPosition(this.Width / 2, getY());
			}
			if(getX() > mapSection.Canvas_map.ActualWidth - (this.Width / 2))
			{
				setPinPosition(mapSection.Canvas_map.ActualWidth - (this.Width / 2), getY());
			}

			if(getY() < this.Height / 2)
			{
				setPinPosition(getX(), this.Height / 2);
			}
			if(getY() > mapSection.Canvas_map.ActualHeight - (this.Height / 2))
			{
				setPinPosition(getX(), mapSection.Canvas_map.ActualHeight - (this.Height / 2));
			}

			//Loop to make sure that last verification ensures no collision with any object
			while (collisionDetected == true)
			{
				collisionDetected = false;
				verificationCount++;

				List<Pin> pinListCopy = new List<Pin>(pinList); //To avoid crashing when a pin is added while in the loop
				//Iterating throught all pins
				foreach (Pin fixedPin in pinListCopy)
				{
					//Getting the current position of the dropped pin
					double movedPin_X = this.getX();
					double movedPin_Y = this.getY();

					//Skipping collision-detection with itself
					if (fixedPin == this)
					{
						continue;
					}

					//Handling all special cases no matter what the movedPin is except if it just got added
					if (!ignoreSpecialCollisions)
					{
						bool skipCollisionDetection = HandleSpecialCollisions(fixedPin);

						if (skipCollisionDetection)
						{
							continue;
						}
					}

					//Getting the position of the pin with which the collision is being tested
					double fixedPin_X = fixedPin.getX();
					double fixedPin_Y = fixedPin.getY();

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
							if ((this.Width / 2) < movedPin_X && movedPin_X < (mapSection.Canvas_map.ActualWidth - (this.Width / 2)))
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
							if ((this.Height / 2) < movedPin_Y && movedPin_Y < (mapSection.Canvas_map.ActualHeight - (this.Height / 2)))
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
							if (movedPin_Y < (mapSection.Canvas_map.ActualHeight / 2))
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
							if (fixedPin_X < (mapSection.Canvas_map.ActualWidth / 2))
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
							double horizontalToBorder = Math.Min(fixedPin_X, (mapSection.Canvas_map.ActualWidth - fixedPin_X));
							double verticalToBorder = Math.Min(fixedPin_Y, (mapSection.Canvas_map.ActualHeight - fixedPin_Y));

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
						
						//Drop the rectangle after resolution of collision
						 setPinPosition(movedPin_X, movedPin_Y);
					}
				}
			}

			//Trigger after collision detection special tasks
			AfterCollisionDetection();
		}
		
		//Handling special cases of collision detection, default: nothing to handle, return false
		internal virtual bool HandleSpecialCollisions(Pin fixedPin)
		{
			return false;
		}

		//Handling cases where some pins need to trigger actions for the correct placement of all pins, default: do nothing
		internal virtual void AfterCollisionDetection()
		{
			return;
		}

        public static Pin MatchPin(Type type, int id)
        {
            foreach(Pin p in pinList)
            {
                if(p.GetType().Equals(type) && type.Equals(typeof(TeamPin)))
                {
                    if (((Team)p.getRelatedObject()).getID() == id)
                    {
                        return p;
                    }
                }
                else if (p.GetType().Equals(type) && type.Equals(typeof(InterventionPin)))
                {
                    if (((Intervention)p.getRelatedObject()).getID() == id)
                    {
                        return p;
                    }
                }
                else if (p.GetType().Equals(type) && type.Equals(typeof(EquipmentPin)))
                {
                    if (((Equipment)p.getRelatedObject()).getID() == id)
                    {
                        return p;
                    }
                }
                else if (p.GetType().Equals(type) && type.Equals(typeof(MapModPin)))
                {
                    if (((MapMod)p.getRelatedObject()).getID() == id)
                    {
                        return p;
                    }
                }
            }
            return null;
        }
	}
}
