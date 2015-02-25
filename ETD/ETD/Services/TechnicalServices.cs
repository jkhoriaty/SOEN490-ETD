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

		private static Dictionary<Equipments, String> equipmentRelPath = new Dictionary<Equipments, String>
		{
			{Equipments.ambulanceCart, @"\Icons\AmbulanceCart.png"},
			{Equipments.epipen, @"\Icons\epipen.png"},
			{Equipments.mountedStretcher, @"\Icons\MountedStretcher.png"},
			{Equipments.sittingCart, @"\Icons\SittingCart.png"},
			{Equipments.transportStretcher, @"\Icons\TransportStretcher.png"},
			{Equipments.wheelchair, @"\Icons\WheelChair.png"}
		};

		private static Dictionary<Trainings, String> trainingRelPath = new Dictionary<Trainings, String>
		{
			{Trainings.firstAid, @"\Icons\First_Aid3.png"},
			{Trainings.firstResponder, @"\Icons\First_Responder2.png"},
			{Trainings.medicine, @"\Icons\Medicine.png"}
		};

		private static Dictionary<Statuses, String> firstAidPinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\FirstAid_available.png"},
			{Statuses.moving, @"\Icons\FirstAid_moving.png"},
			{Statuses.intervening, @"\Icons\FirstAid_intervening.png"},
			{Statuses.unavailable, @"\Icons\FirstAid_unavailable.png"}
		};

		private static Dictionary<Statuses, String> firstResponderPinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\FirstResponder_available.png"},
			{Statuses.moving, @"\Icons\FirstResponder_moving.png"},
			{Statuses.intervening, @"\Icons\FirstResponder_intervening.png"},
			{Statuses.unavailable, @"\Icons\FirstResponder_unavailable.png"}
		};

		private static Dictionary<Statuses, String> medicinePinRelPath = new Dictionary<Statuses, String>
		{
			{Statuses.available, @"\Icons\Medicine_available.png"},
			{Statuses.moving, @"\Icons\Medicine_moving.png"},
			{Statuses.intervening, @"\Icons\Medicine_intervening.png"},
			{Statuses.unavailable, @"\Icons\Medicine_unavailable.png"}
		};

		private static Dictionary<String, String> generalRelPath = new Dictionary<String, String>
		{
			{"intervention", @"\Icons\InterventionIcon.png"}
		};


        private static Dictionary<MapMods, String> MapModPath = new Dictionary<MapMods, String>
        {
            {MapMods.camp, @"\Icons\camp.png"},
            {MapMods.circle, @"\Icons\circle.png"},
            {MapMods.line, @"\Icons\lineCanvas.png"},
            {MapMods.rectangle, @"\Icons\rectangle.png"},
            {MapMods.square, @"\Icons\square.png"},
            {MapMods.stairs, @"\Icons\stairs.png"},
            {MapMods.ramp, @"\Icons\Ramp.png"}
        };

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

    
		//Return image of equipment
		public static BitmapImage getImage(Equipments equipment)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + equipmentRelPath[equipment]));
			return img;
		}

		//Return image of training
		public static BitmapImage getImage(Trainings training)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + trainingRelPath[training]));
			return img;
		}

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

		public static BitmapImage getImage(String type)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + generalRelPath[type]));
			return img;
		}

		//Return the full word of the phonetic letter
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


        //Return image of additionnal info
        public static BitmapImage getImage(MapMods AI)
        {
            BitmapImage img = new BitmapImage(new Uri(AbsolutePath + MapModPath[AI]));
            return img;

        }

        public static void saveMap(AdditionalInfoPage AIPmapSection, MapSectionPage mapSection)
        {
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

                    System.IO.File.WriteAllBytes(desktopFolder + AIFileName + ".png", ms.ToArray());
                    System.IO.File.WriteAllBytes(desktopFolder + MapFileName + ".png", ms2.ToArray());

                    System.Drawing.Image AIimg = System.Drawing.Image.FromFile(desktopFolder + AIFileName + ".png");
                    System.Drawing.Image Mapimg = System.Drawing.Image.FromFile(desktopFolder + MapFileName + ".png");
                    String FinalImage = desktopFolder + MergedMapName + ".png";

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
        }


	}
}
