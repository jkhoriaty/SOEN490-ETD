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

		public MapSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			pinEditor = new PinEditor(this);
			pinHandler = new PinHandler(this);
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
            ScaleMap(1.2, 1.2);

		}

        public void ScaleMap(double x, double y)
        {
            ScaleTransform a = new ScaleTransform();
            a.ScaleX = x;
            a.ScaleY = y;
            imgbrush.Transform = a;
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
			pinHandler.SetPinPosition(g, X, Y);
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
			pinEditor.EditMenuItems(sender, e);
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
					pinEditor.DeletePin(grid.Name);
				}
			}
		}
	}
}
