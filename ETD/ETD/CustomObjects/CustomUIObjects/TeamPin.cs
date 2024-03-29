﻿using ETD.Models.ArchitecturalObjects;
using ETD.Models.Objects;
using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ETD.CustomObjects.CustomUIObjects
{
	class TeamPin : Pin, Observer
	{
		internal static int size = 40;//Sets the size of the team icon

		private MapSectionPage mapSection;

		private Team team;
		internal GPSLocation gpsLocation;
		private InterventionPin interventionPin;
		internal TeamPin parentPin;

		public TeamPin(Team team, MapSectionPage mapSection) : base(team, mapSection, size)
		{
			this.team = team; //Providing a link to the team that this pin represents
			this.mapSection = mapSection;
			//Updating image to match training and status of team in addition to text
			Update();

			//Creating a context menu for TeamPin objects
			MenuItem[] menuItems = new MenuItem[4];

			//Menu items for status change
			menuItems[0] = new MenuItem();
			menuItems[0].Uid = "MenuItem_TeamPin_Available";
			menuItems[0].Header = ETD.Properties.Resources.MenuItem_TeamPin_Available;

			menuItems[1] = new MenuItem();
			menuItems[1].Uid = "MenuItem_TeamPin_Moving";
			menuItems[1].Header = ETD.Properties.Resources.MenuItem_TeamPin_Moving;

			menuItems[2] = new MenuItem();
			menuItems[2].Uid = "MenuItem_TeamPin_Intervening";
			menuItems[2].Header = ETD.Properties.Resources.MenuItem_TeamPin_Intervening;

			menuItems[3] = new MenuItem();
			menuItems[3].Uid = "MenuItem_TeamPin_Unavailable";
			menuItems[3].Header = ETD.Properties.Resources.MenuItem_TeamPin_Unavailable;

			//Adding the method to be called when a menu item is clicked and adding the menuitem to the TeamPin context menu
			ContextMenu contextMenu = new ContextMenu();
			for (int i = 0; i < menuItems.Length; i++)
			{
				menuItems[i].Click += ChangeStatus_Click;
				contextMenu.Items.Add(menuItems[i]);
			}
			this.ContextMenu = contextMenu;

			//When the context menu is opened, check the appropriate status of the team
			this.ContextMenu.Opened += CheckCurrentStatus_Opened;

			//Register as an observer to the team instance that this pin represents
			team.RegisterInstanceObserver(this);

			//Register as an observer to the GPS locations so that it will be notified when the GPS locations are changed (team has moved)
			if(team.getGPSLocation() != null) //The team has been associated to GPS locations
			{
				gpsLocation = team.getGPSLocation(); //Getting a direct pointer for ease of access only
				gpsLocation.RegisterInstanceObserver(this);
			}
		}

		public TeamPin(Team team, MapSectionPage mapSection, TeamPin parentPin) : this(team, mapSection)
		{
			this.parentPin = parentPin;
		}

		//Callback when the team instance has changed
		public void Update()
		{
			//Redraw the background image and text
			base.Children.Clear();
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName()); //Although the text doesn't change, we have to redraw it for it not to be hidden by the image

			//Making the appropriate movement with the information provided by GPS
			if (this != draggedPin && team.getStatus() != Statuses.intervening && gpsLocation != null && GPSLocation.gpsConfigured == true && GPSServices.connectedToServer && gpsLocation.PhoneOnline())
			{
				setPinPosition(gpsLocation.getX(), gpsLocation.getY()); //Move the team
				CollisionDetectionAndResolution(true);

				//Updating arrow if any for it to point from the current teams position to its destination
				if (destinationArrowDictionnary.ContainsKey(team) && destinationArrowDictionnary[team] != null)
				{
					destinationArrowDictionnary[team].ChangeStart(getX(), getY());
				}
			}
		}

		//Removing this item as an observer, called when clearing the map before redrawing
		internal override void DeregisterPinFromObserver()
		{
			team.DeregisterInstanceObserver(this);
		}

		private string getMenuItemStatus(MenuItem menuItem)
		{
			string status = "";
			switch (menuItem.Uid.ToString())
			{
				case "MenuItem_TeamPin_Available":
					status = "available";
					break;
				case "MenuItem_TeamPin_Moving":
					status = "moving";
					break;
				case "MenuItem_TeamPin_Intervening":
					status = "intervening";
					break;
				case "MenuItem_TeamPin_Unavailable":
					status = "unavailable";
					break;
			}
			return status;
		}

		//Making sure the right item of the context menu is checked (it checks the actual status of the team)
		private void CheckCurrentStatus_Opened(object sender, RoutedEventArgs e)
		{
			foreach(MenuItem menuItem in this.ContextMenu.Items)
			{
				menuItem.IsChecked = team.getStatus().ToString().Equals(getMenuItemStatus(menuItem));
			}
		}

		//Change the status to what the user has chosen
		private void ChangeStatus_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
            team.setStatus(getMenuItemStatus(menuItem));

            //Clearing the arrow if the team got to where they are going
            if (getMenuItemStatus(menuItem).Equals("available") || getMenuItemStatus(menuItem).Equals("intervening"))
            {
                RemoveArrow();
            }

            //Handling the case when the user sets the status of a team to intervening
            if (getMenuItemStatus(menuItem).Equals("intervening"))
            {
                //Handling the case of when the user sets a team to intervening but it is not assigned to an intervention yet. Create the new intervention and assign the team to it.
                if (interventionPin == null)
                {
                    Intervention intervention = new Intervention();

                    //Getting references to the new team and intervention pins
                    TeamPin teamPin = null; //Need to get a reference to the new teamPin and not use "this" because the map has been redrawn upon the intervention creation, i.e. this TeamPin is outdated
                    InterventionPin relatedInterventionPin = null;
                    foreach (Pin pin in pinList)
                    {
                        if (pin.relatedObject == team)
                        {
                            teamPin = (TeamPin)pin;
                        }
                        if (pin.relatedObject == intervention)
                        {
                            relatedInterventionPin = (InterventionPin)pin;
                            break;
                        }
                    }

                    //Setting the position of the intervention to be the position of the team and calling collision detection on the teamPin for the team to be added to the intervention
                    relatedInterventionPin.setPinPosition(teamPin.getX(), teamPin.getY());
                    teamPin.CollisionDetectionAndResolution(false);
                    relatedInterventionPin.getIntervention().InterveningTeamArrived(team);
                }
                else
                {
                    //Set team as "In position" on the intervention
                    interventionPin.getIntervention().InterveningTeamArrived(team);
                    
                }
            }
		}

		//Moving the pin when the window is resized
		internal override void MovePin(double widthRatio, double heightRatio)
		{
			if(interventionPin == null) //Do not move pin if it is assigned to an intervention (it will be moved when the intervention will be moved
			{
				base.MovePin(widthRatio, heightRatio);
			}
		}

		//Return the team that this pin represents
		internal Team getTeam()
		{
			return team;
		}

		//Override the default DragStop method to add functionality to it
		public override void DragStop(Canvas Canvas_map, MouseButtonEventArgs e)
		{
			//Merging teams
			if(parentPin != null && SufficientOverlap(parentPin)) //Want to merge
			{
				Team.removeSplitTeam(team); //Delete the fragment
				if (gpsLocation != null)
				{
					gpsLocation.setTeamSplit(false); //Reactivationg gps
				}
				
				//Removing rogue arrows that might appear by movement of the teams or interventions while teams where split
				RemoveArrow();
				parentPin.RemoveArrow();
				if (interventionPin != null)
				{
					interventionPin.RemoveArrow();
				}

				mapSection.Update();
			}

			//Identifying whether the user wants to split the team
			Intervention splitIntervention = null;
			if (team.getStatus().ToString().Equals("intervening"))
			{
				foreach(Pin pin in pinList)
				{
					if(SufficientOverlap(pin) && pin.IsOfType("InterventionPin") && pin != interventionPin)
					{
						splitIntervention = ((InterventionPin)pin).getIntervention();
						break;
					}
				}
			}

			bool accidentalDrag = false;
			if (splitIntervention != null)
			{
				//Disabling GPS because of its unreliability when the team is split
				if(gpsLocation != null)
				{
					gpsLocation.setTeamSplit(true);
				}

				//Creating team as a duplicate of the initial team, name it using the number of the intervention it is assigned to
				Team team2 = new Team(team);
				team2.setName(team.getName() + splitIntervention.getInterventionNumber());
				splitIntervention.AddInterveningTeam(team2); //Assigning split team onto the second intervention
				mapSection.Update(); //Redrawing map so that the split team pin gets recreated with a pointer to the interventionPin it is assigned to
				return;
			}
			else if (interventionPin != null && interventionPin.getInterventionContainer() != null) //Handling the case when the team was in an intervention, choose between keeping it on the intervention (for an accidental drag-and-drop) or removing it from the intervention
			{
                if (SufficientOverlap(interventionPin.getInterventionContainer())) //Considered accidental drag-and-drop
                {
					accidentalDrag = true;
                    interventionPin.getInterventionContainer().PlaceAll();
                }
                else //Remove team from intervention
                {
                    interventionPin.getIntervention().RemoveInterveningTeam(team);
					interventionPin.SelectGPSLocation();
                    team.setStatus("unavailable");

                    //If it was the last team on that intervention and it has been removed, force redrawing of the map so that the InterventionContainer is removed
                    if (interventionPin.getInterveningTeamsPin().Count == 0)
                    {
                        mapSection.Update();
						return;
                    }

                    interventionPin = null;
                }
			}
			else if(team.getStatus().ToString().Equals("available")) //If the team was available and has been moved, set the team as moving
			{
				team.setStatus("moving");
			}

			base.DragStop(Canvas_map, e);

			//Handle situation when the TeamPin is tracked by GPS and the user moves it
			if (gpsLocation != null && GPSLocation.gpsConfigured == true && !accidentalDrag)
			{
				//Create and draw the arrow to the destination point and replace pin at current GPS position
				GPSPinDrop(GPSServices.connectedToServer && gpsLocation.PhoneOnline());
			}
		}

		internal void setInterventionPin(InterventionPin interventionPin)
		{
			this.interventionPin = interventionPin;
		}

		//Handle special collisions between a TeamPin and another pin
		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
            //SpecialCollision: Team is dropped on intervention, add team to intervention
            if (fixedPin.IsOfType("InterventionPin") && SufficientOverlap(fixedPin))
            {
                interventionPin = (InterventionPin)fixedPin;
                interventionPin.getIntervention().AddInterveningTeam(team);
				if(parentPin != null)//Making sure that the intervention count of the parent team accounts for the interventions that the team fragment intervenes on
				{
					parentPin.getTeam().incrementInterventionCount();
				}
                return true;
            }

            //SpecialCollision: Collision detection between team and intervention container
            if (fixedPin.IsOfType("InterventionContainer"))
            {
                if (interventionPin != null && interventionPin.getInterveningTeamsPin().Contains(this))
                {
                    return true;
                }
            }

            //If none of the conditions are true, return false
            return false;
        }
    }
}
