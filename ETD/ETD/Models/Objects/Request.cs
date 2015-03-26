﻿using System;
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
		private static List<Request> requestList = new List<Request>();//Contains a list of requests

        //Variables used for a request
        private String timeHH;
        private String timeMM;
        private String followupHH;
        private String followupMM;
        private String completionHH;
        private String completionMM;

		private String client;
		private String request;
        private String handledBy;
        private String recipient;
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
        
        //Returns the request's information
	    public Request[] getFollowUpInfo()
		{
			return followupInfo;
		}

        //Mutators

        //Sets the request's information
		public void setFollowupInfo(int position, Request followupInfo)
		{
            this.followupInfo[position] = followupInfo;
		}

	
	}
}
