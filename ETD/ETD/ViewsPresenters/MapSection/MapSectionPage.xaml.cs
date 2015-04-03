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
using ETD.Services;

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
	/// Interaction logic for MapSectionPage.xaml
	/// </summary>
	public partial class MapSectionPage : Page, Observer
	{
		MainWindow mainWindow;
        AdditionalInfoPage additionalInfo;

		//Drag-and-Drop related variable
		private bool pinDragInProgress;

        ImageBrush imgbrush = new ImageBrush();
        //ImageBrush original = new ImageBrush();
        internal String zoomLevel = "100%";
        bool isZoomed = false;

        double mouseX;
        double mouseY;
        double TTX;
        double TTY;

		public MapSectionPage(MainWindow mainWindow, AdditionalInfoPage additionalInfo)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
            this.additionalInfo = additionalInfo;

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

				//Redrawing arrow if the pin has one
				if (Pin.getPinArrow(team) != null && GPSServices.connectedToServer && teamPin.gpsLocation.PhoneOnline())
				{
					Pin.getPinArrow(team).DisplayArrow();
				}

				//Setting the pin to it's previous position, if it exists, or to the top-left corner
				bool ignoreSpecialCollisions = false;
				double[] previousPinPosition = Pin.getPreviousPinPosition(team);
				if (previousPinPosition == null)
				{
					ignoreSpecialCollisions = true;
					previousPinPosition = new double[]{ teamPin.Width / 2, teamPin.Height / 2 }; //Top-left corner
				}
				teamPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
				teamPin.CollisionDetectionAndResolution(ignoreSpecialCollisions);
			}

            //Creating team fragments when a team is split
            foreach (Team team in Team.getSplitTeamList())
            {
				TeamPin parentTeamPin = null;
				foreach(Team parentTeam in Team.getTeamList())
				{
					if(team.getID() == parentTeam.getID())
					{
						foreach(Pin pin in Pin.getPinList())
						{
							if(pin.relatedObject == parentTeam)
							{
								parentTeamPin = (TeamPin)pin;
								break;
							}
						}
						break;
					}
				}

                TeamPin teamPin = new TeamPin(team, this, parentTeamPin);
                Canvas_map.Children.Add(teamPin);

                //Setting the pin to it's previous position, if it exists, or to the top-left corner
                bool ignoreSpecialCollisions = false;
                double[] previousPinPosition = Pin.getPreviousPinPosition(team);
                if (previousPinPosition == null)
                {
                    ignoreSpecialCollisions = true;
                    previousPinPosition = new double[] { teamPin.Width / 2, teamPin.Height / 2 }; //Top-left corner
                }
                teamPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
                teamPin.CollisionDetectionAndResolution(ignoreSpecialCollisions);
            }

			//Creating all intervention pins and adding the map to their previous or a new position while detecting newly created collisions
			foreach (Intervention intervention in Intervention.getActiveInterventionList())
			{
				InterventionPin interventionPin = new InterventionPin(intervention, this);
				Canvas_map.Children.Add(interventionPin);

				//Redrawing arrow if the pin has one
				if (Pin.getPinArrow(intervention) != null && GPSServices.connectedToServer && interventionPin.gpsLocation != null && interventionPin.gpsLocation.PhoneOnline())
				{
					Pin.getPinArrow(intervention).DisplayArrow();
				}

				//Setting the pin to it's previous position, if it exists, or to the top-left corner
				bool ignoreSpecialCollisions = false;
				double[] previousPinPosition = Pin.getPreviousPinPosition(intervention);
				if (previousPinPosition == null)
				{
					ignoreSpecialCollisions = true;
					previousPinPosition = new double[] { (interventionPin.Width / 2), Canvas_map.ActualHeight - (interventionPin.Height / 2) }; //Bottom-right corner
				}
				interventionPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
				interventionPin.Update();
				interventionPin.CollisionDetectionAndResolution(ignoreSpecialCollisions);
			}

			//Creating all equipment pins and adding the map to their previous or a new position while detecting newly created collisions
			foreach (Equipment equipment in Equipment.getEquipmentList())
			{
				if (!equipment.IsAssigned())
				{
					EquipmentPin equipmentPin = new EquipmentPin(equipment, this);
					Canvas_map.Children.Add(equipmentPin);

					//Setting the pin to it's previous position, if it exists, or to the top-left corner
					bool ignoreSpecialCollisions = false;
					double[] previousPinPosition = Pin.getPreviousPinPosition(equipment);
					if (previousPinPosition == null)
					{
						ignoreSpecialCollisions = true;
						previousPinPosition = new double[] { Canvas_map.ActualWidth - (equipmentPin.Width / 2), (equipmentPin.Height / 2) }; //Top-right corner
					}
					equipmentPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
					equipmentPin.CollisionDetectionAndResolution(ignoreSpecialCollisions);
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

		
		/*Upon right clicking, stores the mouse position so it knows where to zoom.
         * This also displays one of two context menus based on whether or not the 
         * map is already in a zoomed-in state. I.e. if it's already zoomed in, your
         * only option is to zoom out before zooming in again.*/
        private void Map_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point P = Mouse.GetPosition(Canvas_map);
            mouseX = P.X;
            mouseY = P.Y;

            if (isZoomed == false)
            {
                this.ContextMenu = this.Resources["ZoomContextMenuDefault"] as ContextMenu;
            }
            else
            {
                this.ContextMenu = this.Resources["ZoomContextMenuZoomed"] as ContextMenu;
            }
        }
		

        /*Called from the context menu on the map page
         *Reads in the value selected from the options and
         *scales the map by that amount zooming in on the location
         *of the mouse as the new center.*/
		public void Zoom_Click(object sender, EventArgs e)
        {
            imgbrush = (ImageBrush)additionalInfo.AdditionalMap.Background;

            MenuItem mi = (MenuItem)sender;
            zoomLevel = (String)mi.Header;
            switch(zoomLevel)
            {
                case "100%":
                    ScaleMap(1);
                    isZoomed = false;
                    break;
                case "120%":
                    ScaleMap(1.2);
                    isZoomed = true;
                    break;
                case "140%":
                    ScaleMap(1.4);
                    isZoomed = true;
                    break;
                case "160%":
                    ScaleMap(1.6);
                    isZoomed = true;
                    break;
                case "180%":
                    ScaleMap(1.8);
                    isZoomed = true;
                    break;
                case "200%":
                    ScaleMap(2);
                    isZoomed = true;
                    break;
            }
        }
		
        /*This is called to reset the map back to its original
         * 1.0 scale as well as centering it about its original
         * middle.*/
		public void ScaleMapDefault()
        {
            ScaleTransform ST = new ScaleTransform();
            ST.ScaleX = 1;
            ST.ScaleY = 1;
            imgbrush.RelativeTransform = ST;

            TranslateTransform TT;
            TT = new TranslateTransform(-TTX, -TTY);

            imgbrush.ClearValue(ImageBrush.TransformProperty);

            this.Update();
        }

        /*Scales the map based on the chosen ration and centers
         * it about the location of the mouse when the context menu
         * was first opened.*/
        public void ScaleMap(double ratio)
        {
            ScaleMapDefault();
            if (ratio != 1)
            {
                Pin.ClearAllPins(Canvas_map);

                ScaleTransform ST = new ScaleTransform();
                ST.ScaleX = ratio;
                ST.ScaleY = ratio;
                imgbrush.RelativeTransform = ST;

                TranslateTransform TT;

                TTX = -mouseX * ratio + Canvas_map.ActualWidth / 2;
				TTY = -mouseY * ratio + Canvas_map.ActualHeight / 2;

                TT = new TranslateTransform(TTX, TTY);
                imgbrush.Transform = TT;
            }
        }
	}
}
