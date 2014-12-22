using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;

namespace ETD_UnitTest
{
    [TestClass]
    public class InterventionTest
    {
        [TestMethod]
        public void InterventionCreationTest()
        {
            Intervention a = new Intervention();
            Assert.IsTrue(a.getInterventionNumber() > 0);
        }

        [TestMethod]
        public void InterventionSetInterventionNumTest()
        {
            Intervention a = new Intervention();
            a.setInterventionNumber(3);
            Assert.AreEqual(3, a.getInterventionNumber());
        }

        [TestMethod]
        public void InterventionSetTimeOfCallTest()
        {
            Intervention a = new Intervention();
            DateTime d = DateTime.Now;
            a.setTimeOfCall(d);
            Assert.AreEqual(d, a.getTimeOfCall());
        }

        [TestMethod]
        public void InterventionSetCallerNameTest()
        {
            Intervention a = new Intervention();
            a.setCallerName("Bob");
            Assert.AreEqual("Bob", a.getCallerName());
        }

        [TestMethod]
        public void InterventionSetLocationTest()
        {
            Intervention a = new Intervention();
            a.setLocation("LocationA");
            Assert.AreEqual("LocationA", a.getLocation());
        }

        [TestMethod]
        public void InterventionSetNatureOfCallTest()
        {
            Intervention a = new Intervention();
            a.setNatureOfCall("natureofcall");
            Assert.AreEqual("natureofcall", a.getNatureOfCall());
        }

        [TestMethod]
        public void InterventionSetCodeTest()
        {
            Intervention a = new Intervention();
            a.setCode(1);
            Assert.AreEqual(1, a.getCode());
        }

        [TestMethod]
        public void InterventionSetGenderTest()
        {
            Intervention a = new Intervention();
            a.setGender("m");
            Assert.AreEqual("m", a.getGender());
        }

        [TestMethod]
        public void InterventionSetAgeTest()
        {
            Intervention a = new Intervention();
            a.setAge("21");
            Assert.AreEqual("21", a.getAge());
        }

        [TestMethod]
        public void InterventionSetChiefComplaintTest()
        {
            Intervention a = new Intervention();
            a.setChiefComplaint("testcomplaint");
            Assert.AreEqual("testcomplaint", a.getChiefComplaint());
        }

        [TestMethod]
        public void InterventionSetOtherChiefComplaintTest()
        {
            Intervention a = new Intervention();
            a.setOtherChiefComplaint("testothercomplaint");
            Assert.AreEqual("testothercomplaint", a.getOtherChiefComplaint());
        }

        [TestMethod]
        public void InterventionSetResourcesTest()
        {
            Intervention a = new Intervention();
            a.setResources(1, new Intervention.Resource("resource1", "A", DateTime.Now, DateTime.Now));
            a.setResources(2, new Intervention.Resource("resource2", "B", DateTime.Now, DateTime.Now));

            Intervention.Resource[] b = a.getResources();
            Assert.IsNotNull(b);
            //Not sure how to assertequal here...
        }

        [TestMethod]
        public void InterventionSetABCTest()
        {
            Intervention a = new Intervention();
            a.setABC(new Intervention.ABC(Consciousness.alert, true, Airways.clear, Breathing.normal, 1, Circulation.normal, 1));

            Assert.IsNotNull(a.getABC()); //Not sure if this is correct... someone check.
        }

        [TestMethod]
        public void InterventionSetAdditionalInfoTest()
        {
            Intervention a = new Intervention();
            a.setAdditionalInfo("Test info");
            Assert.AreEqual("Test info", a.getAdditionalInfo());
        }

        [TestMethod]
        public void InterventionSetConclusionTest()
        {
            Intervention a = new Intervention();
            a.setConclusion(Conclusions.hospital);
            Assert.AreEqual(Conclusions.hospital, a.getConclusion());
        }

        [TestMethod]
        public void InterventionSetConclusionAdditionalInfoTest()
        {
            Intervention a = new Intervention();
            a.setConclusionAdditionalInfo("additional conclusion info");
            Assert.AreEqual("additional conclusion info", a.getConclusionAdditionalInfo());
        }

        [TestMethod]
        public void InterventionSetConclusionTimeTest()
        {
            Intervention a = new Intervention();
            DateTime now = DateTime.Now;
            a.setConclusionTime(now);
            Assert.AreEqual(now, a.getConclusionTime());
        }

        [TestMethod]
        public void InterventionSetCall911TimeTest()
        {
            Intervention a = new Intervention();
            DateTime now = DateTime.Now;
            a.setCall911Time(now);
            Assert.AreEqual(now, a.getCall911Time());
        }

        [TestMethod]
        public void InterventionSetMeetingPointTest()
        {
            Intervention a = new Intervention();
            a.setMeetingPoint("meetingpoint");
            Assert.AreEqual("meetingpoint", a.getMeetingPoint());
        }

        [TestMethod]
        public void InterventionSetFirstResponderCompanyTest()
        {
            Intervention a = new Intervention();
            a.setFirstResponderCompany("frcompany");
            Assert.AreEqual("frcompany", a.getFirstResponderCompany());
        }

        [TestMethod]
        public void InterventionSetFirstResponderVehicleTest()
        {
            Intervention a = new Intervention();
            a.setFirstResponderVehicle("frvehicle");
            Assert.AreEqual("frvehicle", a.getFirstResponderVehicle());
        }

        [TestMethod]
        public void InterventionSetFirstResponderArrivalTimeTest()
        {
            Intervention a = new Intervention();
            DateTime now = DateTime.Now;
            a.setFirstResponderArrivalTime(now);
            Assert.AreEqual(now, a.getFirstResponderArrivalTime());
        }

        [TestMethod]
        public void InterventionSetAmbulanceCompanyTest()
        {
            Intervention a = new Intervention();
            a.setAmbulanceCompany("ambulancecompany");
            Assert.AreEqual("ambulancecompany", a.getAmbulanceCompany());
        }

        [TestMethod]
        public void InterventionSetAmbulanceVehicleTest()
        {
            Intervention a = new Intervention();
            a.setAmbulanceVehicle("ambulancevehicle");
            Assert.AreEqual("ambulancevehicle", a.getAmbulanceVehicle());
        }

        [TestMethod]
        public void InterventionSetAmbulanceArrivalTimeTest()
        {
            Intervention a = new Intervention();
            DateTime now = DateTime.Now;
            a.setAmbulanceArrivalTime(now);
            Assert.AreEqual(now, a.getAmbulanceArrivalTime());
        }
    }
}
