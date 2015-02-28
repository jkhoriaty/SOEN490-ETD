using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ETD.Models.ArchitecturalObjects
{
	public abstract class Observable
	{
		/*
		 * A view can either be interested in the creation and deletion of an instance of an object (static interest)
		 * or it can be interested in one of the instances.
		 * E.g.: The TeamSection is interested in the creation and deletion of Team instances (and that's when it redraws itself
		 * to reflect the actual list of teams) but a TeamInfo is only interested in one Team (and when it's status or equipment list is changed)
		 */

		private static Dictionary<Type, List<Observer>> classObserverList = new Dictionary<Type, List<Observer>>(); //Where the Type is the class that the observer is interested in

		public static void RegisterClassObserver(Type type, Observer observer)
		{
			if (!classObserverList.ContainsKey(type))
			{
				classObserverList.Add(type, new List<Observer>());
			}
			classObserverList[type].Add(observer);
		}

		protected static void ClassModifiedNotification(Type type)
		{
			foreach (Observer observer in classObserverList[type])
			{
				observer.Update();
			}
		}

		public static void DeregisterClassObserver(Type type, Observer observer)
		{
			classObserverList[type].Remove(observer);
		}

		private List<Observer> instanceObserverList = new List<Observer>();

		public void RegisterInstanceObserver(Observer observer)
		{
			instanceObserverList.Add(observer);
		}

		protected void InstanceModifiedNotification()
		{
			foreach (Observer observer in instanceObserverList)
			{
				observer.Update();
			}
		}

		public void DeregisterInstanceObserver(Observer observer)
		{
			instanceObserverList.Remove(observer);
		}
	}
}
