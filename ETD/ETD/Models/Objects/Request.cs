using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Request Model Object
    /// </summary>

    public class Request
    {
		private static List<Request> requestList = new List<Request>();

		private String Client;
		private String request;
		private String HandledBy;
		private DateTime SuiviTimeStamp;
		private DateTime FaitTimeStamp;
		private Request[] followUpInfo = new Request[100];

		public Request(String client,String Request,String HandledBy, DateTime SuiviTime, DateTime FaitTime)
		{
			this.Client = client;
            this.request = Request;
			this.HandledBy = HandledBy;
			this.SuiviTimeStamp = SuiviTime;
			this.FaitTimeStamp = FaitTime;

			requestList.Add(this);
		}

		public static List<Request> getRequestList()
		{
			return requestList;
		}

		public void SetFollowUpInfo(int position, Request folUpinfo)
		{
			followUpInfo[position] = folUpinfo;
		}

		public Request[] getFollowUpInfo()
		{
			return followUpInfo;
		}
	}
}
