using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD_Statistic.Model;

namespace ETD_UnitTest.ETD_Statistic_Test.ModelsObjectsTest
{
    [TestClass]
    public class VolunteerStatisticTest
    {
        [TestMethod]
        public void VolunteerStatisticCreation()
        {
            String name = "name";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            String op = "1";
            VolunteerStatistic vs = new VolunteerStatistic(name, start, end, op);
            Assert.AreEqual(name, vs.getName());
            Assert.AreEqual(start, vs.getStart());
            Assert.AreEqual(end, vs.getEnd());
            Assert.AreEqual(op, vs.getOperationID());
        }

        [TestMethod]
        public void SettingVolunteerStatisticTest()
        {
            String name = "name";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            String op = "1";
            VolunteerStatistic vs = new VolunteerStatistic(name, start, end, op);
            Assert.AreEqual(name, vs.getName());
            Assert.AreEqual(start, vs.getStart());
            Assert.AreEqual(end, vs.getEnd());
            Assert.AreEqual(op, vs.getOperationID());

            DateTime newStart = start.AddDays(1);
            DateTime newEnd = end.AddDays(1);
            String newName = "newName";
            vs.setEnd(newEnd);
            vs.setStart(newStart);
            vs.setName(newName);

            Assert.AreEqual(newName, vs.getName());
            Assert.AreEqual(newStart, vs.getStart());
            Assert.AreEqual(newEnd, vs.getEnd());
        }

        [TestMethod]
        public void TestTimeDifference()
        {
            String name = "name";
            String op = "1";
            DateTime firstTime = DateTime.Now;
            DateTime secondTime = firstTime.AddDays(1);
            TimeSpan difference = secondTime.Subtract(firstTime);
            VolunteerStatistic vs = new VolunteerStatistic(name, firstTime, secondTime, op);


            Assert.AreEqual(difference, vs.getTimeDiff());
            Assert.AreEqual(difference, vs.getDateDifference(firstTime, secondTime));

        }
    }
}
