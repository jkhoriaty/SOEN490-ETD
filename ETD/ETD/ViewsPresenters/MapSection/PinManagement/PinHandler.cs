using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD.ViewsPresenters.MapSection.PinManagement
{
	class PinHandler
	{
		private MapSectionPage mapSection;
        private AdditionalInfoPage AIPmap;
		private bool _isRectDragInProg;
		private Grid movingGrid;
		private Grid relatedIntervention;
		private double previousX;
		private double previousY;
		private Dictionary<Grid, List<Grid>> activeTeams = new Dictionary<Grid, List<Grid>>();
		private Dictionary<Grid, InterventionContainer> interventionBorders = new Dictionary<Grid, InterventionContainer>();

		public PinHandler(MapSectionPage mapSection)
		{
			this.mapSection = mapSection;
		}

        public PinHandler(AdditionalInfoPage AIP)
        {
            this.AIPmap = AIP;
        }

		//Setting position of pin
		public void setPinPosition(Grid g, double X, double Y)
		{
			Canvas.SetLeft(g, (X - (g.Width / 2)));
			Canvas.SetTop(g, (Y - (g.Height / 2)));
		}

		public double getX(Grid g)
		{
			return Math.Round((double)Canvas.GetLeft(g) + (g.Width / 2), 3);
		}

		public double getY(Grid g)
		{
			return Math.Round((double)Canvas.GetTop(g) + (g.Height / 2), 3);
		}

		public void DragStart(object sender, MouseButtonEventArgs e)
		{
			Grid g = (Grid)sender;

			_isRectDragInProg = g.CaptureMouse();
			movingGrid = g;

			if(RelatedInterventionBorder(g) != null)
			{
				relatedIntervention = RelatedInterventionBorder(g);
				previousX = getX(g);
				previousY = getY(g);
			}
		}

		//Left Mouse Button Up: Any pin
		internal void DragStop(object sender, MouseButtonEventArgs e)
		{/*
			Grid g = (Grid)sender;

			//Avoid in having method called on object being collided with
			if (g != movingGrid)
			{
				return;
			}

			g.ReleaseMouseCapture();
			_isRectDragInProg = false;

			var mousePos = e.GetPosition(mapSection.Map);

			if(g.Tag.Equals("team"))
			{
				TeamGrid team = (TeamGrid)g;
                if (team.GetStatus() != "intervening")
				    team.ChangeStatus("moving");
			}

			//If the pin dragged is a team that was in an intervention
			if(relatedIntervention != null)
			{
				//If it is kept within the bounds of the intervention (accident drag), put it back in it's place, else remove it from the intervention and treat it any other pin
				if (getX(g) > (getX(relatedIntervention) - ((g.Width / 2) + (relatedIntervention.Width / 2))) && getX(g) < (getX(relatedIntervention) + ((g.Width / 2) + (relatedIntervention.Width / 2))) && getY(g) > (getY(relatedIntervention) - ((g.Height / 2) + (relatedIntervention.Height / 2))) && getY(g) < (getY(relatedIntervention) + ((g.Height / 2) + (relatedIntervention.Height / 2))))
				{
					setPinPosition(g, previousX, previousY);
				}
				else
				{
					//Remove team from active team list and redraw border
					foreach(KeyValuePair<Grid, BorderGrid> interventionBorderPair in interventionBorders)
					{
						if(interventionBorderPair.Value == relatedIntervention)
						{
							activeTeams[interventionBorderPair.Key].Remove(g);
							DrawInterventionBorder(interventionBorderPair.Key);
							break;
						}
					}
					DetectCollision(g, mousePos.X, mousePos.Y);
				}
			}
			else
			{
				//Calling collision detection and resolution for the dropped object
				DetectCollision(g, mousePos.X, mousePos.Y);
			}

			//Resetting values
			relatedIntervention = null;
			previousX = -1;
			previousY = -1;*/
		}

		//Mouse Move
		internal void DragMove(object sender, MouseEventArgs e)
		{/*
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

			//If it's an active intervention, keep the whole border with all the teams within the map space
			if (interventionBorders.ContainsKey(g))
			{
				if (mousePos.X < (interventionBorders[g].Width / 2) || (mapSection.Map.ActualWidth - (interventionBorders[g].Width / 2)) < mousePos.X || mousePos.Y < (g.Height / 2) || (mapSection.Map.ActualHeight - interventionBorders[g].Height + (g.Height / 2)) < mousePos.Y)
				{
					return;
				}
			}

			setPinPosition(g, mousePos.X, mousePos.Y);

			//If it's an active intervention, move the border and all the teams with it
			if(interventionBorders.ContainsKey(g))
			{
				setPinPosition(interventionBorders[g], getX(g), Canvas.GetTop(g) + (interventionBorders[g].Height / 2));
				PlaceInterventionPins(g);
			}*/
		}

		public void DetectCollision(Grid movedPin, double movedPin_X, double movedPin_Y)
		{/*
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
			Grid intervention = null;

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
					if (fixedPin == movedPin)
					{
						continue;
					}

					//If collision detection is being done on an intervention border, ignore collision with all pins related to it
					bool related = false;
					if(movedPin.Tag.Equals("border"))
					{
						foreach(KeyValuePair<Grid, BorderGrid> interventionBorderPair in interventionBorders)
						{
							if(interventionBorderPair.Value == movedPin && (activeTeams[interventionBorderPair.Key].Contains(fixedPin) || fixedPin == interventionBorderPair.Key))
							{
								intervention = interventionBorderPair.Key;
								related = true;
							}
						}
					}

					if(movedPin.Tag.Equals("intervention") && fixedPin.Tag.Equals("border") && interventionBorders.ContainsKey(movedPin) && interventionBorders[movedPin] == fixedPin)
					{
						related = true;
					}
					
					if(related)
					{
						continue;
					}

					//Getting the position of where the rectangle has been dropped (center point)
					double fixedPin_X = getX(fixedPin);
					double fixedPin_Y = getY(fixedPin);

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
						//Add the team to the intervention, create or change the intervention border appropriately
						if (!interventionBorders.ContainsKey(fixedPin))
						{
							BorderGrid borderGrid = new BorderGrid(fixedPin.Name, fixedPin.Width, fixedPin.Height + movedPin.Height);
							mapSection.Map.Children.Add(borderGrid);
							interventionBorders.Add(fixedPin, borderGrid);
							activeTeams.Add(fixedPin, new List<Grid>());
						}
						activeTeams[fixedPin].Add(movedPin);
						DrawInterventionBorder(fixedPin);
						mapSection.AddResource(movedPin.Name, fixedPin.Name);
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

			//Drop the rectangle if there are not collision or after resolution of collision
			setPinPosition(movedPin, movedPin_X, movedPin_Y);

			//If the border has moved, replace all items back in the border
			if(movedPin.Tag.Equals("border") && intervention != null)
			{
				setPinPosition(intervention, movedPin_X, movedPin_Y - (movedPin.Height / 2) + (intervention.Height / 2));
				PlaceInterventionPins(intervention);
			}

			if(interventionBorders.ContainsKey(movedPin))
			{
				DetectCollision(interventionBorders[movedPin], movedPin_X, movedPin_Y - (movedPin.Height / 2) + (interventionBorders[movedPin].Height / 2));
			}*/
		}

		public void movePins(double widthRatio, double heightRatio)
		{
			/*var allPins = mapSection.Map.Children.OfType<Grid>().ToList();

			foreach (var pin in allPins)
			{
				double movedPin_X = widthRatio * getX(pin);
				double movedPin_Y = heightRatio * getY(pin);

				setPinPosition(pin, movedPin_X, movedPin_Y);
				DetectCollision(pin, movedPin_X, movedPin_Y);
			}*/
		}

		private void PlaceInterventionPins(Grid fixedPin)
		{
			double Y = getY(fixedPin) + fixedPin.Height;
			for(int i = 0; i < activeTeams[fixedPin].Count; i++)
			{
				if((i % 2) == 0) //First item on line
				{
					if ((i + 1) == activeTeams[fixedPin].Count) //Single on the line
					{
						setPinPosition(activeTeams[fixedPin][i], getX(fixedPin), Y);
					}
					else //There's another team on the line
					{
						setPinPosition(activeTeams[fixedPin][i], Canvas.GetLeft(fixedPin), Y);
					}
				}
				else //Second item on the line
				{
					setPinPosition(activeTeams[fixedPin][i], Canvas.GetLeft(fixedPin) + fixedPin.Width, Y);
					Y += activeTeams[fixedPin][i].Height;
				}
			}
		}

		//Handles drawing an intervention border, adding all teams to it and detecting collision on it
		private void DrawInterventionBorder(Grid fixedPin)
		{/*
			//Determine the number of rows and number of columns of items that the border is going to have
			int rows = 1;
			int columns = 1;

			double height = ((double)activeTeams[fixedPin].Count()) / 2;
			rows += (int)Math.Ceiling(height);
			if (activeTeams[fixedPin].Count() > 1)
			{
				columns = 2;
			}
			//Adjusting the borders' size according to the number of rows and columns
			interventionBorders[fixedPin].setWidth(columns * fixedPin.Width);
			interventionBorders[fixedPin].setHeight(rows * fixedPin.Height);

			//Setting all the objects in the correct position
			double border_X = getX(fixedPin);
			double border_Y = Canvas.GetTop(fixedPin) + (interventionBorders[fixedPin].Height / 2);

			//Replacing border within horizontal bounds
			if (border_X > (mapSection.Map.ActualWidth - (interventionBorders[fixedPin].Width / 2))) //Right
			{
				border_X = mapSection.Map.ActualWidth - (interventionBorders[fixedPin].Width / 2);
			}
			else if (border_X < (interventionBorders[fixedPin].Width / 2)) //Left
			{
				border_X = (interventionBorders[fixedPin].Width / 2);
			}

			//Replacing border within vertical bounds
			if (border_Y > (mapSection.Map.ActualHeight - (interventionBorders[fixedPin].Height / 2))) //Bottom
			{
				border_Y = mapSection.Map.ActualHeight - (interventionBorders[fixedPin].Height / 2);
			}
			else if (border_Y < (interventionBorders[fixedPin].Height / 2)) //Top
			{
				border_Y = (interventionBorders[fixedPin].Height / 2);
			}

			//Adjusting the border and intervention pin if the border has been moved back to within the bounds
			setPinPosition(interventionBorders[fixedPin], border_X, border_Y);
			setPinPosition(fixedPin, border_X, border_Y - (interventionBorders[fixedPin].Height / 2) + (fixedPin.Height / 2));

			//Populating the border with the teams affected to the intervention
			PlaceInterventionPins(fixedPin);
			DetectCollision(interventionBorders[fixedPin], border_X, border_Y);*/
		}

		internal Grid RelatedInterventionBorder(Grid team)
		{/*
			foreach(KeyValuePair<Grid, BorderGrid> interventionBorderPair in interventionBorders)
			{
				if(activeTeams[interventionBorderPair.Key].Contains(team))
				{
					return interventionBorderPair.Value;
				}
			}*/
			return null;
		}

		internal Grid RelatedIntervention(Grid team)
		{/*
			foreach (KeyValuePair<Grid, BorderGrid> interventionBorderPair in interventionBorders)
			{
				if (activeTeams[interventionBorderPair.Key].Contains(team))
				{
					return interventionBorderPair.Key;
				}
			}*/
			return null;
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {/*
            foreach(KeyValuePair<Grid, List<Grid>> intervention in activeTeams)
            {
                if (intervention.Key.Name.Equals(interventionName))
                {
                    TeamGrid team = (TeamGrid)intervention.Value[rowNumber];
                    team.ChangeStatus("intervening");

                    return;
                }
            }*/
        }

        internal void AppointTeamToIntervention(Grid team, Grid intervention)
        {
            setPinPosition(intervention, getX(team), getY(team));
            DetectCollision(team, getX(team), getY(team));
        }
	}
}
