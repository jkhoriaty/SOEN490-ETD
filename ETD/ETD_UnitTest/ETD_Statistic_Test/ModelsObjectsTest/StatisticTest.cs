using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;
using System.Collections;

namespace ETD_UnitTest.ETD_Statistic_Test.ModelsObjectsTest
{
    [TestClass]
    public class StatisticTest
    {
        [TestMethod]
        public void StatisticSetListTest()
        {
            Statistic.clearOperationsList();
            Assert.AreEqual(0, Statistic.getListSize());
            Statistic.setOperationID("operation9");
            Statistic.setOperationID("operation11");
            Assert.AreEqual(2, Statistic.getListSize());
            ArrayList al = Statistic.getOperationList();

            Assert.AreEqual("9", al[0]);
            Assert.AreEqual("11", al[1]);

        }

        [TestMethod]
        public void StatisticGetIDTest()
        {
            Statistic.clearOperationsList();
            Assert.AreEqual(0, Statistic.getListSize());
            Statistic.setOperationID("operation9");
            Statistic.setOperationID("operation11");
            Assert.AreEqual(2, Statistic.getListSize());

            Assert.AreEqual("(9,11)", Statistic.getOperationID());

            
        }

        [TestMethod]
        public void StatisticRemoveOperationIDTest()
        {
            Statistic.clearOperationsList();
            Assert.AreEqual(0, Statistic.getListSize());
            Statistic.setOperationID("operation9");
            Statistic.setOperationID("operation11");
            Assert.AreEqual(2, Statistic.getListSize());

            Statistic.removeOperationID("operation11");

            String a = Statistic.getOperationID();
            Assert.AreEqual("(9)", a);

            Assert.AreEqual(1, Statistic.getListSize());
            Statistic.clearOperationsList();
            Assert.AreEqual(0, Statistic.getListSize());
 
        }
    }
}
