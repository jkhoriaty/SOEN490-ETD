using ETD.Models.Objects;
using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
	/// Interaction logic for MapSectionPage.xaml
	/// </summary>
	public partial class MapSectionPage : Page, Observer
	{
		MainWindow mainWindow;

		//Drag-and-Drop related variable
		private bool pinDragInProgress;
		private Pin draggedPin;

        internal String zoomLevel = "100%";

        //Creates a map section page
		public MapSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

            //Registers itself to the list of observers
			Observable.RegisterClassObserver(typeof(Team), this);
			Observable.RegisterClassObserver(typeof(Intervention), this);
			Observable.RegisterClassObserver(typeof(Equipment), this);
		}


		//Callback when any of the observed objects modified i.e. creation and addition of all pins (including new pins, excluding deleted pins)
		public void Update()
		{
			Pin.ClearAllPins(Canvas_map); //Clearing all pins from the map

			//Creating all team pins and adding the map to their previous or a new position while detecting newly created collisions
			foreach(Team team in Team.getTeamList())
			{
				TeamPin teamPin = new TeamPin(team, this);
				Canvas_map.Children.Add(teamPin);

				//Setting the pin to it's previous position, if it exists, or to the top-left corner
				bool defaultPosition = false;
				double[] previousPinPosition = Pin.getPreviousPinPosition(team);
				if (previousPinPosition == null)
				{
					defaultPosition = true;
					previousPinPosition = new double[]{ teamPin.Width / 2, teamPin.Height / 2 }; //Top-left corner
				}
				teamPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
				teamPin.CollisionDetectionAndResolution(defaultPosition);
			}

			//Creating all intervention pins and adding the map to their previous or a new position while detecting newly created collisions
			foreach (Intervention intervention in Intervention.getActiveInterventionList())
			{
				InterventionPin interventionPin = new InterventionPin(intervention, this);
				Canvas_map.Children.Add(interventionPin);

				//Setting the pin to it's previous position, if it exists, or to the top-left corner
				bool defaultPosition = false;
				double[] previousPinPosition = Pin.getPreviousPinPosition(intervention);
				if (previousPinPosition == null)
				{
					defaultPosition = true;
					previousPinPosition = new double[] { (interventionPin.Width / 2), Canvas_map.ActualHeight - (interventionPin.Height / 2) }; //Bottom-right corner
				}
				interventionPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
				interventionPin.Update();
				interventionPin.CollisionDetectionAndResolution(defaultPosition);
			}

			//Creating all equipment pins and adding the map to their previous or a new position while detecting newly created collisions
			foreach (Equipment equipment in Equipment.getEquipmentList())
			{
				if (!equipment.IsAssigned())
				{
					EquipmentPin equipmentPin = new EquipmentPin(equipment, this);
					Canvas_map.Children.Add(equipmentPin);

					//Setting the pin to it's previous position, if it exists, or to the top-left corner
					bool defaultPosition = false;
					double[] previousPinPosition = Pin.getPreviousPinPosition(equipment);
					if (previousPinPosition == null)
					{
						defaultPosition = true;
						previousPinPosition = new double[] { Canvas_map.ActualWidth - (equipmentPin.Width / 2), (equipmentPin.Height / 2) }; //Top-right corner
					}
					equipmentPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
					equipmentPin.CollisionDetectionAndResolution(defaultPosition);
				}
			}
		}
		
		//Drag starting when mouse left button is clicked
		internal void DragStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Pin pin = (Pin)sender;
			pinDragInProgress = pin.CaptureMouse();
			pin.DragStart();
		}

		//Drag ongoing when mouse is moving and left mouse button is clicked
		internal void DragMove_MouseMove(object sender, MouseEventArgs e)
		{
			//If left mouse button is not clicked exit method
			if(!pinDragInProgress)
			{
				return;
			}

			Pin pin = (Pin)sender;
			pin.DragMove(Canvas_map, e);
		}

		//Pin dropped (when left mouse button is released)
		internal void DragStop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Pin pin = (Pin)sender;
			pinDragInProgress = false;
			pin.DragStop(Canvas_map, e);
		}

		internal void ReportArrival(Grid team)
		{
			/*Grid intervention = pinHandler.RelatedIntervention(team);
			if(intervention == null)
			{
                mainWindow.CreateIntervention();
                //pinHandler.AppointTeamToIntervention(team, (Grid) Map.Children[Map.Children.Count - 1]);
                intervention = pinHandler.RelatedIntervention(team);
			}
			mainWindow.ReportArrival(team.Name, intervention.Name);*/
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
			/*
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
	}
}
