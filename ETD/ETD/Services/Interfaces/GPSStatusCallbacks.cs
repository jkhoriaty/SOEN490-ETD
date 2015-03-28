using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Interfaces
{
	interface GPSStatusCallbacks
	{
		void NotifyConnectionSuccess();
		void NotifyConnectionFail();
		void SetupCompleted();
	}
}
