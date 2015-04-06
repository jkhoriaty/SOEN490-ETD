using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;
using System.Collections.Generic;

namespace ETD_UnitTest.ETD_Statistic_Test.ModelsObjectsTest
{
    [TestClass]
    public class InterventionStatisticMapperTest
    {

        [TestMethod]
        public void InterventionStatisticMapperCreationTest()
        {
            List<String> complaints = new List<String>();
            Statistic.setOperationID("operation4");
            InterventionStatisticMapper ism = new InterventionStatisticMapper();
            complaints = ism.getListComplaint();
            Assert.AreEqual(complaints.Count, ism.getListComplaint().Count);
        }

        [TestMethod]
        public void InterventionStatisticMapperChildrenWATestCount()
        {
            Statistic.setOperationID("operation4");
            InterventionStatisticMapper ism = new InterventionStatisticMapper();
            List<String> complaints = new List<String>();
            complaints = ism.getListComplaint();
            String firstComplaint = complaints[0];
            ism.getInterventionChildrenWithoutAmublanceCount(firstComplaint);
            List<int> interventionChildList = new List<int>();
            interventionChildList = ism.getInterventionChildrenWithoutAmublanceList();
            Assert.AreEqual(interventionChildList.Count, ism.getInterventionChildrenWithoutAmublanceList().Count);
            ism.clearInterventionChildrenWACountList();
            Assert.AreEqual(0, ism.getInterventionChildrenWithoutAmublanceList().Count);

            ism.getInterventionChildrenWithoutAmublanceCount("TESTING_NULL");
            interventionChildList = ism.getInterventionChildrenWithoutAmublanceList();
            Assert.AreEqual(0, interventionChildList[0]);
        }

        [TestMethod]
        public void InterventionStatisticMapperAdultWATestCount()
        {
            Statistic.setOperationID("operation4");
            InterventionStatisticMapper ism = new InterventionStatisticMapper();
            List<String> complaints = new List<String>();
            complaints = ism.getListComplaint();
            String firstComplaint = complaints[0];
            ism.getInterventionAdultWithoutAmublanceCount(firstComplaint);
            List<int> interventionAdultList = new List<int>();
            interventionAdultList = ism.getInterventionAdultWithoutAmublanceList();
            Assert.AreEqual(interventionAdultList.Count, ism.getInterventionAdultWithoutAmublanceList().Count);
            ism.clearInterventionAdultWACountList();
            Assert.AreEqual(0, ism.getInterventionAdultWithoutAmublanceList().Count);

            ism.getInterventionAdultWithoutAmublanceCount("TESTING_NULL");
            interventionAdultList = ism.getInterventionAdultWithoutAmublanceList();
            Assert.AreEqual(0, interventionAdultList[0]);

        }

        [TestMethod]
        public void InterventionStatisticMapperChildrenATestCount()
        {
            Statistic.setOperationID("operation4");
            InterventionStatisticMapper ism = new InterventionStatisticMapper();
            List<String> complaints = new List<String>();
            complaints = ism.getListComplaint();
            String firstComplaint = complaints[0];
            ism.getInterventionChildrenWithAmublanceCount(firstComplaint);
            List<int> interventionChildList = new List<int>();
            interventionChildList = ism.getInterventionChildrenWithAmublanceList();
            Assert.AreEqual(interventionChildList.Count, ism.getInterventionChildrenWithAmublanceList().Count);
            ism.clearInterventionChildrenACountList();
            Assert.AreEqual(0, ism.getInterventionChildrenWithAmublanceList().Count);

            ism.getInterventionChildrenWithAmublanceCount("TESTING_NULL");
            interventionChildList = ism.getInterventionChildrenWithAmublanceList();
            Assert.AreEqual(0, interventionChildList[0]);

        }

        [TestMethod]
        public void InterventionStatisticMapperAdultATestCount()
        {
            Statistic.setOperationID("operation4");
            InterventionStatisticMapper ism = new InterventionStatisticMapper();
            List<String> complaints = new List<String>();
            complaints = ism.getListComplaint();
            String firstComplaint = complaints[0];
            ism.getInterventionAdultWithAmublanceCount(firstComplaint);
            List<int> interventionAdultList = new List<int>();
            interventionAdultList = ism.getInterventionAdultWithAmublanceList();
            Assert.AreEqual(interventionAdultList.Count, ism.getInterventionAdultWithAmublanceList().Count);
            ism.clearInterventionAdultACountList();
            Assert.AreEqual(0, ism.getInterventionAdultWithAmublanceList().Count);

            ism.getInterventionAdultWithAmublanceCount("TESTING_NULL");
            interventionAdultList = ism.getInterventionAdultWithAmublanceList();
            Assert.AreEqual(0, interventionAdultList[0]);

        }
    }
}
