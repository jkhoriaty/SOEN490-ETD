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

    [Serializable()]
    public class Request
    {
		private static List<Request> requestList = new List<Request>();//Contains a list of requests

        //Variables used for a request
        private String client;
        private String request;
        private String handledBy;
        private String recipient;
        private String timeHH;
        private String timeMM;
        private String followupHH;
        private String followupMM;
        private String completionHH;
        private String completionMM;

        private DateTime time;
        private DateTime followupTimestampMap;
		private DateTime completionTimestampMap;
		private Request[] followupInfo = new Request[100];

        //Creates a new request
        public Request(DateTime time, String client, String request, String recipient, String handledBy, DateTime followupTimestampMap, DateTime completionTimestampMap)
		{
            this.time = time;
            this.client = client;
            this.request = request;
            this.recipient = recipient;
            this.handledBy = handledBy;
            this.followupTimestampMap = followupTimestampMap;
            this.completionTimestampMap = completionTimestampMap;

			requestList.Add(this);
		}

        //Used by RequestLine.cs
        public Request(String timeHH,String timeMM, String client, String request, String recipient, String handledBy, String followupHH, String followupMM, String completionHH, String completionMM)
        {
            this.timeHH = timeHH;
            this.timeMM = timeMM;
            this.client = client;
            this.request = request;
            this.recipient = recipient;
            this.handledBy = handledBy;
            this.followupHH = followupHH;
            this.followupMM = followupMM;
            this.completionHH = completionHH;
            this.completionMM = completionMM;

            requestList.Add(this);
        }


        //Accessors

        //Returns the list of requests
		public static List<Request> getRequestList()
		{
			return requestList;
		}

		//Mutators

		//Sets the request's information
		public void setFollowupInfo(int position, Request followupInfo)
		{
			this.followupInfo[position] = followupInfo;
		}

        //Returns the request's information
	    public Request[] getFollowUpInfo()
		{
			return followupInfo;
		}

		//Set up the request's time hours
        public void setTimeHH(String timeHH)
        {
            this.timeHH = timeHH;
        }

		//Set up the request's time minutes
        public void setTimeMM(String timeMM)
        {
            this.timeMM = timeMM;
        }

		//Set up the request's client
        public void setClient(String client)
        {
            this.client = client;
        }

		//Set up the request's description
        public void setRequest(String request)
        {
            this.request = request;
        }

		//Set up the request's recipient
        public void setRecipient(String recipient)
        {
            this.recipient = recipient;
        }

		//Set up who handled the request
        public void setHandledBy(String handledBy)
        {
            this.handledBy = handledBy;
        }

		//Set up the request's follow up time (hours)
        public void setFollowUpHH(String followupHH)
        {
            this.followupHH = followupHH;
        }

		//Set up the request's follow up time(minutes)
        public void setFollowUpMM(String followupMM)
        {
            this.followupMM = followupMM;
        }

		//Set up the request's completion time(hours)
        public void setCompletionHH(String completionHH)
        {
            this.completionHH = completionHH;
        }

		//Set up the request's completion time(minutes)
        public void setCompletionMM(String completionMM)
        {
            this.completionMM = completionMM;
        }
	}
}
