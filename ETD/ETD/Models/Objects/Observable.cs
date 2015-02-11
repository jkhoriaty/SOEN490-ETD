using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	interface Observable
	{
		void registerObserver(Observer observer);
		void notifyAll();
	}
}
