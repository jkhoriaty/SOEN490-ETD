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
		private List<TeamPin> interveningTeamPinList;

		public InterventionContainer(InterventionPin interventionPin) : base(interventionPin)
		{
			this.interventionPin = interventionPin;
			interveningTeamPinList = interventionPin.getInterveningTeamsPin();
			interventionPin.setInterventionContainer(this);

			//Determine the number of rows that the border is going to have with the interventionPin on top and then 2 teams per row
			int teamRows = 0;
			double height = ((double)interveningTeamPinList.Count()) / 2;
			teamRows += (int)Math.Ceiling(height);
			this.Height = interventionPin.Height + (teamRows * TeamPin.size);

			//Determine the number of columns that the border is going to have, if more than one team than there will be two columns
			int columns = 1;
			if (interveningTeamPinList.Count() > 1)
			{
				columns = 2;
			}
			this.Width = columns * TeamPin.size;

			Border border = new Border();
			border.BorderThickness = new Thickness(1);
			border.CornerRadius = new CornerRadius(5);
			border.BorderBrush = new SolidColorBrush(Colors.Red);
			this.Children.Add(border);
		}

		public void PlaceAll()
		{
			setBorderPosition(interventionPin.getX(), interventionPin.getY());

			double Y = interventionPin.getY() + (InterventionPin.size/2) + (TeamPin.size/2);
			for (int i = 0; i < interveningTeamPinList.Count; i++)
			{
				if ((i % 2) == 0) //First item on line
				{
					if ((i + 1) == interveningTeamPinList.Count) //Single on the line
					{
						interveningTeamPinList[i].setPinPosition(interventionPin.getX(), Y);
					}
					else //There's another team on the line
					{
						interveningTeamPinList[i].setPinPosition(interventionPin.getX() - (TeamPin.size / 2), Y);
					}
				}
				else //Second item on the line
				{
					interveningTeamPinList[i].setPinPosition(interventionPin.getX() + (TeamPin.size / 2), Y);
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
			//Ignore collision with all pins related to it
			if(fixedPin == interventionPin || interveningTeamPinList.Contains(fixedPin))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//Making sure that if the border moved, that all it's related pins have moved as well
		internal override void AfterCollisionDetection(Canvas Canvas_map)
		{
			interventionPin.setPinPosition(getX(), getY() - (this.Height / 2) + (InterventionPin.size / 2));
			PlaceAll();
		}
	}
}
