using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.ArchitecturalObjects
{
	public interface Observer
	{
		void ObservedObjectUpdated();
	}
}
