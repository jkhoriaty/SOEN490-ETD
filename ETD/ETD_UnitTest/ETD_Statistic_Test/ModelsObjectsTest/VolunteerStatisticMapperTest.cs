using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;
using System.Collections.Generic;

namespace ETD_UnitTest.ETD_Statistic_Test.ModelsObjectsTest
{
    [TestClass]
    public class VolunteerStatisticMapperTest
    {
        [TestMethod]
        public void VolunteerStatisticMapperCreationTest()
        {
            VolunteerStatisticMapper vsm = new VolunteerStatisticMapper();
            List<VolunteerStatistic> vsList= new List<VolunteerStatistic>();
            Assert.AreEqual(0, vsList.Count);
            vsList = vsm.getList();
            int size = vsList.Count;
            Assert.AreEqual(size, vsm.getList().Count);
            vsm.ClearList();
            Assert.AreEqual(0, vsm.getList().Count);
        }
    }
}
