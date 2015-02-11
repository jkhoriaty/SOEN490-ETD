using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	interface Observer
	{
		public void ObservedObjectUpdated();
	}
}
