using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    public class Request
    {

      private String Client;
      private String Demande;
      private String PriseEnCharge;
      private DateTime SuiviTimeStamp;
      private DateTime FaitTimeStamp;
      private Request[] followUpInfo;

       public Request(String client,String demande,String priseencharge, DateTime SuiviTime, DateTime FaitTime)
       {
           this.Client = client;
           this.Demande = demande;
           this.PriseEnCharge = priseencharge;
           this.SuiviTimeStamp = SuiviTime;
           this.FaitTimeStamp = FaitTime;
       }


        public void SetFollowUpInfo(int position, Request followUpinfo)
        {
            this.followUpInfo[position] = followUpinfo;
        }

        public Request[] getFollowUpInfo()
        {
            return this.followUpInfo;
        }


    }
}
