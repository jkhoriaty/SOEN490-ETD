using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;
using System.Collections.Generic;



namespace ETD_UnitTest.ETD_Statistic_Test.ModelsObjectsTest
{
    [TestClass]
    public class OperationStatisticMapperTest
    {
        [TestMethod]
        public void OperationStatisticMapperCreationTest()
        {
            OperationStatisticMapper osm = new OperationStatisticMapper();
            List<OperationStatistic> osList = new List<OperationStatistic>();
            Assert.AreEqual(0,osm.getList().Count);
            osList = osm.getList();
            
            Assert.AreEqual(osList.Count, osm.getList().Count);
            osm.ClearList();
            Assert.AreEqual(0, osm.getList().Count);
        }

        [TestMethod]
        public void OperationStatisticMapperTeamCountTest()
        {
            OperationStatisticMapper osm = new OperationStatisticMapper();
            int teamCount = osm.getTeamCountFromDB();
            Assert.AreEqual(0, teamCount);

        }

        [TestMethod]
        public void OperationStatisticMapperVolunteerCountTest()
        {
            OperationStatisticMapper osm = new OperationStatisticMapper();
            int volunteerCount = osm.getVolunteerCountFromDB();
            Assert.AreEqual(0, volunteerCount);

        }

    }
}
