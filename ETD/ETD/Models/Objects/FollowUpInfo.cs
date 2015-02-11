using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    public class FollowUpInfo
    {

      private String Client;
      private String Demande;
      private String PriseEnCharge;
      private DateTime SuiviTimeStamp;
      private DateTime FaitTimeStamp;
      private FollowUpInfo[] followUpInfo;

       public FollowUpInfo(String client,String demande,String priseendcharge, DateTime SuiviTime, DateTime FaitTime)
       {
           this.Client = client;
           this.Demande = demande;
           this.PriseEnCharge = priseendcharge;
           this.SuiviTimeStamp = SuiviTime;
           this.FaitTimeStamp = FaitTime;
       }


        public void SetFollowUpInfo(int position, FollowUpInfo followUpinfo)
        {
            this.followUpInfo[position] = followUpinfo;
        }

        public FollowUpInfo[] getFollowUpInfo()
        {
            return this.followUpInfo;
        }


    }
}
