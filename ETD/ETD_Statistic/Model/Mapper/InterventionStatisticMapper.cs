using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.Services.Database;
using System.Data.SQLite;
using ETD_Statistic.Model;

namespace ETD_Statistic.Model
{
    public class InterventionStatisticMapper
    {
        List<String> chiefComplaint = new List<String>();
        List<int> interventionChildrenWAList = new List<int>();
        List<int> interventionAdultWAList = new List<int>();
        List<int> interventionChildrenAList = new List<int>();
        List<int> interventionAdultAList = new List<int>();


        String dBQueryComplaints = "SELECT DISTINCT Chief_Complaint FROM Interventions WHERE Operation_ID IN " + Statistic.getOperationID();       
        
        public InterventionStatisticMapper()
        {
            chiefComplaint.Clear();
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase(dBQueryComplaints);
            while (reader.Read()) 
            {
                String complaint = reader["Chief_Complaint"].ToString();
                chiefComplaint.Add(complaint);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<String> getListComplaint()
        {
            return chiefComplaint;
        }

        //Children without Ambulance

        public void getInterventionChildrenWithoutAmublanceCount(String complaint)
        {
            String dBQueryInterventionChildrenWACount = "SELECT count(*) as Count from Interventions WHERE Conclusion NOT LIKE 'get_ComboBoxItem_Conclusion_911' AND Conclusion NOT LIKE 'NULL' AND Operation_ID IN " + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age < 18";
            SQLiteDataReader childrenReader = StaticDBConnection.QueryDatabase(dBQueryInterventionChildrenWACount);

            while (childrenReader.Read())
            {
                int count = int.Parse(childrenReader["Count"].ToString());
                interventionChildrenWAList.Add(count);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<int> getInterventionChildrenWithoutAmublanceList()
        {
            return interventionChildrenWAList;
        }

        public void clearInterventionChildrenWACountList()
        {
            interventionChildrenWAList.Clear();
        }


        //Adult without ambulance
        public void getInterventionAdultWithoutAmublanceCount(String complaint)
        {
            String dBQueryInterventionAdultWACount = "SELECT count(*) as Count from Interventions WHERE Conclusion NOT LIKE 'get_ComboBoxItem_Conclusion_911' AND Conclusion NOT LIKE 'NULL' AND Operation_ID IN " + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age >= 18";
            SQLiteDataReader adultReader = StaticDBConnection.QueryDatabase(dBQueryInterventionAdultWACount);

            while (adultReader.Read())
            {
                int count = int.Parse(adultReader["Count"].ToString());
                interventionAdultWAList.Add(count);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<int> getInterventionAdultWithoutAmublanceList()
        {
            return interventionAdultWAList;
        }

        public void clearInterventionAdultWACountList()
        {
            interventionAdultWAList.Clear();
        }


        //Children with Ambulance
        public void getInterventionChildrenWithAmublanceCount(String complaint)
        {
            String dBQueryInterventionChildrenACount = "SELECT count(*) as Count from Interventions WHERE Conclusion LIKE 'get_ComboBoxItem_Conclusion_911' AND Operation_ID IN " + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age < 18";
            SQLiteDataReader childrenReader = StaticDBConnection.QueryDatabase(dBQueryInterventionChildrenACount);

            while (childrenReader.Read())
            {
                int count = int.Parse(childrenReader["Count"].ToString());
                interventionChildrenAList.Add(count);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<int> getInterventionChildrenWithAmublanceList()
        {
            return interventionChildrenAList;
        }

        public void clearInterventionChildrenACountList()
        {
            interventionChildrenAList.Clear();
        }


        //Adult with ambulance

        public void getInterventionAdultWithAmublanceCount(String complaint)
        {
            String dBQueryInterventionAdultACount = "SELECT count(*) as Count from Interventions WHERE Conclusion LIKE 'get_ComboBoxItem_Conclusion_911' AND Operation_ID IN " + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age >= 18";
            SQLiteDataReader adultReader = StaticDBConnection.QueryDatabase(dBQueryInterventionAdultACount);

            while (adultReader.Read())
            {
                int count = int.Parse(adultReader["Count"].ToString());
                interventionAdultAList.Add(count);
            }
            StaticDBConnection.CloseConnection();
        }

        public List<int> getInterventionAdultWithAmublanceList()
        {
            return interventionAdultAList;
        }

        public void clearInterventionAdultACountList()
        {
            interventionAdultAList.Clear();
        }


    }
}
