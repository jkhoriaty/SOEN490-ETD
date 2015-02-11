using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	interface Observable
	{
		private List<Observer> observers;
		public void registerObserver(Observer observer);
		private void notifyAll();
	}
}
