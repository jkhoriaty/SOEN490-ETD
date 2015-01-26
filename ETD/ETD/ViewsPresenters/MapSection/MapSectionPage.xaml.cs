using ETD.Models.Objects;
using ETD.Models.Grids;
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

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
	/// Interaction logic for MapSectionPage.xaml
	/// </summary>
	public partial class MapSectionPage : Page
	{
		MainWindow mainWindow;
		PinEditor pinEditor;
		PinHandler pinHandler;
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
            Map.ContextMenu = Resources["ContextMenu"] as ContextMenu;
		}

        public void Zoom(object sender, EventArgs e)
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
			Map.Background = imgbrush;

		}

        private void Map_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(Map);
            mouseX = mousePos.X;
            mouseY = mousePos.Y;
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

                TTX = -mouseX * ratio + Map.ActualWidth / 2;
                TTY = -mouseY * ratio + Map.ActualHeight / 2;

                TT = new TranslateTransform(TTX, TTY);
                imgbrush.Transform = TT;
            }
            
        }
		
		//Pushing request to children classes
		public void CreateTeamPin(Team team)
		{
			pinEditor.CreateTeamPin(team);
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

        internal void ChangeStatus(object sender, RoutedEventArgs e)
        {
            pinEditor.ChangeStatus(sender, e);
        }

        internal void EditMenuItems(object sender, RoutedEventArgs e)
        {
			ContextMenu cm = (ContextMenu)sender;
			pinEditor.EditMenuItems(cm);
        }

		public void CheckRight(object sender, ContextMenuEventArgs e)
		{
			TeamGrid fe = (TeamGrid)sender;
			pinEditor.EditMenuItems(fe.ContextMenu);
		}

		//When the window is resized, the pins need to move to stay in the window
		public void movePins(double widthRatio, double heightRatio)
		{
			pinHandler.movePins(widthRatio, heightRatio);
		}

		private void DeleteEquipment(object sender, RoutedEventArgs e)
		{
			MenuItem item = sender as MenuItem;
			if (item != null)
			{
				ContextMenu parent = item.Parent as ContextMenu;
				if (parent != null)
				{
					EquipmentGrid grid = (EquipmentGrid)parent.PlacementTarget;
					pinEditor.DeletePin(grid);
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
                pinHandler.AppointTeamToIntervention(team, (Grid) Map.Children[Map.Children.Count - 1]);
                intervention = pinHandler.RelatedIntervention(team);
			}
			mainWindow.ReportArrival(team.Name, intervention.Name);
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
            pinHandler.ReportArrived(interventionName, rowNumber);
        }
	}
}
