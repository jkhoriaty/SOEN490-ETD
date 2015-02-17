using ETD.Models.Objects;
using ETD.Models.ArchitecturalObjects;
using ETD.ViewsPresenters.MapSection.PinManagement;
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
using ETD.Models.CustomUIObjects;

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
	/// Interaction logic for MapSectionPage.xaml
	/// </summary>
	public partial class MapSectionPage : Page, Observer
	{
		MainWindow mainWindow;
		PinEditor pinEditor;
		PinHandler pinHandler;

		Dictionary<String, double[]> teamPinPositionList = new Dictionary<String, double[]>();

        ImageBrush imgbrush;
        internal String zoomLevel = "100%";

        double mouseX;
        double mouseY;
        double TTX;
        double TTY;

		public MapSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			pinEditor = new PinEditor(this);
			pinHandler = new PinHandler(this);
            Canvas_map.ContextMenu = Resources["ContextMenu"] as ContextMenu;

			Team.RegisterObserver(this);
		}

		//Loading of map as a result to the user clicking the "Load Map" button
		public void SetMap(BitmapImage coloredImage)
		{
			//Making the picture grayscale
			FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
			grayBitmap.BeginInit();
			grayBitmap.Source = coloredImage;
			grayBitmap.DestinationFormat = PixelFormats.Gray8;
			grayBitmap.EndInit();

			//Displaying the map as the background
            imgbrush = new ImageBrush(grayBitmap);
			Canvas_map.Background = imgbrush;
		}

		//Callback when Team object modified
		public void ObservedObjectUpdated()
		{
			Canvas_map.Children.Clear();
			foreach(Team team in Team.getTeamList())
			{
				TeamPin teamPin = new TeamPin(team, this);
				Canvas_map.Children.Add(teamPin);

				//Setting the pin to it's previous position, if it exists, or to the top-left corner
				if(!teamPinPositionList.ContainsKey(team.getName()))
				{
					double[] teamPinCoordinates = { teamPin.Width / 2, teamPin.Height / 2 };
					teamPinPositionList.Add(team.getName(), teamPinCoordinates);
				}
				teamPin.setPinPosition(teamPinPositionList[team.getName()][0], teamPinPositionList[team.getName()][0]);
				teamPin.DetectCollision(Canvas_map);
			}
		}

		public void CreateEquipmentPin(String equipmentName)
		{
			pinEditor.CreateEquipmentPin(equipmentName);
		}

		public void CreateInterventionPin(int interventionNumber)
		{
			pinEditor.CreateInterventionPin(interventionNumber);
		}

		public void DeletePin(String pinName)
		{
			pinEditor.DeletePin(pinName);
		}
		public void SetPinPosition(Grid g, double X, double Y)
		{
			pinHandler.setPinPosition(g, X, Y);
		}
		
		internal void DragStart(object sender, MouseButtonEventArgs e)
		{
			pinHandler.DragStart(sender, e);
		}

		internal void DragStop(object sender, MouseButtonEventArgs e)
		{
			pinHandler.DragStop(sender, e);
		}

		internal void DragMove(object sender, MouseEventArgs e)
		{
			pinHandler.DragMove(sender, e);
		}

		public void DetectCollision(Grid movedItem, double movedItem_X, double movedItem_Y)
		{
			pinHandler.DetectCollision(movedItem, movedItem_X, movedItem_Y);
		}

		//Pushing request to mainWindow
		public void AddTeamEquipment(String equip, String teamName)
		{
            Equipment equipment = new Equipment((Equipments)Enum.Parse(typeof(Equipments), equip));
			mainWindow.AddTeamEquipment(equipment, teamName);
		}

        internal void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            pinEditor.ChangeStatus(sender, e);
        }

        internal void EditMenuItems_Opened(object sender, RoutedEventArgs e)
        {
			ContextMenu cm = (ContextMenu)sender;
			pinEditor.EditMenuItems(cm);
        }

		public void CheckRight(object sender, ContextMenuEventArgs e)
		{
			//TeamGrid fe = (TeamGrid)sender;
			//pinEditor.EditMenuItems(fe.ContextMenu);
		}

		//When the window is resized, the pins need to move to stay in the window
		public void movePins(double widthRatio, double heightRatio)
		{
			pinHandler.movePins(widthRatio, heightRatio);
		}

		private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			if (item != null)
			{
				ContextMenu parent = item.Parent as ContextMenu;
				if (parent != null)
				{
					//EquipmentGrid grid = (EquipmentGrid)parent.PlacementTarget;
					//pinEditor.DeletePin(grid);
				}
			}
		}

		internal void AddResource(String teamName, String interventionName)
		{
			mainWindow.AddResource(teamName, interventionName);
		}

		internal void ReportArrival(Grid team)
		{
			Grid intervention = pinHandler.RelatedIntervention(team);
			if(intervention == null)
			{
                mainWindow.CreateIntervention();
                //pinHandler.AppointTeamToIntervention(team, (Grid) Map.Children[Map.Children.Count - 1]);
                intervention = pinHandler.RelatedIntervention(team);
			}
			mainWindow.ReportArrival(team.Name, intervention.Name);
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
            pinHandler.ReportArrived(interventionName, rowNumber);
        }

		//Upon right click store mouse position to know where to zoom
        private void Map_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(Canvas_map);
            mouseX = mousePos.X;
            mouseY = mousePos.Y;
        }
		

		public void Zoom_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            zoomLevel = (String)mi.Header;
            switch(zoomLevel)
            {
                case "100%":
                    ScaleMap(1);
                    break;
                case "120%":
                    ScaleMap(1.2);
                    break;
                case "140%":
                    ScaleMap(1.4);
                    break;
                case "160%":
                    ScaleMap(1.6);
                    break;
                case "180%":
                    ScaleMap(1.8);
                    break;
                case "200%":
                    ScaleMap(2);
                    break;
            }
        }
		
		public void ScaleMapDefault()
        {
            ScaleTransform ST = new ScaleTransform();
            ST.ScaleX = 1;
            ST.ScaleY = 1;
            imgbrush.RelativeTransform = ST;

            TranslateTransform TT;

            TT = new TranslateTransform(-TTX, -TTY);
            imgbrush.Transform = TT;
        }

        public void ScaleMap(double ratio)
        {
            ScaleMapDefault();
            if (ratio != 1)
            {
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
