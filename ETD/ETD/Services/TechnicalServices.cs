using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ETD.Models.Objects;
using ETD.ViewsPresenters.MapSection;
using System.Drawing;
using System.Windows;
using System.Windows.Media;


namespace ETD.Services
{
	static class TechnicalServices
	{
		private static String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        //Sets the image path for each equipment 
		private static Dictionary<Equipments, String> equipmentRelPath = new Dictionary<Equipments, String>
		{
			{Equipments.ambulanceCart, @"\Icons\AmbulanceCart.png"},
			{Equipments.epipen, @"\Icons\epipen.png"},
			{Equipments.mountedStretcher, @"\Icons\MountedStretcher.png"},
			{Equipments.sittingCart, @"\Icons\SittingCart.png"},
			{Equipments.transportStretcher, @"\Icons\TransportStretcher.png"},
			{Equipments.wheelchair, @"\Icons\WheelChair.png"}
		};

        //Sets the image path for each training levels 
		private static Dictionary<Trainings, String> trainingRelPath = new Dictionary<Trainings, String>
		{
			{Trainings.firstAid, @"\Icons\First_Aid3.png"},
			{Trainings.firstResponder, @"\Icons\First_Responder2.png"},
			{Trainings.medicine, @"\Icons\Medicine.png"}
		};

        //Sets the image path for the status of the first aid pin
		private static Dictionary<Statuses, String> firstAidPinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\FirstAid_available.png"},
			{Statuses.moving, @"\Icons\FirstAid_moving.png"},
			{Statuses.intervening, @"\Icons\FirstAid_intervening.png"},
			{Statuses.unavailable, @"\Icons\FirstAid_unavailable.png"}
		};

        //Sets the image path for the status of the first responder pin
		private static Dictionary<Statuses, String> firstResponderPinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\FirstResponder_available.png"},
			{Statuses.moving, @"\Icons\FirstResponder_moving.png"},
			{Statuses.intervening, @"\Icons\FirstResponder_intervening.png"},
			{Statuses.unavailable, @"\Icons\FirstResponder_unavailable.png"}
		};

        //Sets the image path for the status of the medicine pin
		private static Dictionary<Statuses, String> medicinePinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\Medicine_available.png"},
			{Statuses.moving, @"\Icons\Medicine_moving.png"},
			{Statuses.intervening, @"\Icons\Medicine_intervening.png"},
			{Statuses.unavailable, @"\Icons\Medicine_unavailable.png"}
		};

        //Sets the image path for each intervntion
		private static Dictionary<String, String> generalRelPath = new Dictionary<String, String>
		{
			{"intervention", @"\Icons\InterventionIcon.png"}
		};

        //Sets the image path for each map modification 
        private static Dictionary<MapMods, String> MapModPath = new Dictionary<MapMods, String>
        {
            {MapMods.camp, @"\Icons\camp.png"},
            {MapMods.circle, @"\Icons\lineCanvas.png"},
            {MapMods.line, @"\Icons\lineCanvas.png"},
            {MapMods.rectangle, @"\Icons\lineCanvas.png"},
            {MapMods.square, @"\Icons\lineCanvas.png"},
            {MapMods.stairs, @"\Icons\stairs.png"},
            {MapMods.ramp, @"\Icons\Ramp.png"}
        };

        //Sets the acronym for a team
		private static Dictionary<String, String> alphabet = new Dictionary<String, String>
		{
			{"A", "Alpha"},
			{"B", "Bravo"},
			{"C", "Charlie"},
			{"D", "Delta"},
			{"E", "Echo"},
			{"F", "Foxtrot"},
			{"G", "Golf"},
			{"H", "Hotel"},
			{"I", "India"},
			{"J", "Juliette"},
			{"K", "Kilo"},
			{"L", "Lima"},
			{"M", "Mike"},
			{"N", "November"},
			{"O", "Oscar"},
			{"P", "Papa"},
			{"Q", "Quebec"},
			{"R", "Romeo"},
			{"S", "Sierra"},
			{"T", "Tango"},
			{"U", "Uniform"},
			{"V", "Victor"},
			{"W", "Whiskey"},
			{"X", "X-Ray"},
			{"Y", "Yankee"},
			{"Z", "Zulu"},
		};

    
		//Returns the equipment image
		public static BitmapImage getImage(Equipments equipment)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + equipmentRelPath[equipment]));
			return img;
		}

		//Returns the level of training image
		public static BitmapImage getImage(Trainings training)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + trainingRelPath[training]));
			return img;
		}

        //Returns the team's image
		public static BitmapImage getImage(Team team, Statuses status)
		{
			Trainings teamTraining = team.getHighestLevelOfTraining();

			BitmapImage img = null;
			if(teamTraining == Trainings.firstAid)
			{
				img = new BitmapImage(new Uri(AbsolutePath + firstAidPinRelPath[status]));
			}
			else if (teamTraining == Trainings.firstResponder)
			{
				img = new BitmapImage(new Uri(AbsolutePath + firstResponderPinRelPath[status]));
			}
			else if (teamTraining == Trainings.medicine)
			{
				img = new BitmapImage(new Uri(AbsolutePath + medicinePinRelPath[status]));
			}

			return img;
		}

        //Returns the image of the intervention pin
		public static BitmapImage getImage(String type)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + generalRelPath[type]));
			return img;
		}

		//Returns the full word of the phonetic letter
		public static String getPhoneticLetter(String letter)
		{
			if(alphabet.ContainsKey(letter))
			{
				return alphabet[letter];
			}
			else
			{
				return letter;
			}
		}

        //Returns the image of the map modification item
        public static BitmapImage getImage(MapMods AI)
        {
            BitmapImage img = new BitmapImage(new Uri(AbsolutePath + MapModPath[AI]));
            return img;
        }

        //The map is saved when the window is closed
        public static void saveMap(AdditionalInfoPage AIPmapSection)
        {
            /* Absolute path doesnt work..
             Saving to desktop directory for now
            String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
             * */

            Rect AIbounds = VisualTreeHelper.GetDescendantBounds(AIPmapSection);
            var AIFileName = "MapModification_" + DateTime.Now.ToString("yyyyMMdd_hhss");
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            double dpi = 96d;
            bool isMapLoaded = AIPmapSection.MapLoaded();

            //If a map was loaded and modifications were made, save the map 
            if (AIbounds.ToString() != "Empty" && isMapLoaded)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)AIbounds.Width, (int)AIbounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();

                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(AIPmapSection.AdditionalMap);
                    dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), AIbounds.Size));
                }

                rtb.Render(dv);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pngEncoder.Save(ms);
                    ms.Close();
                    System.IO.File.WriteAllBytes(desktopFolder + AIFileName + ".png", ms.ToArray());//Save the modified map as an image 
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
	}
}
