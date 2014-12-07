using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ETD.Models
{
	static class Services
	{
		private static String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

		private static Dictionary<equipments, String> equipmentRelPath = new Dictionary<equipments, String>
		{
			{equipments.ambulanceCart, @"\Icons\AmbulanceCart.png"},
			{equipments.epipen, @"\Icons\epipen.png"},
			{equipments.mountedStretcher, @"\Icons\MountedStretcher.png"},
			{equipments.sittingCart, @"\Icons\SittingCart.png"},
			{equipments.transportStretcher, @"\Icons\TransportStretcher.png"},
			{equipments.wheelchair, @"\Icons\WheelChair.png"}
		};

		private static Dictionary<trainings, String> trainingRelPath = new Dictionary<trainings, String>
		{
			{trainings.firstAid, @"\Icons\First_Aid3.png"},
			{trainings.firstResponder, @"\Icons\First_Responder2.png"},
			{trainings.medicine, @"\Icons\Medicine.png"}
		};

		private static Dictionary<statuses, String> firstAidPinRelPath = new Dictionary<statuses, String>
		{
			{statuses.available, @"\Icons\FirstAid_available.png"},
			{statuses.moving, @"\Icons\FirstAid_moving.png"},
			{statuses.intervening, @"\Icons\FirstAid_intervening.png"},
			{statuses.busy, @"\Icons\FirstAid_busy.png"}
		};

		private static Dictionary<statuses, String> firstResponderPinRelPath = new Dictionary<statuses, String>
		{
			{statuses.available, @"\Icons\FirstResponder_available.png"},
			{statuses.moving, @"\Icons\FirstResponder_moving.png"},
			{statuses.intervening, @"\Icons\FirstResponder_intervening.png"},
			{statuses.busy, @"\Icons\FirstResponder_busy.png"}
		};

		private static Dictionary<statuses, String> medicinePinRelPath = new Dictionary<statuses, String>
		{
			{statuses.available, @"\Icons\Medicine_available.png"},
			{statuses.moving, @"\Icons\Medicine_moving.png"},
			{statuses.intervening, @"\Icons\Medicine_intervening.png"},
			{statuses.busy, @"\Icons\Medicine_busy.png"}
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
		public static BitmapImage getImage(equipments equipment)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + equipmentRelPath[equipment]));
			return img;
		}

		//Return image of training
		public static BitmapImage getImage(trainings training)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + trainingRelPath[training]));
			return img;
		}

		public static BitmapImage getImage(Team team, statuses status)
		{
			trainings teamTraining = team.getHighestLevelOfTraining();

			BitmapImage img = null;
			if(teamTraining == trainings.firstAid)
			{
				img = new BitmapImage(new Uri(AbsolutePath + firstAidPinRelPath[status]));
			}
			else if (teamTraining == trainings.firstResponder)
			{
				img = new BitmapImage(new Uri(AbsolutePath + firstResponderPinRelPath[status]));
			}
			else if (teamTraining == trainings.medicine)
			{
				img = new BitmapImage(new Uri(AbsolutePath + medicinePinRelPath[status]));
			}

			return img;
		}

		//Return the full word of the phonetic letter
		public static String getPhoneticLetter(String letter)
		{
			return alphabet[letter];
		}
	}
}
