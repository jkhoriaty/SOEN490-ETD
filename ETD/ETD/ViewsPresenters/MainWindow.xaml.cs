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
using ETD.ViewsPresenters.TeamsSection;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters.InterventionsSection;
using ETD.Models.Objects;
using System.Windows.Threading;
using System.Drawing;
using ETD.ViewsPresenters.ScheduleSection;
using ETD.Services;
using System.Threading;
using System.Windows.Controls.Primitives;
using ETD.CustomObjects.PopupForms;


namespace ETD.ViewsPresenters
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow
	{
		private TeamsSectionPage teamsSection;
		private MapSectionPage mapSection;
		private InterventionSectionPage interventionsSection;
        private AdditionalInfoPage AIPmapSection;
        private ScheduleSectionPage ScheduleSection;
        private FollowUpSectionForm FollowupSection;

        private bool isdrawing = false;
		private double previousWidth;
		private double previousHeight;

		private Dictionary<String, String> registeredVolunteers = new Dictionary<String, String>();

		public MainWindow()
		{
			InitializeComponent();
			FormPopup.RegisterMainWindow(this);

			teamsSection = new TeamsSectionPage(this);
			mapSection = new MapSectionPage(this);
			interventionsSection = new InterventionSectionPage(this);
            AIPmapSection = new AdditionalInfoPage(this);
            //ScheduleSection = new ScheduleSectionPage(this);

			previousWidth = MapSection.ActualWidth;
			previousHeight = MapSection.ActualHeight;

			//Populating the Teams section
			Frame teamsFrame = new Frame();
			teamsFrame.Content = teamsSection;
			TeamsSection.Child = teamsFrame;

			//Populating the Map section
			Frame mapFrame = new Frame();
			mapFrame.Content = mapSection;
			MapSection.Child = mapFrame;

			//Populating the Interventions section
			Frame interventionsFrame = new Frame();
			interventionsFrame.Content = interventionsSection;
			InterventionsSection.Child = interventionsFrame;


            //Populating the AI section
            Frame AIFrame = new Frame();
            AIFrame.Content = AIPmapSection;
            AIPSection.Child = AIFrame;

            //Populating the Schedule section
            /*Frame ScheduleFrame = new Frame();
            ScheduleFrame.Content = ScheduleSection;
            MapSection.Child = ScheduleFrame;*/

            
		}

		//Ping server to test connection and update registed volunteers - Executes every 10 seconds
		public void refresh(object sender, EventArgs e)
		{
			UpdateRegistered();
		}


        //window closed
        public void WindowClosed(object sender, System.EventArgs e)
        {

            //TechnicalServices.saveMap(AIPmapSection, mapSection);

            /*
            //MessageBox.Show("Saving map..");

           // Absolute path doesnt work..
           // Saving to desktop directory for now
            String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            String Filename = @"\Maps\test.png";
            String test = AbsolutePath + Filename;
           // MessageBox.Show((AbsolutePath + Filename).ToString());
         
            Rect AIbounds = VisualTreeHelper.GetDescendantBounds(AIPmapSection);
            Rect Mapbounds = VisualTreeHelper.GetDescendantBounds(mapSection);
            var AIFileName = "AIInfo_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var MapFileName = "Map_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var MergedMapName = "ModMap_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
       
           // MessageBox.Show("mapbounds:"+ Mapbounds.ToString());
           // MessageBox.Show("Aibounds:" + AIbounds.ToString());

            double dpi = 96d;
            if (AIbounds.ToString() != "Empty" && Mapbounds.ToString() != "Empty")
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)AIbounds.Width, (int)AIbounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
                RenderTargetBitmap rtb2 = new RenderTargetBitmap((int)Mapbounds.Width, (int)Mapbounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);

                //ai
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(AIPmapSection.AdditionalMap);
                    dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), AIbounds.Size));
                }

                //map
                DrawingVisual dv2 = new DrawingVisual();
                using (DrawingContext dc2 = dv2.RenderOpen())
                {
                    VisualBrush vb2 = new VisualBrush(mapSection.Canvas_map);
                    dc2.DrawRectangle(vb2, null, new Rect(new System.Windows.Point(), Mapbounds.Size));
                }

                //ai
                rtb.Render(dv);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                //map
                rtb2.Render(dv2);
                BitmapEncoder pngEncoder2 = new PngBitmapEncoder();
                pngEncoder2.Frames.Add(BitmapFrame.Create(rtb2));

                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    System.IO.MemoryStream ms2 = new System.IO.MemoryStream();

                    pngEncoder.Save(ms);
                    ms.Close();
                    pngEncoder2.Save(ms2);
                    ms2.Close();
                    
                    System.IO.File.WriteAllBytes(desktopFolder  + AIFileName + ".png", ms.ToArray());
                    System.IO.File.WriteAllBytes(desktopFolder  + MapFileName + ".png", ms2.ToArray());

                    System.Drawing.Image AIimg = System.Drawing.Image.FromFile(desktopFolder  + AIFileName + ".png");
                    System.Drawing.Image Mapimg = System.Drawing.Image.FromFile(desktopFolder  + MapFileName + ".png");
                    String FinalImage = desktopFolder  + MergedMapName + ".png";

                    int width = Mapimg.Width;
                    int height = Mapimg.Height;

                    Bitmap FinalImg = new Bitmap(width, height);
                    Graphics g = Graphics.FromImage(FinalImg);

                    g.DrawImage(Mapimg, new System.Drawing.Point(0, 0));
                    g.DrawImage(AIimg, new System.Drawing.Point(0, 0));
                    g.Dispose();
                    AIimg.Dispose();
                    Mapimg.Dispose();

                    FinalImg.Save(FinalImage, System.Drawing.Imaging.ImageFormat.Png);
                    FinalImg.Dispose();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            */
        }
         


		//Window size or state changed - Adjusting the team section height
		public void setSectionsHeight(object sender, EventArgs e)
		{
			teamsSection.setTeamsSectionHeight(TeamsSection);
			interventionsSection.setInterventionsSectionWidth(InterventionsSection);

			mapSection.movePins((MapSection.ActualWidth / previousWidth), (MapSection.ActualHeight / previousHeight));

			previousWidth = MapSection.ActualWidth;
			previousHeight = MapSection.ActualHeight;
		}

		//Click: Load Map
		private void LoadMap(object sender, RoutedEventArgs e)
		{
			//Initiating and displaying of the dialog
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg; *.png; *.gif";
			openFileDialog.FilterIndex = 1;

			//Getting the selected image
			BitmapImage coloredImage = null;
			if (openFileDialog.ShowDialog() == true)
			{
				System.IO.FileInfo File = new System.IO.FileInfo(openFileDialog.FileName);
				coloredImage = new BitmapImage(new Uri(openFileDialog.FileName));

				mapSection.setMap(coloredImage);
			}
		}

		//Click: Create any type equipement
		private void CreateEquipmentPin(object sender, RoutedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)EquipmentAdd.SelectedItem;
			if (selectedItem != null)
			{
				new Equipment("" + selectedItem.Name);
			}
			else
			{
				MessageBox.Show("You need to select an equipment to add!");
			}
		}

		//Add equipment to team
		public void AddTeamEquipment(Equipment equip, String teamName)
		{
			teamsSection.AddTeamEquipment(equip, teamName);
		}

        //Change intervention deadlines
		private void ChangeDeadlines(object sender, RoutedEventArgs e)
		{
			bool success = true;
			int interventionDeadline = 0;
			int movingDeadline = 0;
			try
			{
				interventionDeadline = Int32.Parse(InterventionDeadline.Text);
				movingDeadline = Int32.Parse(MovingDeadline.Text);
				if (interventionDeadline < 0 || movingDeadline < 0)
				{
					success = false;
				}
			}
			catch (Exception ex)
			{
				success = false;
			}

			if(success == false)
			{
				MessageBox.Show("The intervention deadlines should be numbers!");
			}
			else
			{
				InterventionSectionPage.setInterventionDeadline(interventionDeadline);
				InterventionSectionPage.setMovingDeadline(movingDeadline);
				MessageBox.Show("The deadlines have been changed.");
			}
		}

        //Create additional shapes on the map
        public void CreateMapModificationPin(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox parent = item.Parent as ComboBox;
            foreach (ComboBoxItem mi in parent.Items)
            {
                if (mi!=null && mi.IsSelected )
                {
                    AIPmapSection.createMapModificationPin("" + mi.Name);
                    isdrawing = false;
                }
            }  
        }

        /*
        public void CreateAdditionnalInfoPin(String AI,int size)
        {
            AIPmapSection.CreateAdditionnalInfoPin(AI,size);
        }
        

       // delete additional pins
        public void AIDeletePin(object sender, RoutedEventArgs e)
        {
          //  AIPmapSection.AIDeletePin(sender, e);
        }
        */

        //switch between Regular mode and Edit mode
        private void ModeChange(object sender, RoutedEventArgs e)
		{
			ComboBoxItem item = sender as ComboBoxItem;
			ComboBox parent = modeCB;
            foreach (ComboBoxItem mi in parent.Items)
            {  
                if (mi.Content.Equals("Regular Mode") && (mi.IsSelected))
                {
                    AI.Visibility = Visibility.Collapsed;
                    AIPmapSection.IsEnabled = false;
                }
                else if (mi.Content.Equals("Edit Mode") && (mi.IsSelected ))
                {
                    AI.Visibility = Visibility.Visible;
                    AIPmapSection.IsEnabled = true;
                }
            }  
		}

		internal void AddResource(String teamName, String interventionName)
		{
			interventionsSection.AddResource(teamName, interventionName);
		}

		internal void ReportArrival(String teamName, String interventionName)
		{
			interventionsSection.ReportArrival(teamName, interventionName);
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
            mapSection.ReportArrived(interventionName, rowNumber);
        }
        
        /*internal void UpdateSectors()
        {
            ScheduleSection.UpdateSectors();
        }*/

        internal void CreateIntervention()
        {
            interventionsSection.CreateIntervention();
        }

		public void PopupLostFocus(object sender, EventArgs e)
		{
			Popup popup = (Popup)sender;
			popup.IsOpen = false;
		}

		private void ShowGPSLocations(object sender, RoutedEventArgs e)
		{
			UpdateRegistered().Wait();
			Dispatcher.Invoke(() =>
			{
				//new FormPopup(this, new RegisteredVolunteersForm(registeredVolunteers));
			});
			newRegisteredCTR.Content = "0";
		}

		//Pinging server for registered volunteers and interpret the return
		private Task UpdateRegistered()
		{
			Task<String[]> UpdateRegisteredTask = new Task<string[]>(NetworkServices.UpdateRegisted);
			UpdateRegisteredTask.ContinueWith(task => UpdateRegisteredResultAnalysis(task.Result));
			UpdateRegisteredTask.Start();
			return UpdateRegisteredTask;
		}

        //Displays follow up section page
        private void ShowFollowUpSection(object sender, RoutedEventArgs e)
        {
            //new FormPopup(this, new FollowUpSectionForm(FollowupSection));
        }

		//Interpret the servers return
		private void UpdateRegisteredResultAnalysis(String[] reply)
		{
			if(reply == null)
			{
				NotifyConnectionFail();
			}
			else if(reply.Length > 0)
			{
				NotifyConnectionSuccess();
				int newCtr = 0;
				for (int i = 1; i < (reply.Length - 1); i++)
				{
					String[] volunteerInfo = reply[i].Split('|');
					if (!registeredVolunteers.ContainsKey(volunteerInfo[0]))
					{
						registeredVolunteers.Add(volunteerInfo[0], volunteerInfo[1]);
						newCtr++;
					}
					else
					{
						registeredVolunteers[volunteerInfo[0]] = volunteerInfo[1];
					}
				}
				Dispatcher.Invoke(() => newRegisteredCTR.Content = "" + newCtr);
			}
		}

		//UI work to notify of connection success
		private void NotifyConnectionSuccess()
		{
			Dispatcher.Invoke(() => 
			{
				GPSLocationsTextBlock.Background = new SolidColorBrush(Colors.Green);
				GPSLocationsTextBlock.Foreground = new SolidColorBrush(Colors.White);
				GPSLocationsTextBlock.IsEnabled = true;
			});
		}

		//UI work to notify of connection failure
		private void NotifyConnectionFail()
		{
			Dispatcher.Invoke(() => 
			{
				GPSLocationsTextBlock.Background = new SolidColorBrush(Colors.Red);
				GPSLocationsTextBlock.Foreground = new SolidColorBrush(Colors.White);
				GPSLocationsTextBlock.IsEnabled = false;
			});
		}
    }

}
