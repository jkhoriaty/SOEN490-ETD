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
	public class InterventionPin : Pin, Observer
	{
		internal static int size = 40;//Sets the size of the intervention icon

		private Intervention intervention;
		private InterventionContainer interventionContainer;
		private GPSLocation gpsLocation;

		public InterventionPin(Intervention intervention, MapSectionPage mapSection) : base(intervention, mapSection, size)
		{
			this.intervention = intervention; //Providing a link to the team that this pin represents

			//Setting the image and text of the pin
			base.setImage(TechnicalServices.getImage("intervention"));
			base.setText(intervention.getInterventionNumber().ToString());

			//Setting the intervention pin  of all team pins that are intervening on this pin
			foreach(TeamPin teamPin in getInterveningTeamsPin())
			{
				teamPin.setInterventionPin(this);

				//Have the intervention track the position of a team intervening on it so that it moves instead of the team
				if(gpsLocation == null && teamPin.getTeam().getGPSLocation() != null)
				{
					gpsLocation = teamPin.getTeam().getGPSLocation();
					gpsLocation.RegisterInstanceObserver(this);
				}
			}

			//Register as an observer to the intervention instance so that any modification to it are reflected on the map, e.g. addition of a team
			intervention.RegisterInstanceObserver(this);
		}

		//Callback when the intervention instance has changed
		public void Update()
		{
			//If teams are assigned to this intervention draw the border
			if (intervention.getInterveningTeamList().Count > 0)
			{
				if (interventionContainer == null)
				{
					//Create the border if it doesn't exist yet
					interventionContainer = new InterventionContainer(this, mapSection);
					mapSection.Canvas_map.Children.Add(interventionContainer);
				}

				//Place all the pins in the appropriate place and make sure that it is not colliding with anything
				interventionContainer.PlaceAll();
				interventionContainer.CollisionDetectionAndResolution(false);
			}

			//Move the intervention pin using the GPS location of one of its intervening teams
			if(gpsLocation != null && GPSLocation.gpsConfigured == true)
			{
				setPinPosition(gpsLocation.getMapX(), gpsLocation.getMapY());
				CollisionDetectionAndResolution(false);
			}
		}

		//Removing this item as an observer, called when clearing the map before redrawing
		internal override void DeregisterPinFromObserver()
		{
			intervention.DeregisterInstanceObserver(this);
		}

		//Return the intervention that this pin represents
		internal Intervention getIntervention()
		{
			return intervention;
		}
		
		//Override default DragMove to add functionality
		public override void DragMove(Canvas Canvas_map, MouseEventArgs e)
		{
			//Get the position of the mouse relative to the Canvas
			var mousePosition = e.GetPosition(Canvas_map);

			//If it has an intervention container, keep it within bounds even when user tries to drag it outside of them
			if (interventionContainer != null)
			{
				if (mousePosition.X < (interventionContainer.Width / 2) || (Canvas_map.ActualWidth - (interventionContainer.Width / 2)) < mousePosition.X || mousePosition.Y < (this.Height / 2) || (Canvas_map.ActualHeight - interventionContainer.Height + (this.Height / 2)) < mousePosition.Y)
				{
					return;
				}
			}

			base.DragMove(Canvas_map, e);

			//Move the border and all the team pins along with the intervention pin
			if(interventionContainer != null)
			{
				interventionContainer.PlaceAll();
			}
		}

		//Make a list of the TeamPin of the teams intervening on this intervention
		internal List<TeamPin> getInterveningTeamsPin()
		{
			List<TeamPin> interveningTeamsPin = new List<TeamPin>();
			foreach(Team team in intervention.getInterveningTeamList())
			{
				foreach(Pin pin in pinList)
				{
					if(pin.relatedObject == team)
					{
						interveningTeamsPin.Add((TeamPin)pin);
					}
				}
			}
			return interveningTeamsPin;
		}

		//Get the intervention container related to this intervention
		internal InterventionContainer getInterventionContainer()
		{
			return interventionContainer;
		}

		//Handle special collisions between an InterventionPin and another pin
		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
			//SpecialCollision: Ignore collision with it's related InterventionContainer
			if(fixedPin == interventionContainer)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//Making sure that if the InterventionPin moved, that the boder and all it's related pins have moved as well
		internal override void AfterCollisionDetection()
		{
			if (interventionContainer != null)
			{
				interventionContainer.PlaceAll();
				interventionContainer.CollisionDetectionAndResolution(false);
			}
		}
	}
}
