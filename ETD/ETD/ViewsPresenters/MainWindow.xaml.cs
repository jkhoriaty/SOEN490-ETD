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
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.TimersInterventionForm;
using System.Windows.Threading;
using System.Drawing;

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
        private int AddtionalInfoSize;
        private bool isdrawing = false;
		private double previousWidth;
		private double previousHeight;

		public MainWindow()
		{
			InitializeComponent();
			teamsSection = new TeamsSectionPage(this);
			mapSection = new MapSectionPage(this);
			interventionsSection = new InterventionSectionPage(this);
            AIPmapSection = new AdditionalInfoPage(this);

            //save map content on window close
            this.Closed += new EventHandler(WindowClosed);

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
		}

        //window closed
        public void WindowClosed(object sender, System.EventArgs e)
        {
            MessageBox.Show("Saving map..");

           // Absolute path doesnt work..
           // Saving to desktop directory for now
           // String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
           // String Filename = @"\Maps\test.png";
           // String test = AbsolutePath + Filename;
           // MessageBox.Show((AbsolutePath + Filename).ToString());

            Rect AIbounds = VisualTreeHelper.GetDescendantBounds(AIPmapSection);
            Rect Mapbounds = VisualTreeHelper.GetDescendantBounds(mapSection);
            var AIFileName = "AIInfo_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var MapFileName = "Map_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var MergedMapName = "ModMap_" + DateTime.Now.ToString("yyyyMMdd_hhss");

            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
       
            MessageBox.Show("mapbounds:"+ Mapbounds.ToString());
            MessageBox.Show("Aibounds:" + AIbounds.ToString());

            double dpi = 96d;
            if (AIbounds.ToString() != "Empty")
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
                    VisualBrush vb2 = new VisualBrush(mapSection.Map);
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
                    
                    System.IO.File.WriteAllBytes(desktopFolder + "/" + AIFileName + ".png", ms.ToArray());
                    System.IO.File.WriteAllBytes(desktopFolder + "/" + MapFileName + ".png", ms2.ToArray());

                    System.Drawing.Image img1 = System.Drawing.Image.FromFile(desktopFolder + "/" + AIFileName + ".png");
                    System.Drawing.Image img2 = System.Drawing.Image.FromFile(desktopFolder + "/" + MapFileName + ".png");
                    String FinalImage = desktopFolder + "/" + MergedMapName + ".png";

                    int width = img2.Width;
                    int height =  img2.Height;

                    Bitmap img3 = new Bitmap(width, height);
                    Graphics g = Graphics.FromImage(img3);

                   // g.DrawImage(img2, new System.Drawing.Rectangle(new System.Drawing.Point(), img2.Size), new System.Drawing.Rectangle(new System.Drawing.Point(), img2.Size), GraphicsUnit.Pixel);
                   // g.DrawImage(img1, new System.Drawing.Rectangle(new System.Drawing.Point(0, img2.Height + 1), img1.Size),
                   //               new System.Drawing.Rectangle(new System.Drawing.Point(), img1.Size), GraphicsUnit.Pixel);
                   // g.DrawImage(img1, new System.Drawing.Point(img1.Width, 0));
                  g.DrawImage(img2,new System.Drawing.Point(0,0));
                  g.DrawImage(img1, new System.Drawing.Point(0, 0));
                   g.Dispose();
                    img1.Dispose();
                   img2.Dispose();

                    img3.Save(FinalImage, System.Drawing.Imaging.ImageFormat.Png);
                     img3.Dispose();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
    

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

				mapSection.SetMap(coloredImage);
			}
		}

		//Called after successful submission and processing of the team form
		public void CreateTeamPin(Team team)
		{
			mapSection.CreateTeamPin(team);
		}

		//Click: Create any type equipement
		private void CreateEquipmentPin(object sender, RoutedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)EquipmentAdd.SelectedItem;
			if (selectedItem != null)
			{
				mapSection.CreateEquipmentPin("" + selectedItem.Name);
			}
			else
			{
				MessageBox.Show("You need to select an equipment to add!");
			}
		}

		//Recreating equipment after removal from the team
		public void CreateEquipmentPin(String equipmentName)
		{
			mapSection.CreateEquipmentPin(equipmentName);
		}

		//Creating intervention pin
		public void CreateInterventionPin(int interventionNumber)
		{
			mapSection.CreateInterventionPin(interventionNumber);
		}

		//Deleting pin using its name (e.g. when a team is deleted)
		public void DeletePin(String pinName)
		{
			mapSection.DeletePin(pinName);
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
				TimersInterventionFormPage.interventionDeadline = interventionDeadline;
				TimersInterventionFormPage.movingDeadline = movingDeadline;
				MessageBox.Show("The deadlines have been changed.");
			}
		}

        //Create additional shapes on the map
        public void CreateAdditionnalInfoPin(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox parent = item.Parent as ComboBox;
            foreach (ComboBoxItem mi in parent.Items)
            {
                if (mi!=null && mi.IsSelected )
                {
                    AIPmapSection.CreateAdditionnalInfoPin("" + mi.Name, AddtionalInfoSize);
                    isdrawing = false;
                }
            }  
        }
        
        public void CreateAdditionnalInfoPin(String AI,int size)
        {
            AIPmapSection.CreateAdditionnalInfoPin(AI,size);
        }

        //delete additional pins
        public void AIDeletePin(object sender, RoutedEventArgs e)
        {
            AIPmapSection.AIDeletePin(sender, e);
        }

        //switch between Regular mode and Edit mode
        private void ModeChange(object sender, RoutedEventArgs e)
		{
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox parent = item.Parent as ComboBox;
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

   
   
	}
}
