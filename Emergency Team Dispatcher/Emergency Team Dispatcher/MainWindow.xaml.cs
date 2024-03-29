﻿using Microsoft.Win32;
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
using System.Diagnostics;
using System.Threading;



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
        int shapeRadius = 20;
        int TeamNumberPosition = 0;
        int TeamNameLabelPosition = 0;
        int TeamEquipPositionTop = 43;
        int TeamMemberPosition = 70;
        int iconPositionLeft = 0;
        int NumberOfEquipment = 0;
        int LevelofTrainingPositionLeft = 150;
        int TeamCount = 1;
        int i = 0;
		String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
      

		public MainWindow()
		{
			InitializeComponent();
           	LanguageSelector.loadVocabulary();
            //Put preferred default language
            LanguageSelector.changeLanguage(this, "en");
			AbsolutePath = AbsolutePath.Substring(6) + @"\";
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TimeTest_MenuItem_Click(object sender, RoutedEventArgs e)
        {
			MessageBox.Show(dbAccess.OpenConnection());
            //This Method currently returns the timer of the last queue'd timer.
            //Once the intervention creation interface is completed, it will be a trivial
            //matter to select which intervention from the dictionary you would like to retrieve the time from.
            //It is important to note, that in this current function, the timer keeps going even after you poll
            //it for its value. I left this in intentionally to prove that my function for retrieving didn't break
            //the stopwatch's functionality. This could be extremely useful if we wanted to keep a running display of
            //the time taken for each portion of each intervention.
            if (Globals.timers.ContainsKey(Globals.currentIntervention - 1))
            {
                TimeSpan elapsed = Globals.timers[Globals.currentIntervention - 1].Elapsed;
                timer.Text = elapsed.TotalSeconds.ToString();                               //Converts the total time on the stopwatch into an amount of seconds.

                Globals.interventionTime.Add(Globals.currentIntervention - 1, elapsed);     //Saves it to a second dictionary, this one only stores the elapsed times, this will be pushed to DB.
            }
        }

        private void NewEvent_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            //Menu item that initialises the timer and saves it to a dictionary with an incrementing index.
            //This index represents the ID of the intervention. It's currently simply the numbers from 1-X because
            //I didn't have out naming convention for interventions on hand. It is a trivial matter to change the index
            //to be anything we want, very little would have to be adjusted.
            int temp = Globals.currentIntervention;
            Stopwatch interventionTimer = new Stopwatch();
            interventionTimer.Start();

            Globals.timers.Add(temp, interventionTimer);

            Globals.currentIntervention += 1;

            timer.Text = Globals.currentIntervention.ToString();

        }

        private void Map_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg; *.png; *.gif";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                System.IO.FileInfo File = new System.IO.FileInfo(openFileDialog.FileName);
                BitmapImage coloredImage = new BitmapImage(new Uri(openFileDialog.FileName));

                FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
                grayBitmap.BeginInit();
                grayBitmap.Source = coloredImage;
                grayBitmap.DestinationFormat = PixelFormats.Gray8;
                grayBitmap.EndInit();

                Map.Background = new ImageBrush(grayBitmap);
            }
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

		private int rectangleCtr = 1;

        private void Team_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TeamCount/(TeamCount-i)==TeamCount && TeamCount!=1)
            {

                iconPositionLeft = 0;
                TeamEquipPositionTop += 150;
                NumberOfEquipment = 0;
                e.Handled = true;

            }

            i++;
            TeamCount++;

			//ShowDialog creates a modal form so need to execute in a new thread because it will only keep control of the thread
			int currentTeam = Globals.currentTeam;
			//Thread thread = new Thread(new ThreadStart(formThread));
			//thread.Start();
			//thread.Join();

            CreateTeamForm Ctf = new CreateTeamForm(this);
            Ctf.Show();

			//if(currentTeam < Globals.currentTeam) TeamDisplay();
            //box.Text = "succ";
        }

		//Execution of form
		private void formThread()
		{
			CreateTeamForm Ctf = new CreateTeamForm(this);
			Ctf.ShowDialog();
		}

        //Add equipment on the map
        private void Add_Equipment_Click(object sender, RoutedEventArgs e)
        {
            MenuItem Equipment = (MenuItem)sender;
 
            System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
            r.Width = 25;
            r.Height = 23;
            ImageBrush imgb = new ImageBrush();
            
            if (Equipment.Name == "_AmbulanceCart")
            {

                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\AmbulanceCart.png"));

            }

            if (Equipment.Name == "_SittingCart")
            {
                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\SittingCart.png"));
            }

            if (Equipment.Name == "_MountedStretcher")
            {
                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\MountedStretcher.png"));
            }

            if (Equipment.Name == "_WheelChair")
            {
                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\WheelChair.png"));
            }

            if (Equipment.Name == "_TransportStretcher")
            {
                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\TransportStretcher.png"));
            }

            if (Equipment.Name == "_Epipen")
            {
                imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\Epipen.png"));
            }

            r.Fill = imgb;

            r.MouseLeftButtonDown += new MouseButtonEventHandler(team_MouseLeftButtonDown);
            r.MouseLeftButtonUp += new MouseButtonEventHandler(team_MouseLeftButtonUp);
            r.MouseMove += new MouseEventHandler(team_MouseMove);
        
            Map.Children.Add(r);

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

            var mousePos = e.GetPosition(Map);
            double horizontalDropped = mousePos.X;
            double verticalDropped = mousePos.Y;

            //Calling collision detection and resolution for the dropped object
            collisionDetection(r, horizontalDropped, verticalDropped);
        }

        private void collisionDetection(System.Windows.Shapes.Rectangle r, double horizontalDropped, double verticalDropped)
        {
            //Replacing item within horizontal bounds
            if (horizontalDropped > (Map.ActualWidth - shapeRadius)) //Right
            {
                horizontalDropped = Map.ActualWidth - shapeRadius;
            }
            else if (horizontalDropped < shapeRadius) //Left
            {
                horizontalDropped = shapeRadius;
            }

            //Replacing item within vertical bounds
            if (verticalDropped > (Map.ActualHeight - shapeRadius)) //Bottom
            {
                verticalDropped = Map.ActualHeight - shapeRadius;
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
                var rectangles = Map.Children.OfType<System.Windows.Shapes.Rectangle>().ToList();

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
                                if (shapeRadius < horizontalDropped && horizontalDropped < (Map.ActualWidth - shapeRadius))
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
                                if (shapeRadius < verticalDropped && verticalDropped < (Map.ActualHeight - shapeRadius))
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
                                horizontalDropped = (Map.ActualWidth / 2);
                                verticalDropped = (Map.ActualHeight / 2);
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

                                double horizontalToBorder = Math.Min(horizontalFixed, (Map.ActualWidth - horizontalFixed));
                                double verticalToBorder = Math.Min(verticalFixed, (Map.ActualHeight - verticalFixed));

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
            //Move label associated with rectangle
            System.Windows.Controls.Label l = new Label();
            foreach (KeyValuePair<int, Team> t in Globals.listOfTeams)
            {
                if (t.Value.getRectangle() == r)
                    l = t.Value.getLabel();
            }
            Canvas.SetLeft(l, (horizontalDropped - shapeRadius));
            Canvas.SetTop(l, (verticalDropped - shapeRadius));
        }

        //Method to visually drag the item selected and insuring it doesn't go outside of the map
        private void team_MouseMove(object sender, MouseEventArgs e)
        {
            //If no rectangle are clicked, exit method
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(Map);
            System.Windows.Shapes.Rectangle r = sender as System.Windows.Shapes.Rectangle;
            System.Windows.Controls.Label l = new Label();
            foreach(KeyValuePair<int, Team> t in Globals.listOfTeams)
            {
                if(t.Value.getRectangle() == r)
                    l = t.Value.getLabel();
            }

            //Handling exception where fixed rectangle gets moved when another rectangle is dropped on it
            if (!r.Name.Equals(movingRectangle))
            {
                return;
            }

            //Making sure it is not dragged out of bounds
            if (mousePos.X > (Map.ActualWidth - shapeRadius) || mousePos.Y > (Map.ActualHeight - shapeRadius) || mousePos.X < shapeRadius || mousePos.Y < shapeRadius)
            {
                return;
            }

            Canvas.SetLeft(r, (mousePos.X - shapeRadius));
            Canvas.SetTop(r, (mousePos.Y - shapeRadius));
            Canvas.SetLeft(l, (mousePos.X - shapeRadius));
            Canvas.SetTop(l, (mousePos.Y - shapeRadius));
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
        public void TeamDisplay( )
        {
			Team team = Globals.listOfTeams[Globals.currentTeam - 1];
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);
            System.Windows.Media.Color randomColor = System.Windows.Media.Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);

			//Team
			Label TeamNameLabel = new Label();
			TeamNameLabel.Name = "TeamNameLabel";
            TeamNameLabel.Width = 188;
            TeamNameLabel.Height = 40;
			TeamNameLabel.FontSize = 20;
			TeamNameLabel.VerticalContentAlignment = VerticalAlignment.Center;
			TeamNameLabel.Content = "Team " + Globals.listOfTeams[Globals.currentTeam-1].getName();
            TeamNameLabel.Foreground = new SolidColorBrush(Colors.Black);
            TeamNameLabel.Background = new SolidColorBrush(randomColor);
            TeamNameLabel.BorderBrush = System.Windows.Media.Brushes.Black;
            TeamNameLabel.BorderThickness = new Thickness(0, 1, 0, 1);

            Canvas.SetLeft(TeamNameLabel, 0);
            Canvas.SetTop(TeamNameLabel, TeamNameLabelPosition);
            Team_display.Children.Add(TeamNameLabel);

			//Members
            ImageBrush imgb = new ImageBrush();
            int NumberOfLevel0Members = 0;
            int NumberOfLevel1Members = 0;
            int NumberOfLevel2Members = 0;
			int pos = 0;
			TeamMember member;
			while((member = team.getMember(pos++)) != null)
			{
				Label newMember = new Label();
				newMember.Name = "TeamMember" + pos;
                newMember.Width = 188;
				newMember.Height = 30;
				newMember.FontSize = 16;
				newMember.VerticalContentAlignment = VerticalAlignment.Center;
				newMember.Foreground = new SolidColorBrush(Colors.Black);
				//newMember.Background = new SolidColorBrush(Colors.Aqua);
				newMember.BorderBrush = System.Windows.Media.Brushes.Black;
				switch(member.getTrainingLevel())
				{
					case 0:
						newMember.Tag = "first_aid";
                        NumberOfLevel0Members++;
						break;
					case 1:
						newMember.Tag = "first_responder";
                        NumberOfLevel1Members++;
						break;
					case 2:
						newMember.Tag = "medicine";
                        NumberOfLevel2Members++;
						break;
				}

                if (NumberOfLevel0Members >= NumberOfLevel1Members )
                {
                    imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\First_Aid2.png"));
                }
                else if (NumberOfLevel1Members >= NumberOfLevel0Members)
                {
                    imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\First_Responder2.png"));
                }
                else
                {
                    imgb.ImageSource = new BitmapImage(new Uri(AbsolutePath + @"Icons\Medicine2.png"));
                }

				newMember.Content = member.getName();
				Canvas.SetLeft(newMember, 0);
				Canvas.SetTop(newMember, TeamMemberPosition);
				LevelOfTraining(newMember);
				TeamMemberPosition += 30;
                Team_display.Children.Add(newMember);
			}

			TeamNameLabelPosition = TeamMemberPosition + 15;
			TeamMemberPosition += 90;
			TeamNumberPosition++;

            System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
            r.Name = "team_" + rectangleCtr++;
            r.Width = shapeRadius * 2;
            r.Height = shapeRadius * 2;
            r.Stroke = new SolidColorBrush(Colors.Black);
            
            r.Fill = imgb;
            r.MouseLeftButtonDown += new MouseButtonEventHandler(team_MouseLeftButtonDown);
            r.MouseLeftButtonUp += new MouseButtonEventHandler(team_MouseLeftButtonUp);
            r.MouseMove += new MouseEventHandler(team_MouseMove);

            Label l = new Label(); 
            l.Content = team.getName();
            l.Width = shapeRadius * 2;
            l.Height = shapeRadius * 2;
            l.Margin = new Thickness(0);
            l.Padding = new Thickness(0);
            l.FontWeight = FontWeights.DemiBold;
            System.Windows.Media.Effects.DropShadowEffect shadow = new System.Windows.Media.Effects.DropShadowEffect();
            shadow.ShadowDepth = 3;
            shadow.Direction = 315;
            shadow.Opacity = 1.0;
            shadow.BlurRadius = 3;
            shadow.Color = Colors.White;
            l.Effect = shadow;

            switch(l.Content.ToString().Length)
            {
                case 1: l.FontSize = 28;
                    break;
                case 2: l.FontSize = 24;
                    break;
                case 3: l.FontSize = 20;
                    break;
                case 4: l.FontSize = 16;
                    break;
                case 5: l.FontSize = 12;
                    break;
                case 6: l.FontSize = 12;
                    break;
                default: l.FontSize = 16;
                    break;
            }
            l.HorizontalContentAlignment = HorizontalAlignment.Center;
            l.VerticalContentAlignment = VerticalAlignment.Center;
            l.IsHitTestVisible = false;
            Canvas.SetTop(r, 0);
            Canvas.SetLeft(r, 0);
            Canvas.SetTop(l, 0);
            Canvas.SetLeft(l, 0);
            Map.Children.Add(r);
            Map.Children.Add(l);
            Globals.listOfTeams[Globals.currentTeam - 1].setRectangle(r);
            Globals.listOfTeams[Globals.currentTeam - 1].setLabel(l);

            label_Click();
        }

        //Add equipment menu
        private void label_Click()
        {

            //add equipment by right clicking
            ContextMenu mnuContextMenu = new ContextMenu();
            this.ContextMenu = mnuContextMenu;
            Team_display.ContextMenu=mnuContextMenu;

         
            mnuContextMenu.Width = 205;
            mnuContextMenu.Height = 150;
            MenuItem AmbulanceCart = new MenuItem();
            MenuItem MountedStretcher = new MenuItem();
            MenuItem SittingCart = new MenuItem();
            MenuItem TransportStretcher = new MenuItem();
            MenuItem WheelChair = new MenuItem();
            MenuItem Epipen = new MenuItem();


            AmbulanceCart.Click += new RoutedEventHandler(loadEquipment);
            AmbulanceCart.Name = "Ambulance_Cart";
            AmbulanceCart.Tag = "Ambulance_Cart";
            AmbulanceCart.Header = "Add Ambulance Cart";


            MountedStretcher.Click += new RoutedEventHandler(loadEquipment);
            MountedStretcher.Name = "Mounted_stretcher";
            MountedStretcher.Tag = "Mounted_stretcher";
            MountedStretcher.Header = "Add Mounted stretcher";

            SittingCart.Click += new RoutedEventHandler(loadEquipment);
            SittingCart.Name = "Sitting_Cart";
            SittingCart.Tag = "Sitting_Cart";
            SittingCart.Header = "Add Sitting Cart";

            TransportStretcher.Click += new RoutedEventHandler(loadEquipment);
            TransportStretcher.Name = "Transport_Stretcher";
            TransportStretcher.Tag = "Transport_Stretcher";
            TransportStretcher.Header = "Add Transport Stretcher";

            WheelChair.Click += new RoutedEventHandler(loadEquipment);
            WheelChair.Name = "WheelChair";
            WheelChair.Tag = "WheelChair";
            WheelChair.Header = "Add WheelChair";

            Epipen.Click += new RoutedEventHandler(loadEquipment);
            Epipen.Name = "Epipen";
            Epipen.Tag = "Epipen";
            Epipen.Header = "Add Epipen";

            mnuContextMenu.Items.Add(AmbulanceCart);
            mnuContextMenu.Items.Add(MountedStretcher);
            mnuContextMenu.Items.Add(SittingCart);
            mnuContextMenu.Items.Add(TransportStretcher);
            mnuContextMenu.Items.Add(WheelChair);
            mnuContextMenu.Items.Add(Epipen);


        }

        //Displaying Equipment
        void loadEquipment(object sender, RoutedEventArgs e)
        {
            MenuItem AmbulanceCart = (MenuItem)sender;
            MenuItem MountedStretcher = (MenuItem)sender;
            MenuItem WheelChair = (MenuItem)sender;
            MenuItem TransportStretcher = (MenuItem)sender;
            MenuItem SittingCart = (MenuItem)sender;
            MenuItem Epipen = (MenuItem)sender;
    
            //limits the number of equipments the user can add
            if (NumberOfEquipment > 5)
            {
                MessageBox.Show("A Team can only hold 6 types of equipment!");
                AmbulanceCart.IsEnabled = false;
                MountedStretcher.IsEnabled = false;
                WheelChair.IsEnabled = false;
                TransportStretcher.IsEnabled = false;
                SittingCart.IsEnabled = false;

            }

            else
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

                //AmbulanceCart
                if (AmbulanceCart.Name == "Ambulance_Cart" )
                {
                    myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\AmbulanceCart.png");
                }

                //SittingCart
                if (SittingCart.Name == "Sitting_Cart")
                {
					myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\SittingCart.png");
                }


                //MountedStretcher
                if (MountedStretcher.Name == "Mounted_stretcher")
                {
					myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\MountedStretcher.png");
                }

                //TransportStretcher
                if (TransportStretcher.Name == "Transport_Stretcher")
                {
					myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\TransportStretcher.png");
                }

                //WheelChair
                if (WheelChair.Name == "WheelChair")
                {
					myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\WheelChair.png");
                }


                //epipen
                if (Epipen.Name == "Epipen")
                {
					myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\Epipen.png");
                }

                myBitmapImage.DecodePixelWidth = 25;
                myBitmapImage.EndInit();
                myImage.Source = myBitmapImage;
                Team_display.Children.Add(myImage);
                iconPositionLeft += 26;
                NumberOfEquipment++;
            }
			


        }

        //display level of training
        private void LevelOfTraining(Label Member)
        {
            // Create Image Element
            System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            myImage.Width = 30;
            myImage.Height = 30;

            //position icon
            Canvas.SetLeft(myImage, LevelofTrainingPositionLeft);

            // Create image source
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();

            //first aid
            if (Member.Tag.ToString().Equals("first_aid"))
            {
				myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\First_Aid2.png");
                Canvas.SetTop(myImage, TeamMemberPosition);
            }

            //first responder
            if (Member.Tag.ToString().Equals("first_responder"))
            {
				myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\First_Responder2.png");
                Canvas.SetTop(myImage, TeamMemberPosition);
            }

            //Medicine
            if (Member.Tag.ToString().Equals("medicine"))
            {
				myBitmapImage.UriSource = new Uri(AbsolutePath + @"Icons\Medicine2.png");
                Canvas.SetTop(myImage, TeamMemberPosition);
            }
            myBitmapImage.DecodePixelWidth = 30;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;
            Team_display.Children.Add(myImage);
        }

		public System.Windows.HorizontalAlignment Center { get; set; }
	}
}
