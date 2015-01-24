using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ETD.Models.Objects;

namespace ETD.Models.Services
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


        private static Dictionary<AdditionalInfos, String> AdditionalInfoPath = new Dictionary<AdditionalInfos, String>
        {
            {AdditionalInfos.camp, @"\Icons\camp.png"},
            {AdditionalInfos.circle, @"\Icons\circle.png"},
            {AdditionalInfos.line, @"\Icons\lineCanvas.png"},
            {AdditionalInfos.rectangle, @"\Icons\rectangle.png"},
            {AdditionalInfos.square, @"\Icons\square.png"},
            {AdditionalInfos.stairs, @"\Icons\stairs.png"},
            {AdditionalInfos.ramp, @"\Icons\Ramp.png"}
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
			return alphabet[letter];
		}


        //Return image of additionnal info
        public static BitmapImage getImage(AdditionalInfos AI)
        {
            BitmapImage img = new BitmapImage(new Uri(AbsolutePath + AdditionalInfoPath[AI]));
            return img;

        }
	}
}
