using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ETD.Models.ArchitecturalObjects
{
    [Serializable()]
	public abstract class Observable
	{
		/*
		 * A view can either be interested in the creation and deletion of an instance of an object (static interest)
		 * or it can be interested in one of the instances.
		 * E.g.: The TeamSection is interested in the creation and deletion of Team instances (and that's when it redraws itself
		 * to reflect the actual list of teams) but a TeamInfo is only interested in one Team (and when it's status or equipment list is changed)
		 */

		//The following section is for observers to register to an object, basically listen for creation and deletion of instances of that object
        [field: NonSerialized()]
        private static Dictionary<Type, List<Observer>> classObserverList; //Where the Type is the class that the observer is interested in

        static Observable()
        {
            classObserverList = new Dictionary<Type, List<Observer>>();
        }

        //Add the new observer to the list of observers
		public static void RegisterClassObserver(Type type, Observer observer)
		{
			if (!classObserverList.ContainsKey(type))
			{
				classObserverList.Add(type, new List<Observer>());
			}
			classObserverList[type].Add(observer);
		}

        //If a change of state occured, notify every registered observers
		protected static void ClassModifiedNotification(Type type)
		{
			if(classObserverList.ContainsKey(type))
			{
				foreach (Observer observer in classObserverList[type])
				{
					observer.Update();
				}
			}
		}

        //Remove an observer from the list of observers
		public static void DeregisterClassObserver(Type type, Observer observer)
		{
			classObserverList[type].Remove(observer);
		}

		//The following is for observers that want to listen to only one instance of that object, e.g. when a team status changes or when a team is assigned to an intervention
        [field: NonSerialized()]
		private List<Observer> instanceObserverList = new List<Observer>();

		public void RegisterInstanceObserver(Observer observer)
		{
            if(instanceObserverList == null)
            {
                instanceObserverList = new List<Observer>();
            }
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
            if (instanceObserverList == null)
            {
                instanceObserverList = new List<Observer>();
            }
			instanceObserverList.Remove(observer);
		}
	}
}
