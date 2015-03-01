using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.ArchitecturalObjects
{
	public interface Observer
	{
		//Callback for when an object or instance is changed
		void Update();
	}
}
