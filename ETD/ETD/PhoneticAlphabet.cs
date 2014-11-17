using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
	static class PhoneticAlphabet
	{
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

		public static String getLetter(String letter)
		{
			return alphabet[letter];
		}
	}
}
