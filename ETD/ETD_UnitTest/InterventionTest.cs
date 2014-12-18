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
    }
}
