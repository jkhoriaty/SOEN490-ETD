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
        ContextMenu cm;

		public MapSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			pinEditor = new PinEditor(this);
			pinHandler = new PinHandler(this);

            
            cm = new ContextMenu();
            this.ContextMenu = cm;
            //cm.Opened += context_Popup;

            MenuItem mi100 = new MenuItem();
            mi100.Header = "100%";
            mi100.Click += Zoom_Click_100;
            cm.Items.Add(mi100);
            mi100.IsChecked = true;

            MenuItem mi120 = new MenuItem();
            mi120.Header = "120%";
            mi120.Click += Zoom_Click_120;
            cm.Items.Add(mi120);

            MenuItem mi140 = new MenuItem();
            mi140.Header = "140%";
            mi140.Click += Zoom_Click_140;
            cm.Items.Add(mi140);

            MenuItem mi160 = new MenuItem();
            mi160.Header = "160%";
            mi160.Click += Zoom_Click_160;
            cm.Items.Add(mi160);

            MenuItem mi180 = new MenuItem();
            mi180.Header = "180%";
            mi180.Click += Zoom_Click_180;
            cm.Items.Add(mi180);

            MenuItem mi200 = new MenuItem();
            mi200.Header = "200%";
            mi200.Click += Zoom_Click_200;
            cm.Items.Add(mi200); 
		}

        public void uncheckMenuItems()
        {
            for(int i = 0; i <= (this.cm.Items.Count) - 1; i++)
            {
                (this.cm.Items[i] as MenuItem).IsChecked = false;
            }
        }
        public void Zoom_Click_100(object sender, EventArgs e)
        {
            ScaleMap(1, 1);
            uncheckMenuItems();
            (this.cm.Items[0] as MenuItem).IsChecked = true;
        }

        public void Zoom_Click_120(object sender, EventArgs e)
        {
            ScaleMap(1.2, 1.2);
            uncheckMenuItems();
            (this.cm.Items[1] as MenuItem).IsChecked = true;
        }

        public void Zoom_Click_140(object sender, EventArgs e)
        {
            ScaleMap(1.4, 1.4);
            uncheckMenuItems();
            (this.cm.Items[2] as MenuItem).IsChecked = true;
        }

        public void Zoom_Click_160(object sender, EventArgs e)
        {
            ScaleMap(1.6, 1.6);
            uncheckMenuItems();
            (this.cm.Items[3] as MenuItem).IsChecked = true;
        }

        public void Zoom_Click_180(object sender, EventArgs e)
        {
            ScaleMap(1.8, 1.8);
            uncheckMenuItems();
            (this.cm.Items[4] as MenuItem).IsChecked = true;
        }

        public void Zoom_Click_200(object sender, EventArgs e)
        {
            ScaleMap(2, 2);
            uncheckMenuItems();
            (this.cm.Items[5] as MenuItem).IsChecked = true;
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

        public void ScaleMapDefault()
        {
            ScaleTransform ST = new ScaleTransform();
            ST.ScaleX = 1;
            ST.ScaleY = 1;;
            imgbrush.Transform = ST;
            imgbrush.RelativeTransform = ST;
        }

        public void ScaleMap(double x, double y)
        {
            ScaleMapDefault();
            if (x != 1 && y != 1)
            {
                var mousePos = Mouse.GetPosition(Map);
                Point centerOfScreen = new Point();
                centerOfScreen.X = Map.ActualWidth / 2;
                centerOfScreen.Y = Map.ActualHeight / 2;

                Point scaledPoint = mousePos;
                scaledPoint.X = scaledPoint.X * x;
                scaledPoint.Y = scaledPoint.Y * y;

                TransformGroup tg = new TransformGroup();
                ScaleTransform ST = new ScaleTransform();
                ST.ScaleX = x;
                ST.ScaleY = y;
                tg.Children.Add(ST);
                imgbrush.RelativeTransform = tg;

                TranslateTransform TT;
                double TTX = 0;
                double TTY = 0;
                
                //cursor is in the top right corner of screen
                if (mousePos.X > centerOfScreen.X && mousePos.Y < centerOfScreen.Y)
                {
                    TTX = -Map.ActualWidth*x + Map.ActualWidth;//-(scaledPoint.X - mousePos.X);
                    TTY = 0;//-Map.ActualHeight;//-(scaledPoint.Y - mousePos.Y);
                    // MessageBox.Show(Convert.ToString(mousePos.X) + "  " + Convert.ToString(mousePos.Y) + " " + Convert.ToString(TTY));
                }
                //cursor is in bottom right corner of screen
                else if (mousePos.X > centerOfScreen.X && mousePos.Y > centerOfScreen.Y)
                {

                    //not currently working
                    TTX = -Map.ActualWidth*x + Map.ActualWidth;
                    TTY = -Map.ActualHeight*y + Map.ActualHeight;

                    //MessageBox.Show(Convert.ToString(scaledPoint.Y) + "  " + Convert.ToString(mousePos.Y) + " " + Convert.ToString(TTY));
                }
                //cursor is in bottom left corner of screen
                else if (mousePos.X < centerOfScreen.X && mousePos.Y > centerOfScreen.Y)
                {
                    TTX = 0;
                    TTY = -Map.ActualHeight * y + Map.ActualHeight;
                }


                TT = new TranslateTransform(TTX, TTY);
                //MessageBox.Show(Convert.ToString(TTX) + "  " + Convert.ToString(TTY));
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
					pinEditor.DeletePin(grid.Name);
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
			if(intervention != null)
			{
				mainWindow.ReportArrival(team.Name, intervention.Name);
			}
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
            pinHandler.ReportArrived(interventionName, rowNumber);
        }
	}
}
