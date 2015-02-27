using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.ArchitecturalObjects
{
	public class Observable
	{
		private static List<Observer> observerList = new List<Observer>();

		public static void RegisterObserver(Observer observer)
		{
			observerList.Add(observer);
		}

		protected static void NotifyAll()
		{
			foreach(Observer observer in observerList)
			{
				observer.Update();
			}
		}

		public static void DeregisterObserver(Observer observer)
		{
			observerList.Remove(observer);
		}
	}
}
