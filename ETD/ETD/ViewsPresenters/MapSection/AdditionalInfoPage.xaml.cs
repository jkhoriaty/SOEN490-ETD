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

        //Drag-and-Drop related variable
        private bool pinDragInProgress;
        private Pin draggedPin;

        // Drawing variables
        private bool IsDrawing = false;
        private System.Windows.Point NewPt1, NewPt2;
        private List<Line> Lines = new List<Line>();
        private Line newline;
        private bool ContainsLine = false;

        //variables for drawing shapes
        private List<object> objectList = new List<object>();
        private System.Windows.Shapes.Shape mapModObject;
        private int _startX, _startY;
        private String mapModName;
        private System.Drawing.Rectangle _rect;

        public AdditionalInfoPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
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
		

		internal void DragStart(object sender, MouseButtonEventArgs e)
		{
         /*   Pin pin = (Pin)sender;
            pinDragInProgress = pin.CaptureMouse();
            draggedPin = pin;
          * */
		}

        internal void DrawingStart(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = true;
            //get starting point
            NewPt1 = e.GetPosition(AdditionalMap);
            _startX = Convert.ToInt32(NewPt1.X);
            _startY = Convert.ToInt32(NewPt1.Y);

        }

        internal void setMapModObjectType()
        {
            if (mapModName.Equals("rectangle"))
            {
                mapModObject = new System.Windows.Shapes.Rectangle();
            }

            if (mapModName.Equals("circle"))
            {
                mapModObject = new System.Windows.Shapes.Ellipse();
            }

            if (mapModName.Equals("ramp") || mapModName.Equals("camp") || mapModName.Equals("stairs")  )
            {
                mapModObject = new System.Windows.Shapes.Rectangle();
                ImageBrush mapModImg = new ImageBrush();
                MapMods mapModi = (MapMods)Enum.Parse(typeof(MapMods), mapModName);
                mapModImg.ImageSource = TechnicalServices.getImage(mapModi);
                mapModObject.Fill = mapModImg;
            }

            if (mapModName.Equals("line"))
            {
                return;
            }
        }

        internal void DrawingMove(object sender, MouseEventArgs e)
        {
            if (mapModName.Equals("line"))
            {
                newline = new Line();
                newline.Stroke = System.Windows.Media.Brushes.Black;
                newline.StrokeThickness = 4;
                newline.X1 = NewPt1.X;
                newline.Y1 = NewPt1.Y;
                newline.X2 = NewPt2.X;
                newline.Y2 = NewPt2.Y;

                objectList.Add(newline);
                AdditionalMap.Children.Add(newline);
            }

            else if (!mapModName.Equals("line"))
            {
                //The width of the shape should be the maximum between the start x-position and current x-position minus
                //the minimum of start x-position and current x-position
                int width = Convert.ToInt32(Math.Max(_startX, NewPt2.X) - Math.Min(_startX, NewPt2.X));

                //For the height value, it's basically the same thing as above, but with the y-values:
                int height = Convert.ToInt32(Math.Max(_startY, NewPt2.Y) - Math.Min(_startY, NewPt2.Y));

                setMapModObjectType();
                mapModObject.Stroke = System.Windows.Media.Brushes.Black;
                mapModObject.StrokeThickness = 4;
                mapModObject.Width = width;
                mapModObject.Height = height;

                if (NewPt2.X < NewPt1.X) //draw from right to left
                {
                    mapModObject.Margin = new Thickness(NewPt2.X, NewPt2.Y, NewPt1.X, NewPt1.Y);
                }
                else //draw from left to right 
                {
                    mapModObject.Margin = new Thickness(NewPt1.X, NewPt1.Y, NewPt2.X, NewPt2.Y);
                }

                objectList.Add(mapModObject);
                AdditionalMap.Children.Add(mapModObject);
            }
        }

        internal void Move(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NewPt2 = e.GetPosition(AdditionalMap);
            }

            //false, no element
            bool isEmpty = !objectList.Any();

            //erase line when the escape key is pressed and the mouse is moving
            if (Keyboard.IsKeyDown(Key.Escape) && !isEmpty)
            {
                int objectIndex = objectList.Count - 1;
                AdditionalMap.Children.RemoveAt(objectList.Count);

                if (objectIndex == 0 && objectList[0] != null)
                {
                    objectList.RemoveAt(0);
                }
                else
                {
                    objectList.RemoveAt(objectIndex);
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

                if (objectList != null)
                {
                    foreach (Shape ob in objectList)
                    {
                        ob.Stroke = System.Windows.Media.Brushes.Red;
                    }

                }

            }
            //scroll down to original color
            else
            {
                if (objectList != null)
                {
                    foreach (Shape ob in objectList)
                    {
                        ob.Stroke = System.Windows.Media.Brushes.Black;
                    }
                }
            }

        }

        public void createMapModificationPin(String AI)
        {
            mapModName = AI;
            MapMod mapMod = new MapMod("line");
            MapModPin lineMapModPin = new MapModPin(mapMod, this, this.ActualWidth, this.ActualHeight);

            if (!ContainsLine)
            {
                AdditionalMap.Children.Add(lineMapModPin);
                ContainsLine = true;
            }
        }

		internal void DragStop(object sender, MouseButtonEventArgs e)
		{
            /*
            Pin pin = (Pin)sender;

            //Avoid in having method called on object being collided with
            if (pin != draggedPin)
            {
                return;
            }

            pin.ReleaseMouseCapture();
            pinDragInProgress = false;

            var mousePos = e.GetPosition(AdditionalMap);
             * */
		}

		internal void DragMove(object sender, MouseEventArgs e)
		{
         /*   // Set the new cursor.
            Cursor new_cursor = Cursors.Cross;
            if (Cursor != new_cursor)
                Cursor = new_cursor;

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
            var mousePos = e.GetPosition(AdditionalMap);

            //Making sure it is not dragged out of bounds
            if (mousePos.X < (pin.Width / 2) || (AdditionalMap.ActualWidth - (pin.Width / 2)) < mousePos.X || mousePos.Y < (pin.Width / 2) || (AdditionalMap.ActualHeight - (pin.Width / 2)) < mousePos.Y)
            {
                return;
            }

            pin.setPinPosition(mousePos.X, mousePos.Y);
          * */
		}

        internal void DeleteMapMod_Click(object sender, RoutedEventArgs e)
		{
            /*
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
                    
                }
            }
             * */
		}

        public void Update()
        {
            //Pin.ClearAllPins(AdditionalMap); //Clearing all pins from the additional info map

            /*Creating all normal map modification pins and adding the map to their previous or a new position 
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
                
                    MapModPin mapLinePin = new MapModPin(mapLineModification, this, this.ActualWidth, this.ActualHeight);
                    AdditionalMap.Children.Add(mapLinePin);

                    //Setting the pin to it's previous position, if it exists, or to the top-left corner
                    double[] previousPinPosition = Pin.getPreviousPinPosition(mapLineModification);
                    if (previousPinPosition == null)
                    {
                        previousPinPosition = new double[] { this.ActualWidth - (mapLinePin.Width / 2), (mapLinePin.Height / 2) }; //Top-right corner
                    }
                    mapLinePin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
                }
             
            }
            */
            
        }

	}
}
