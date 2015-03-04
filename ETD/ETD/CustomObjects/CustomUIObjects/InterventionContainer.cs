using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ETD.CustomObjects.CustomUIObjects
{
	class InterventionContainer : Pin
	{
		private InterventionPin interventionPin;
		private Canvas Canvas_map;

		public InterventionContainer(InterventionPin interventionPin, Canvas Canvas_map) : base(interventionPin)
		{
			this.interventionPin = interventionPin;
			this.Canvas_map = Canvas_map;

			//Creating border that defines the edge of the container
			Border border = new Border();
			border.BorderThickness = new Thickness(1);
			border.CornerRadius = new CornerRadius(5);
			border.BorderBrush = new SolidColorBrush(Colors.Red);
			this.Children.Add(border);
		}

		//Places all the pins appropriately relatively to the InterventionPin
		public void PlaceAll()
		{
			//Determine the number of rows that the border is going to have with the interventionPin on top and then 2 teams per row
			int teamRows = 0;
			double height = ((double)interventionPin.getInterveningTeamsPin().Count) / 2;
			teamRows += (int)Math.Ceiling(height);
			this.Height = interventionPin.Height + (teamRows * TeamPin.size);

			//Determine the number of columns that the border is going to have, if more than one team than there will be two columns
			int columns = 1;
			if (interventionPin.getInterveningTeamsPin().Count() > 1)
			{
				columns = 2;
			}
			this.Width = columns * TeamPin.size;

			//Places a border around the intervention pin
			setBorderPosition(interventionPin.getX(), interventionPin.getY());

			double Y = interventionPin.getY() + (InterventionPin.size/2) + (TeamPin.size/2);
			for (int i = 0; i < interventionPin.getInterveningTeamsPin().Count; i++)
			{
				if ((i % 2) == 0) //First item on line
				{
					if ((i + 1) == interventionPin.getInterveningTeamsPin().Count) //Single on the line
					{
						interventionPin.getInterveningTeamsPin()[i].setPinPosition(interventionPin.getX(), Y);
					}
					else //There's another item on the line
					{
						interventionPin.getInterveningTeamsPin()[i].setPinPosition(interventionPin.getX() - (TeamPin.size / 2), Y);
					}
				}
				else //Second item on the line
				{
					interventionPin.getInterveningTeamsPin()[i].setPinPosition(interventionPin.getX() + (TeamPin.size / 2), Y);
					Y += TeamPin.size;
				}
			}
		}

		private void setBorderPosition(double interventionPin_X, double interventionPin_Y)
		{
			Canvas.SetLeft(this, interventionPin_X - (this.Width/2));
			Canvas.SetTop(this, interventionPin_Y - (interventionPin.Height/2));
		}

		//Handling special cases when collision detection is made on an InterventionContainer
		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
			//Those special cases occur when the border just got drawn but it went out of bouds left or right or at the bottom
			bool handled = false;

			if (this.getX() < (this.Width / 2)) //Left
			{
				interventionPin.setPinPosition(this.Width / 2, interventionPin.getY()); //Move it right
				handled = true;
			}
			if ((Canvas_map.ActualWidth - (this.Width / 2)) < this.getX()) //Right
			{
				interventionPin.setPinPosition(Canvas_map.ActualWidth - (this.Width / 2), interventionPin.getY()); //Move it left
				handled = true;
			}
			if ((Canvas_map.ActualHeight - (this.Height / 2)) < this.getY()) //Bottom
			{
				interventionPin.setPinPosition(interventionPin.getX(), Canvas_map.ActualHeight - this.Height + (interventionPin.Height / 2));
				handled = true;
			}

			if(handled)
			{
				PlaceAll(); //Replace all the team and border in accordance with the new intervention pin position
				CollisionDetectionAndResolution(false); //Recursive call to make sure that it does not collide with anything even the things it previously found no collision with
				return true;
			}

			//Ignore collision with all pins related to it
			if (fixedPin == interventionPin || interventionPin.getInterveningTeamsPin().Contains(fixedPin))
			{
				return true;
			}

			return false;
		}

		//Making sure that if the border moved, that all it's related pins have moved as well
		internal override void AfterCollisionDetection()
		{
			interventionPin.setPinPosition(getX(), getY() - (this.Height / 2) + (InterventionPin.size / 2));
			PlaceAll();
		}
	}
}
