﻿using ETD.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Operation Model Object
    /// </summary>

    [Serializable()]
    public class Operation
    {
        public static Operation currentOperation = null;
        //Database reflection variables
        private int operationID;

        //Variables used during an operation
        private String operationName;
        private String acronym;
        private DateTime shiftStart;
        private DateTime shiftEnd;
        private String dispatcherName;

        //Creates a new operation
        public Operation(String operationName, String acronym, DateTime shiftStart, DateTime shiftEnd, String dispatcherName)
		{
            this.operationName = operationName;
            this.acronym = acronym;
            this.shiftStart = shiftStart;
            this.shiftEnd = shiftEnd;
            this.dispatcherName = dispatcherName;
            this.operationID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Operations] (Name, Acronym, Shift_Start, Shift_End, Dispatcher) VALUES ('" + operationName.Replace("'", "''") + "', '" + acronym.Replace("'", "''") + "', '" + StaticDBConnection.DateTimeSQLite(shiftStart) + "', '" + StaticDBConnection.DateTimeSQLite(shiftEnd) + "', '" + dispatcherName.Replace("'", "''") + "')");
            currentOperation = this;
		}

        //Accessors
        public int getID()
        {
            return operationID;
        }

        //Returns the operation name
        public String getOperationName()
        {
            return operationName;
        }

        //Returns the operation's acronym
        public String getAcronym()
        {
            return acronym;
        }

        //Returns the start time of an operation
        public DateTime getShiftStart()
        {
            return shiftStart;
        }
        
        //Returns the end time of an operation
        public DateTime getShiftEnd()
        {
            return shiftEnd; 
        }

        //Returns the dispatcher's name
        public String getDispatcherName()
        {
            return dispatcherName;
        }
    }
}
