using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;




namespace Emergency_Team_Dispatcher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

        //variable initialization
        private bool _isRectDragInProg;
        String movingRectangle;
        int shapeRadius = 25;
        int TeamNumberPosition = 0;
        int TeamLabelPosition = 0;
        int TeamEquipPositionTop =31;
        int iconPositionLeft = 0;

		public MainWindow()
		{
			InitializeComponent();
            LanguageSelector.loadVocabulary();
            //Put preferred default language
            LanguageSelector.changeLanguage(this, "en");
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewEvent_MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Map_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg; *.png; *.gif";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                System.IO.FileInfo File = new System.IO.FileInfo(openFileDialog.FileName);
                canvas.Background = new ImageBrush(new BitmapImage(new Uri(openFileDialog.FileName)));
            }
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

		private int rectangleCtr = 1;

        private void Team_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);
            System.Windows.Media.Color randomColor = System.Windows.Media.Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
            System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
			r.Name = "team_" + rectangleCtr++;
            r.Width = shapeRadius * 2;
            r.Height = shapeRadius * 2;
            r.Stroke = new SolidColorBrush(Colors.Black);
            r.Fill = new SolidColorBrush(randomColor);
            r.MouseLeftButtonDown += new MouseButtonEventHandler(team_MouseLeftButtonDown);
            r.MouseLeftButtonUp += new MouseButtonEventHandler(team_MouseLeftButtonUp);
            r.MouseMove += new MouseEventHandler(team_MouseMove);
            Canvas.SetTop(r, 0);
            Canvas.SetLeft(r, 0);
            canvas.Children.Add(r);

            //create team
            Teamformation(sender, e);

            //add equipment 
            label_Click(sender, e);

            CreateTeamForm Ctf = new CreateTeamForm();
            Ctf.Show();

            //box.Text = "succ";
        }


        private void team_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Rectangle r = sender as System.Windows.Shapes.Rectangle;
            _isRectDragInProg = r.CaptureMouse();
            movingRectangle = r.Name;
        }

        private void team_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Rectangle r = sender as System.Windows.Shapes.Rectangle;

            //Avoid in having method called on object being collided with
            if (!r.Name.Equals(movingRectangle))
            {
                return;
            }

            r.ReleaseMouseCapture();
            _isRectDragInProg = false;

            var mousePos = e.GetPosition(canvas);
            double horizontalDropped = mousePos.X;
            double verticalDropped = mousePos.Y;

            //Calling collision detection and resolution for the dropped object
            collisionDetection(r, horizontalDropped, verticalDropped);
        }

        private void collisionDetection(System.Windows.Shapes.Rectangle r, double horizontalDropped, double verticalDropped)
        {
            //Replacing item within horizontal bounds
            if (horizontalDropped > (canvas.ActualWidth - shapeRadius)) //Right
            {
                horizontalDropped = canvas.ActualWidth - shapeRadius;
            }
            else if (horizontalDropped < shapeRadius) //Left
            {
                horizontalDropped = shapeRadius;
            }

            //Replacing item within vertical bounds
            if (verticalDropped > (canvas.ActualHeight - shapeRadius)) //Bottom
            {
                verticalDropped = canvas.ActualHeight - shapeRadius;
            }
            else if (verticalDropped < shapeRadius)
            {
                verticalDropped = shapeRadius;
            }

            bool collisionDetected = true;
            int verificationCount = 0;

            //Loop to make sure that last verification ensures no collision with any object
            while (collisionDetected == true)
            {

                collisionDetected = false;
                verificationCount++;

                //Gathering all rectangles to search for collision
                var rectangles = canvas.Children.OfType<System.Windows.Shapes.Rectangle>().ToList();

                //Iterating throught them
                foreach (var rectangle in rectangles)
                {
                    //Skipping collision-detection with itself
                    if (rectangle != r)
                    {
                        //Getting the position of where the rectangle has been dropped
                        double horizontalFixed = Math.Round((((double)Canvas.GetLeft(rectangle)) + shapeRadius), 3);
                        double verticalFixed = Math.Round((((double)Canvas.GetTop(rectangle)) + shapeRadius), 3);

                        //Checking if the dropped rectangle is within the bounds of any other rectangle
                        while (horizontalDropped > (horizontalFixed - (shapeRadius * 2)) && horizontalDropped < (horizontalFixed + (shapeRadius * 2)) && verticalDropped > (verticalFixed - (shapeRadius * 2)) && verticalDropped < (verticalFixed + (shapeRadius * 2)))
                        {
                            //Collision detected, resolution by shifting the rectangle in the same direction that it has been dropped
                            collisionDetected = true;

                            //Rounding values to avoid ratio division by tiny number creating a huge ratio
                            horizontalDropped = Math.Round(horizontalDropped, 3);
                            verticalDropped = Math.Round(verticalDropped, 3);

                            //Finding out how much is the dropped rectangle covering the fixed one
                            double horizontalDifference = Math.Round((horizontalDropped - horizontalFixed), 3);
                            double verticalDifference = Math.Round((verticalDropped - verticalFixed), 3);
                            double differenceRatio = 0.1;
                            bool moved = false;

                            //////////////////////////////////////////DEBUGGING CODE/////////////////////////////////////
                            /*box.Text = "horDrop: " + horizontalDropped + "; verDrop: " + verticalDropped;
                            box2.Text = "horPos: " + horizontalFixed + "; verPos: " + verticalFixed;
                            box3.Text = "horDifference: " + horizontalDifference;
                            box4.Text = "verDifference: " + verticalDifference;*/
                            /////////////////////////////////////////////////////////////////////////////////////////////

                            //Don't move horizontally if there are no difference and avoiding division by 0
                            if (horizontalDifference != 0)
                            {
                                //Finding the ratio at which we should increase the values to put the objects side by side but in the same direction as it was dropped
                                differenceRatio = Math.Round(((Math.Abs(verticalDifference) / Math.Abs(horizontalDifference)) / 10), 3);

                                //Shifting horizontally in the correct direction, if not at the border
                                if (shapeRadius < horizontalDropped && horizontalDropped < (canvas.ActualWidth - shapeRadius))
                                {
                                    if (horizontalDifference < 0)
                                    {
                                        horizontalDropped -= 0.1;
                                        moved = true;
                                    }
                                    else
                                    {
                                        horizontalDropped += 0.1;
                                        moved = true;
                                    }
                                }
                            }

                            //Don't move vertically if there are no difference
                            if (verticalDifference != 0)
                            {
                                //Shifting vertically in the correct direction
                                if (shapeRadius < verticalDropped && verticalDropped < (canvas.ActualHeight - shapeRadius))
                                {
                                    if (verticalDifference < 0)
                                    {
                                        verticalDropped -= differenceRatio;
                                        moved = true;
                                    }
                                    else
                                    {
                                        verticalDropped += differenceRatio;
                                        moved = true;
                                    }
                                }
                            }

                            //Handling situation where object is dropped between two others and is just bouncing around, placing object in the middle
                            if (verificationCount > 100)
                            {

                                MessageBox.Show("The droppd object is dropped between two objects and is bouncing around with no progress. Resetting it.");
                                horizontalDropped = (canvas.ActualWidth / 2);
                                verticalDropped = (canvas.ActualHeight / 2);
                                verificationCount = 0;
                            }

                            //Handling case of perfect superposition
                            if (horizontalDropped == horizontalFixed && verticalDropped == verticalFixed)
                            {
                                MessageBox.Show("Perfect superposition");
                                horizontalDropped = horizontalDropped + (2 * shapeRadius);
                                moved = true;
                            }

                            //Handling corner situation, placing object back in the middle
                            if (moved == false)
                            {
                                MessageBox.Show("There's not enough space in the corner for this item. Replacing it in the center for you to replace it elsewhere.");

                                double horizontalToBorder = Math.Min(horizontalFixed, (canvas.ActualWidth - horizontalFixed));
                                double verticalToBorder = Math.Min(verticalFixed, (canvas.ActualHeight - verticalFixed));

                                if (horizontalToBorder <= verticalToBorder) //Need horizontal mvoement
                                {
                                    if (horizontalDropped <= shapeRadius) //Left
                                    {
                                        horizontalDropped = horizontalFixed + (shapeRadius * 2);
                                    }
                                    else //Right
                                    {
                                        horizontalDropped = horizontalFixed - (shapeRadius * 2);
                                    }
                                }
                                else //Need vertical mvoement
                                {
                                    if (verticalDropped <= shapeRadius) //Left
                                    {
                                        verticalDropped = verticalFixed + (shapeRadius * 2);
                                    }
                                    else //Right
                                    {
                                        verticalDropped = verticalFixed - (shapeRadius * 2);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Drop the rectangle if there are not collision or after resolution of collision
            Canvas.SetLeft(r, (horizontalDropped - shapeRadius));
            Canvas.SetTop(r, (verticalDropped - shapeRadius));
        }

        //Method to visually drag the item selected and insuring it doesn't go outside of the map
        private void team_MouseMove(object sender, MouseEventArgs e)
        {
            //If no rectangle are clicked, exit method
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);
            System.Windows.Shapes.Rectangle r = sender as System.Windows.Shapes.Rectangle;

            //Handling exception where fixed rectangle gets moved when another rectangle is dropped on it
            if (!r.Name.Equals(movingRectangle))
            {
                return;
            }

            //Making sure it is not dragged out of bounds
            if (mousePos.X > (canvas.ActualWidth - shapeRadius) || mousePos.Y > (canvas.ActualHeight - shapeRadius) || mousePos.X < shapeRadius || mousePos.Y < shapeRadius)
            {
                return;
            }

            Canvas.SetLeft(r, (mousePos.X - shapeRadius));
            Canvas.SetTop(r, (mousePos.Y - shapeRadius));
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {
            LanguageSelector.changeLanguage(this, "fr");
        }

        private void _English_Click(object sender, RoutedEventArgs e)
        {
            LanguageSelector.changeLanguage(this, "en");
        }

        //Assign and display team name
        private void Teamformation(object sender, EventArgs e)
        {
           
            string TeamName = "Team ";
            string[] TeamNumber = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "ALPHA", "BETA", "THETA" };

            Label dynamicLabel = new Label();
            dynamicLabel.Name = "Label";
            dynamicLabel.Width = 100;
            dynamicLabel.Height = 30;
            dynamicLabel.Content = TeamName + TeamNumber[TeamNumberPosition];
            dynamicLabel.Foreground = new SolidColorBrush(Colors.White);
            dynamicLabel.Background = new SolidColorBrush(Colors.Black);
            dynamicLabel.BorderBrush = System.Windows.Media.Brushes.Black;

            Canvas.SetLeft(dynamicLabel, 0);
            Canvas.SetTop(dynamicLabel, TeamLabelPosition);
            Team_display.Children.Add(dynamicLabel);
            TeamLabelPosition += 60;
            TeamNumberPosition++;

        }

        //Add equipment menu
        private void label_Click(object sender, RoutedEventArgs e)
        {
          
            //add equipment by right clicking
            ContextMenu mnuContextMenu = new ContextMenu();
            this.ContextMenu = mnuContextMenu;
         
            mnuContextMenu.Width = 200;
            mnuContextMenu.Height = 130;
            MenuItem AmbulanceCart = new MenuItem();
            MenuItem MountedStretcher = new MenuItem();
            MenuItem SittingCart = new MenuItem();
            MenuItem TransportStretcher = new MenuItem();
            MenuItem WheelChair = new MenuItem();

            AmbulanceCart.Tag = "Ambulance_Cart";
            AmbulanceCart.Header = "Add Ambulance Cart";

            MountedStretcher.Tag = "Mounted_stretcher";
            MountedStretcher.Header = "Add Mounted stretcher";

            SittingCart.Tag = "Sitting_Cart";
            SittingCart.Header = "Add Sitting Cart";

            TransportStretcher.Tag = "Transport_Stretcher";
            TransportStretcher.Header = "Add Transport Stretcher";

            WheelChair.Tag = "WheelChair";
            WheelChair.Header = "Add WheelChair";

            mnuContextMenu.Items.Add(AmbulanceCart);
            mnuContextMenu.Items.Add(MountedStretcher);
            mnuContextMenu.Items.Add(SittingCart);
            mnuContextMenu.Items.Add(TransportStretcher);
            mnuContextMenu.Items.Add(WheelChair);

            //test display icon
            //to be refactored and put into their own command method
            loadTransportStretcher(sender, e);
            loadWheelChair(sender, e);
            loadAmbulanceCart(sender, e);
            loadSittingCart(sender, e);
            loadMountedStretcher(sender, e);
           
            //reset icon position for the next team to be created
            iconPositionLeft = 0;

            //place icon under current team
            TeamEquipPositionTop += 60;

        
        }

        //Displaying Equipment(to be refactored)
        void loadWheelChair(object sender, RoutedEventArgs e)
        {

            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 25;
            myImage.Height = 23;
            
            //position icon
            Canvas.SetLeft(myImage, iconPositionLeft);
            Canvas.SetTop(myImage, TeamEquipPositionTop);
        
            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Soen 490\Icons\WheelChair2.png");
            myBitmapImage.DecodePixelWidth = 25;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);
            iconPositionLeft += 26;
        }

        void loadAmbulanceCart(object sender, RoutedEventArgs e)
        {


            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 25;
            myImage.Height = 23;

            //position icon
            Canvas.SetLeft(myImage, iconPositionLeft);
            Canvas.SetTop(myImage, TeamEquipPositionTop);

            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Soen 490\Icons\AmbulanceCart2.png");
            myBitmapImage.DecodePixelWidth = 25;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);

            iconPositionLeft += 26;
        }

        void loadSittingCart(object sender, RoutedEventArgs e)
        {


            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 25;
            myImage.Height = 23;

            //position icon
            Canvas.SetLeft(myImage, iconPositionLeft);
            Canvas.SetTop(myImage, TeamEquipPositionTop);

            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Soen 490\Icons\SittingCart2.png");
            myBitmapImage.DecodePixelWidth = 25;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);

            iconPositionLeft += 26;
        }

        void loadMountedStretcher(object sender, RoutedEventArgs e)
        {

            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 25;
            myImage.Height = 23;

            //position icon
            Canvas.SetLeft(myImage, iconPositionLeft);
            Canvas.SetTop(myImage, TeamEquipPositionTop);

            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Soen 490\Icons\MountedStretcher2.png");
            myBitmapImage.DecodePixelWidth = 25;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);
            iconPositionLeft += 26;
        }

        void loadTransportStretcher(object sender, RoutedEventArgs e)
        {

            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 25;
            myImage.Height = 23;

            //position icon
            Canvas.SetLeft(myImage, iconPositionLeft);
            Canvas.SetTop(myImage, TeamEquipPositionTop);

            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Soen 490\Icons\TransportStretcher2.png");
            myBitmapImage.DecodePixelWidth = 25;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);
            iconPositionLeft += 26;
        }


	}


}
