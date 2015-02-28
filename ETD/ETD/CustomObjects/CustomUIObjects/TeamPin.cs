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

			//Creating a context menu for TeamPins
			MenuItem[] menuItems = new MenuItem[4];

			menuItems[0] = new MenuItem();
			menuItems[0].Header = "Available";

			menuItems[1] = new MenuItem();
			menuItems[1].Header = "Moving";

			menuItems[2] = new MenuItem();
			menuItems[2].Header = "Intervening";

			menuItems[3] = new MenuItem();
			menuItems[3].Header = "Unavailable";

			ContextMenu contextMenu = new ContextMenu();
			for (int i = 0; i < menuItems.Length; i++)
			{
				menuItems[i].Click += ChangeStatus_Click;
				contextMenu.Items.Add(menuItems[i]);
			}

			this.ContextMenu = contextMenu;
			this.ContextMenu.Opened += CheckCurrentStatus_Opened;

			team.RegisterInstanceObserver(this);
		}

		public void Update()
		{
			base.Children.Clear();
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName());
		}

		//Making sure the right item of the context menu is checked (it checks the actual status of the team)
		private void CheckCurrentStatus_Opened(object sender, RoutedEventArgs e)
		{
			foreach(MenuItem menuItem in this.ContextMenu.Items)
			{
				menuItem.IsChecked = team.getStatus().ToString().Equals(menuItem.Header.ToString().ToLower());
			}
		}

		//Change the status to whatever the team has chosen
		private void ChangeStatus_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			team.setStatus(menuItem.Header.ToString().ToLower());

			if (menuItem.Header.ToString().ToLower().Equals("intervening"))
			{
				if(interventionPin == null)
				{
					Intervention intervention = new Intervention();

					//Getting references to the new team and and intervention pins
					TeamPin teamPin = null; //Need to get a reference to the teamPin and not use this because the map is being redrawn, i.e. new team pin for the same team, this has been cleared
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
					relatedInterventionPin.setPinPosition(teamPin.getX(), teamPin.getY());
					teamPin.CollisionDetectionAndResolution(mapSection.Canvas_map);
				}
				else
				{
					//TODO connect to hook to set team as "In position" on the intervention
				}
			}
		}

		internal Team getTeam()
		{
			return team;
		}

		public override void DragStop(Canvas Canvas_map, MouseButtonEventArgs e)
		{
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

					if(interventionPin.getInterveningTeamsPin().Count == 0)
					{
						mapSection.Update();
					}

					interventionPin = null;
				}
			}
			else if(team.getStatus().ToString().Equals("available"))
			{
				team.setStatus("moving");
			}

			base.DragStop(Canvas_map, e);
		}

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
