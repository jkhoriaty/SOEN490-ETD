using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;

namespace ETD_UnitTest
{
    [TestClass]
    public class InterventionTest
    {

        [TestMethod]
        public void AddInterventionInitialDetailsTest()
        {
             Intervention intervention = new Intervention();
             int interventionNumber = 1;
             DateTime timeOfCall = new DateTime(2014, 12, 20);
             String callerName = "Paul";
             String location = "7141 Sherbrooke Street West, Montreal, QC H4B 1R6 ";
             String natureOfCall = "Broken ankle";

             intervention.setInterventionNumber(interventionNumber);
             intervention.setTimeOfCall(timeOfCall);
             intervention.setCallerName(callerName);
             intervention.setLocation(location);
             intervention.setNatureOfCall(natureOfCall);

             Assert.AreEqual(intervention.getInterventionNumber(), interventionNumber);
             Assert.AreEqual(intervention.getTimeOfCall() ,timeOfCall );
             Assert.AreEqual(intervention.getCallerName() , callerName);
             Assert.AreEqual(intervention.getLocation() , location);
             Assert.AreEqual(intervention.getNatureOfCall() ,natureOfCall );
        

        }
        [TestMethod]
        public void Document911CallTest()
        {
            Intervention intervention = new Intervention();
            DateTime call911Time = new DateTime(2014, 12, 20);
            String meetingPoint = "Front door";
            String firstResponderCompany = "Marie";
            String firstResponderVehicle = "ambulanceCart";
            DateTime firstResponderArrivalTime = new DateTime(2014, 12, 20);
            String ambulanceCompany = "Jean";
            String ambulanceVehicle = "vehicle";
            DateTime ambulanceArrivalTime = new DateTime(2014, 12, 20);

            intervention.setCall911Time(call911Time);
            intervention.setMeetingPoint(meetingPoint);
            intervention.setFirstResponderCompany(firstResponderCompany);
            intervention.setFirstResponderCompany(firstResponderCompany);
            intervention.setFirstResponderVehicle(firstResponderVehicle);
            intervention.setFirstResponderArrivalTime(firstResponderArrivalTime);
            intervention.setAmbulanceCompany(ambulanceCompany);
            intervention.setAmbulanceVehicle(ambulanceVehicle);
            intervention.setAmbulanceArrivalTime(ambulanceArrivalTime);

            Assert.AreEqual(intervention.getCall911Time(), call911Time);
            Assert.AreEqual(intervention.getMeetingPoint(), meetingPoint);
            Assert.AreEqual(intervention.getFirstResponderCompany(), firstResponderCompany);
            Assert.AreEqual(intervention.getFirstResponderVehicle(), firstResponderVehicle);
            Assert.AreEqual(intervention.getFirstResponderArrivalTime(), firstResponderArrivalTime);
            Assert.AreEqual(intervention.getAmbulanceCompany(), ambulanceCompany);
            Assert.AreEqual(intervention.getAmbulanceVehicle(), ambulanceVehicle);
            Assert.AreEqual(intervention.getAmbulanceArrivalTime(), ambulanceArrivalTime);
        }

        [TestMethod]
        public void LogEndOfInterventionTest()
        {
            Intervention intervention = new Intervention();
            String chiefComplaint = "Not fast enough.";
            String otherChiefComplaint = null;
            DateTime conclusionTime = DateTime.Now;
            Conclusions conclusion = Conclusions.referredToDoctor;
            String conclusionAdditionalInfo = null;
		    
            intervention.setChiefComplaint(chiefComplaint);
            intervention.setOtherChiefComplaint(otherChiefComplaint);
            intervention.setConclusion(conclusion);
            intervention.setConclusionAdditionalInfo(conclusionAdditionalInfo);
            intervention.setConclusionTime(conclusionTime);
     
            Assert.AreEqual(intervention.getChiefComplaint(), chiefComplaint);
            Assert.AreEqual(intervention.getOtherChiefComplaint(), otherChiefComplaint);
            Assert.AreEqual(intervention.getConclusion(), conclusion);
            Assert.AreEqual(intervention.getConclusionAdditionalInfo(), conclusionAdditionalInfo);
            Assert.AreEqual(intervention.getConclusionTime(), conclusionTime);
        }

       [TestMethod]
        public void AddAdditionalDetailsTest()
        {
            Intervention intervention = new Intervention();
            String additionalInfo = null;
            int code = 1;
            String gender = "male";
            String age = "25";

            intervention.setAdditionalInfo(additionalInfo);
            intervention.setCode(code);
            intervention.setGender(gender);
            intervention.setAge(age);

            Assert.AreEqual(intervention.getCode(), code);
            Assert.AreEqual(intervention.getGender(), gender);
            Assert.AreEqual(intervention.getAge(), age);
            Assert.AreEqual(intervention.getAdditionalInfo(), additionalInfo);
        }

        [TestMethod]
        public void getResourcesTest()
        {
            Intervention intervention = new Intervention();
            String resourceName = "ambulanceCart";
            String team = "A";
            DateTime moving = new DateTime(2014, 12, 20);
            DateTime arrival = new DateTime(2014, 12, 20);
            //Resource resource1 = new Resource(resourceName, team, moving, arrival);

            //intervention.setResources(1, resource1);
            //Assert.AreEqual(intervention.getResources(), resource1);
        }

        [TestMethod]
        public void getABCTest()
        {
            Intervention intervention = new Intervention();
            //ABC abc = new ABC(Consciousness.alert, true, Airways.clear, Breathing.normal, 12, Circulation.normal, 3);

            //intervention.setABC(abc);
            //Assert.AreEqual(intervention.getABC(), abc);
        }

    }
}
