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
using ETD.CustomObjects.CustomUIObjects;
using ETD.Services;
using System.Windows.Shapes;

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
    /// Interaction logic for AdditionnalInfoPage.xaml
	/// </summary>
	public partial class AdditionalInfoPage : Page, Observer
	{
		MainWindow mainWindow;
		PinEditor pinEditor;
        AIPinHandler pinHandler;

        private AdditionalInfoPage AIPmap;

        //Drag-and-Drop related variable
        private bool pinDragInProgress;
        private Pin draggedPin;

        // Drawing variables
        private bool IsDrawing = false;
        private System.Windows.Point NewPt1, NewPt2;
        private List<Line> Lines = new List<Line>();
        private Line newline;
        private bool ContainsLine = false;

        public AdditionalInfoPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			pinEditor = new PinEditor(this);
            pinHandler = new AIPinHandler(this);
            AdditionalMap.Focus();

            MapMod.RegisterObserver(this);
		}

        //Loading the map should only be done on the AdditionalInfoPAge.xaml rather than MapSectionPage.xaml
		public void SetMap(BitmapImage coloredImage)
		{
			//Making the picture grayscale
			FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
			grayBitmap.BeginInit();
			grayBitmap.Source = coloredImage;
			grayBitmap.DestinationFormat = PixelFormats.Gray8;
			grayBitmap.EndInit();

			//Displaying the map as the background
            AdditionalMap.Background = new ImageBrush(grayBitmap);
		}
		
        /*
         * moved to pin.cs
		public void SetPinPosition(Grid g, double X, double Y)
		{
			pinHandler.SetPinPosition(g, X, Y);
		}
		*/

		internal void DragStart(object sender, MouseButtonEventArgs e)
		{
            Pin pin = (Pin)sender;

            pinDragInProgress = pin.CaptureMouse();
            draggedPin = pin;
		}

        internal void DrawingStart(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = true;
            //get starting point
            NewPt1 = e.GetPosition(AIPmap.AdditionalMap);
        }

        internal void DrawingMove(object sender, MouseEventArgs e)
        {
            newline = new Line();
            newline.Stroke = System.Windows.Media.Brushes.Black;
            newline.StrokeThickness = 4;
            newline.X1 = NewPt1.X;
            newline.Y1 = NewPt1.Y;
            newline.X2 = NewPt2.X;
            newline.Y2 = NewPt2.Y;

            Lines.Add(newline);
            AIPmap.AdditionalMap.Children.Add(newline);
        }

        internal void Move(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NewPt2 = e.GetPosition(AIPmap.AdditionalMap);
            }

            //false, no element
            bool isEmpty = !Lines.Any();

            //erase line when the escape key is pressed and the mouse is moving
            if (Keyboard.IsKeyDown(Key.Escape) && !isEmpty)
            {
                int i = Lines.Count - 1;
                AIPmap.AdditionalMap.Children.RemoveAt(Lines.Count);

                if (i == 0 && Lines[0] != null)
                {
                    Lines.RemoveAt(0);
                }
                else
                {
                    Lines.RemoveAt(i);
                }
            }
        }

        internal void DrawingStop(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = false;
        }


        internal void ChangeColor(object sender, MouseWheelEventArgs e)
        {
            //scroll up
            if (e.Delta > 0)
            {
                newline.Stroke = System.Windows.Media.Brushes.Red;
            }
            //scroll down to original color
            else
            {
                newline.Stroke = System.Windows.Media.Brushes.Black;
            }
        }

		internal void DragStop(object sender, MouseButtonEventArgs e)
		{
            Pin pin = (Pin)sender;

            //Avoid in having method called on object being collided with
            if (pin != draggedPin)
            {
                return;
            }

            pin.ReleaseMouseCapture();
            pinDragInProgress = false;

            var mousePos = e.GetPosition(AIPmap.AdditionalMap);
		}

		internal void DragMove(object sender, MouseEventArgs e)
		{
            // Set the new cursor.
            Cursor new_cursor = Cursors.Cross;
            if (AIPmap.Cursor != new_cursor)
                AIPmap.Cursor = new_cursor;

            //If no rectangle are clicked, exit method
            if (!pinDragInProgress)
            {
                return;
            }

            Pin pin = (Pin)sender;

            //Handling behaviour where fixed rectangle gets moved when another rectangle is dropped on it
            if (pin != draggedPin)
            {
                return;
            }

            //Get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(AIPmap.AdditionalMap);

            //Making sure it is not dragged out of bounds
            if (mousePos.X < (pin.Width / 2) || (AIPmap.AdditionalMap.ActualWidth - (pin.Width / 2)) < mousePos.X || mousePos.Y < (pin.Width / 2) || (AIPmap.AdditionalMap.ActualHeight - (pin.Width / 2)) < mousePos.Y)
            {
                return;
            }

            pin.setPinPosition(mousePos.X, mousePos.Y);
		}

        internal void DeleteMapMod_Click(object sender, RoutedEventArgs e)
		{
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                ContextMenu parent = item.Parent as ContextMenu;
                if (parent != null)
                {
                    /*
                    foreach (AdditionalInfoGrid grid in AIPmap.AdditionalMap.Children)
                    {
                        AIPmap.AdditionalMap.Children.Remove(grid);
                        return;
                    }
                    */
                }
            }
		}

        public void Update()
        {
            Pin.ClearAllPins(AdditionalMap); //Clearing all pins from the additional info map

            //Creating all normal map modification pins and adding the map to their previous or a new position 
            foreach (MapMod mapModification in MapMod.getMapModList())
            {
                MapModPin mapModPin = new MapModPin(mapModification, this);
                AdditionalMap.Children.Add(mapModPin);

                //Setting the pin to it's previous position, if it exists, or to the top-left corner
                double[] previousPinPosition = Pin.getPreviousPinPosition(mapModification);
                if (previousPinPosition == null)
                {
                    previousPinPosition = new double[] { AdditionalMap.ActualWidth - (mapModPin.Width / 2), (mapModPin.Height / 2) }; //Top-right corner
                }
                mapModPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
            }


            //Creating all map line modification pins and adding the map to their previous or a new position 
            foreach (MapMod mapLineModification in MapMod.getMapModList())
            {
                if (mapLineModification.getMapModType().Equals("line"))
                {
                    MapModPin mapLinePin = new MapModPin(mapLineModification, this, this.AdditionalMap.ActualWidth, this.AdditionalMap.ActualHeight);
                    AdditionalMap.Children.Add(mapLinePin);

                    //Setting the pin to it's previous position, if it exists, or to the top-left corner
                    double[] previousPinPosition = Pin.getPreviousPinPosition(mapLineModification);
                    if (previousPinPosition == null)
                    {
                        previousPinPosition = new double[] { AdditionalMap.ActualWidth - (mapLinePin.Width / 2), (mapLinePin.Height / 2) }; //Top-right corner
                    }
                    mapLinePin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
                }
             
            }


        }

        public void createMapModificationPin(String AI)
        {

            MapMod mapMod = new MapMod(AI);
            MapModPin MapModPin = new MapModPin(mapMod, AIPmap);

            if (AI.Equals("line"))
            {
                MapModPin lineMapModPin = new MapModPin(mapMod, AIPmap, AdditionalMap.ActualWidth,AdditionalMap.ActualHeight);

                //line obj doesnt exist
                if (!ContainsLine)
                {
                    AIPmap.AdditionalMap.Children.Add(lineMapModPin);
                    ContainsLine = true;
                }

            }
            else
            {
                AIPmap.AdditionalMap.Children.Add(MapModPin);
                MapModPin.setPinPosition(MapModPin.ActualHeight/2 , (AdditionalMap.ActualHeight - (MapModPin.ActualHeight/2))); 
            }

            /* AdditionalInfo AI2 = new AdditionalInfo(AI);
            AdditionalInfoGrid mainContainer = new AdditionalInfoGrid(AI2, AIPmap, AddtionalInfoSize);


            if (AI.Equals("line"))
            {
                //if button released, mouse not drawing
                //if button pressed, save point
                //if button relesed, save end point and draw line
                isdrawing = true;

                AIPmap.AdditionalMap.MouseLeftButtonDown += new MouseButtonEventHandler(AIPmap.DrawingStart);
                AIPmap.AdditionalMap.MouseLeftButtonUp += new MouseButtonEventHandler(AIPmap.DrawingStop);
                AIPmap.AdditionalMap.MouseMove += new MouseEventHandler(AIPmap.Move);
                AIPmap.AdditionalMap.MouseUp += new MouseButtonEventHandler(AIPmap.DrawingMove);
                AIPmap.AdditionalMap.MouseWheel += new MouseWheelEventHandler(AIPmap.ChangeColor);

                double lwidth = AIPmap.AdditionalMap.ActualWidth;
                double lheight = AIPmap.AdditionalMap.ActualHeight;
                AdditionalInfoGridLines line = new AdditionalInfoGridLines(AI2, AIPmap, lwidth, lheight);

                //line obj doesnt exist
                if (!ContainsLine)
                {
                    AIPmap.AdditionalMap.Children.Add(line);
                    ContainsLine = true;
                }


                AIPmap.AdditionalMap.Children.Add(mainContainer);

                //Setting pin in the bottom-left corner and making sure it does not cover any other item
                AIPmap.SetPinPosition(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
                //AIPmap.DetectCollision(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
            }*/
        }

        /*
        //When the window is resized, the pins need to move to stay in the window
        public void movePins(double widthRatio, double heightRatio)
        {
            pinHandler.movePins(widthRatio, heightRatio);
        }
        */



	}
}
