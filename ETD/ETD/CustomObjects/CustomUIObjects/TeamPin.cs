using ETD.Models.ArchitecturalObjects;
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
		internal static int size = 40;

		private Team team;
		private MapSectionPage mapSection;
		private InterventionPin interventionPin;

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
			menuItems[0].Header = "Available";

			menuItems[1] = new MenuItem();
			menuItems[1].Header = "Moving";

			menuItems[2] = new MenuItem();
			menuItems[2].Header = "Intervening";

			menuItems[3] = new MenuItem();
			menuItems[3].Header = "Unavailable";

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
		}

		//Callback when the team instance has changed
		public void Update()
		{
			//Redraw the background image and text
			base.Children.Clear();
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName()); //Although the text doesn't change, we have to redraw it for it not to be hidden by the image
		}

		//Removing this item as an observer, called when clearing the map before redrawing
		internal override void DeregisterPinFromObserver()
		{
			team.DeregisterInstanceObserver(this);
		}

		//Making sure the right item of the context menu is checked (it checks the actual status of the team)
		private void CheckCurrentStatus_Opened(object sender, RoutedEventArgs e)
		{
			foreach(MenuItem menuItem in this.ContextMenu.Items)
			{
				menuItem.IsChecked = team.getStatus().ToString().Equals(menuItem.Header.ToString().ToLower());
			}
		}

		//Change the status to what the user has chosen
		private void ChangeStatus_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			team.setStatus(menuItem.Header.ToString().ToLower());

			//Handling the case when the user sets the status of a team to intervening
			if (menuItem.Header.ToString().ToLower().Equals("intervening"))
			{
				//Handling the case of when the user sets a team to intervening but it is not assigned to an intervention yet. Create the new intervention and assign the team to it.
				if(interventionPin == null)
				{
					Intervention intervention = new Intervention();

					//Getting references to the new team and intervention pins
					TeamPin teamPin = null; //Need to get a reference to the new teamPin and not use "this" because the map has been redrawn upon the intervention creation, i.e. this TeamPin is outdated
					InterventionPin relatedInterventionPin = null;
					foreach (Pin pin in pinList)
					{
						if(pin.relatedObject == team)
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
					teamPin.CollisionDetectionAndResolution(mapSection.Canvas_map);
				}
				else
				{
					//TODO connect to hook to set team as "In position" on the intervention
				}
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
			//Handling the case when the team was in an intervention, choose between keeping it on the intervention (for an accidental drag-and-drop) or removing it from the intervention
			if (interventionPin != null && interventionPin.getInterventionContainer() != null)
			{
				if(SufficientOverlap(interventionPin.getInterventionContainer())) //Considered accidental drag-and-drop
				{
					interventionPin.getInterventionContainer().PlaceAll();
				}
				else //Remove team from intervention
				{
					interventionPin.getIntervention().RemoveTeam(team);
					team.setStatus("unavailable");

					//If it was the last team on that intervention and it has been removed, force redrawing of the map so that the InterventionContainer is removed
					if(interventionPin.getInterveningTeamsPin().Count == 0)
					{
						mapSection.Update();
					}

					interventionPin = null;
				}
			}
			else if(team.getStatus().ToString().Equals("available")) //If the team was available and has been moved, set the team as moving
			{
				team.setStatus("moving");
			}

			base.DragStop(Canvas_map, e);
		}

		//Handle special collisions between a TeamPin and another pin
		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
			//SpecialCollision: Team is dropped on intervention, add team to intervention
			if(fixedPin.IsOfType("InterventionPin") && SufficientOverlap(fixedPin))
			{
				interventionPin = (InterventionPin)fixedPin;
				interventionPin.getIntervention().AddTeam(team);
				return true;
			}

			//SpecialCollsion: Collision detection between team and intervention container
			if(fixedPin.IsOfType("InterventionContainer"))
			{
				if(interventionPin != null && interventionPin.getInterveningTeamsPin().Contains(this))
				{
					return true;
				}
			}

			//If none of the conditions are true, return false
			return false;
		}
	}
}
