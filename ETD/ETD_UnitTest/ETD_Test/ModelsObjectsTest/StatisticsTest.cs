using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using ETD.Services.Database;

namespace ETD_UnitTest
{
    [TestClass]
    public class StatisticsTest
    {
        [TestMethod]
        public void StatisticsSetNoOfInterventionsTest()
        {
			Statistics statistics = new Statistics();
			Intervention i = new Intervention();
			i.setTimeOfCall(DateTime.Now);
			statistics.setNumberOfInterventions();
			Assert.AreEqual(statistics.numberOfOngoingInterventions, 1);
			i.Completed();
			statistics.setNumberOfInterventions();
			Assert.AreEqual(statistics.numberOfCompletedInterventions, 1);
        }

		[TestMethod]
		public void StatisticsSetNumberOfInterventionsPerClassification()
		{
			Statistics statistics = new Statistics();
			Intervention i = new Intervention();
			i.setChiefComplaint("Ocular");
			statistics.setNumberOfInterventionsPerClassification();
			Assert.AreEqual(1, statistics.numberOfInterventionsPerClassification["Ocular"]);
		}

		[TestMethod]
		public void StatisticsSetInterventionsPerTeam()
		{
			Statistics statistics = new Statistics();
			Intervention i = new Intervention();
			Team t = new Team("A");
			i.AddInterveningTeam(t);
			i.setCode(1);
			statistics.setInterventionsPerTeam();
			Assert.AreEqual(0, t.getCode1Count());
		}
    }
}
