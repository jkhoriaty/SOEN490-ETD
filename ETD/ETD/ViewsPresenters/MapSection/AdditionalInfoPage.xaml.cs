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
using System.Windows.Shapes;

namespace ETD.ViewsPresenters.MapSection
{
	/// <summary>
    /// Interaction logic for AdditionnalInfoPage.xaml
	/// </summary>
	public partial class AdditionalInfoPage : Page, Observer
	{
		MainWindow mainWindow;
        ImageBrush imgbrush;//Used for loading the map
        private bool isMapLoaded= false;//Used to check if a map was loaded

        // Drawing lines variables
        private bool IsDrawing = false;
        private System.Windows.Point NewPt1, NewPt2;
        private List<Line> Lines = new List<Line>();
        private Line newline;
        private bool ContainsLine = false;

        //Drawing shapes variables
        private List<object> objectList = new List<object>();//Contains the list of added map modification items
        private System.Windows.Shapes.Shape mapModObject;
        private int _startX, _startY;
        private String mapModName;

        //Creates a new Additional map information page
        public AdditionalInfoPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
            AdditionalMap.Focus();
			Observable.RegisterClassObserver(typeof(MapMod), this);
		}

        //Loading the map 
		public void setMap(BitmapImage coloredImage)
		{
            //Making the picture grayscale
            FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
            grayBitmap.BeginInit();
            grayBitmap.Source = coloredImage;
            grayBitmap.DestinationFormat = PixelFormats.Gray8;
            grayBitmap.EndInit();

            //Displaying the map as the background
            imgbrush = new ImageBrush(grayBitmap);
            isMapLoaded = true;
            AdditionalMap.Background = imgbrush;
		}

        //Checks if a map has been loaded
        public bool MapLoaded()
        {
            return isMapLoaded;
        }
		
        //Get the current position of the mouse on the screen when the left mouse button is clicked
        internal void DrawingStart(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = true;
            //get starting point
            NewPt1 = e.GetPosition(AdditionalMap);
            _startX = Convert.ToInt32(NewPt1.X);
            _startY = Convert.ToInt32(NewPt1.Y);

        }

        //Set the map modification object type
        internal void setMapModObjectType()
        {
            if (mapModName.Equals("rectangle"))
            {
                mapModObject = new System.Windows.Shapes.Rectangle();
                mapModObject.StrokeThickness = 4;
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

        //The selected map modification item is drawn on the map when the left mouse button is released
        internal void DrawingMove(object sender, MouseEventArgs e)
        {
            //Draws the selected map modification option
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
                //The width of the shape is the the maximum between the start x-position and current x-position minus the minimum of start x-position and current x-position
                int width = Convert.ToInt32(Math.Max(_startX, NewPt2.X) - Math.Min(_startX, NewPt2.X));

                //For the height value, it's basically the same thing as above, but with the y-values:
                int height = Convert.ToInt32(Math.Max(_startY, NewPt2.Y) - Math.Min(_startY, NewPt2.Y));

                setMapModObjectType();
                mapModObject.Stroke = System.Windows.Media.Brushes.Black;
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

        //When the mouse is moving, get its position to create the selected map modification item
        internal void Move(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NewPt2 = e.GetPosition(AdditionalMap);
            }

            bool isEmpty = !objectList.Any();//Checks if the list of map modification object is empty

            //When the mouse is moving and the escape key is pressed, remove the most recently added map modification item
            if (Keyboard.IsKeyDown(Key.Escape) && !isEmpty)
            {
                try
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
                catch (Exception ex)
                {

                }
               
            }
        }

        //Stopped drawing 
        internal void DrawingStop(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = false;
        }

        //Change the map modification color on mouse scroll
        internal void ChangeColor(object sender, MouseWheelEventArgs e)
        {
            //scroll up to new color
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

        //Creates a new Map modification item
        public void CreateMapModificationPin(String AI)
        {
            mapModName = AI;
            MapMod mapMod = new MapMod("line");
            MapModPin lineMapModPin = new MapModPin(mapMod, this, this.ActualWidth, this.ActualHeight);
            this.Cursor = Cursors.Pen;
            if (!ContainsLine)
            {
                AdditionalMap.Children.Add(lineMapModPin);
                ContainsLine = true;
            }
        }

        //Notifies its observables
        public void Update()
        {
            /*    
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
            */
        }

	}
}
