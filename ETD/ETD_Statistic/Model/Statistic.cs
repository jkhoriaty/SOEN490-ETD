using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

//class to store statistic objects and basic getters

namespace ETD_Statistic.Model
{
    public static class Statistic
    {
        static ArrayList operationList = new ArrayList();
        static String operationID;
        static String operationString; 


        public static void setOperationID(String operation)
        {
            operationID = operation.Substring(9).ToString();
            operationList.Add(operationID);
        }


        //display the operation IDs as a (ID1, ID2, ID3.. ) string to be used for database extraction
        public static String getOperationID()
        {
            operationString = string.Join(",", operationList.ToArray());
            operationString = "(" + operationString + ")";
            return operationString;
        }

        public static ArrayList getOperationList()
        {
            return operationList;
        }

        public static void removeOperationID(String operation)
        {
            operationID = operation.Substring(9).ToString(); 
            operationList.Remove(operationID);
        }

        public static void clearOperationsList()
        {
            operationList.Clear();
        }

        public static int getListSize()
        {
            return operationList.Count;
        }
   
    }
}
