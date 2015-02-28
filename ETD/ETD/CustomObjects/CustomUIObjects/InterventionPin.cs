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
	class InterventionPin : Pin, Observer
	{
		internal static int size = 40;

		private Intervention intervention;
		private InterventionContainer interventionContainer;
		private MapSectionPage mapSection;

		public InterventionPin(Intervention intervention, MapSectionPage mapSection) : base(intervention, mapSection, size)
		{
			this.intervention = intervention; //Providing a link to the team that this pin represents
			this.mapSection = mapSection; //Keeping pointer to draw a InterventionContainer when a team is added

			base.setImage(TechnicalServices.getImage("intervention"));
			base.setText(intervention.getInterventionNumber().ToString());

			intervention.RegisterInstanceObserver(this);
		}

		public void Update()
		{
			if (intervention.getInterveningTeamList().Count > 0)
			{
				if (interventionContainer == null)
				{
					interventionContainer = new InterventionContainer(this, mapSection.Canvas_map);
					mapSection.Canvas_map.Children.Add(interventionContainer);
				}
				interventionContainer.PlaceAll();
				interventionContainer.CollisionDetectionAndResolution(mapSection.Canvas_map);
			}
		}

		internal Intervention getIntervention()
		{
			return intervention;
		}

		public override void DragMove(Canvas Canvas_map, MouseEventArgs e)
		{
			//Get the position of the mouse relative to the Canvas
			var mousePosition = e.GetPosition(Canvas_map);

			//Keep the object within bounds even when user tries to drag it outside of them
			if (interventionContainer != null)
			{
				if (mousePosition.X < (interventionContainer.Width / 2) || (Canvas_map.ActualWidth - (interventionContainer.Width / 2)) < mousePosition.X || mousePosition.Y < (this.Height / 2) || (Canvas_map.ActualHeight - interventionContainer.Height + (this.Height / 2)) < mousePosition.Y)
				{
					return;
				}
			}

			base.DragMove(Canvas_map, e);

			if(interventionContainer != null)
			{
				interventionContainer.PlaceAll();
			}
		}

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

		internal InterventionContainer getInterventionContainer()
		{
			return interventionContainer;
		}

		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
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
		internal override void AfterCollisionDetection(Canvas Canvas_map)
		{
			if (interventionContainer != null)
			{
				interventionContainer.PlaceAll();
				interventionContainer.CollisionDetectionAndResolution(Canvas_map);
			}
		}
	}
}
