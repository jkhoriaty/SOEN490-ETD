using Emergency_Team_Dispatcher;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmergencyTeamDispatcherTest
{
    [TestClass]
    public class TeamMemberTest
    {
        [TestMethod]
        public void TeamMemberCreation()
        {
            String name = "Test";
            int trainLevel = 9000;
            String departure = "1500";
            TeamMember a = new TeamMember(name, trainLevel,departure);
            Assert.AreEqual(a.getName(), "Test");
            Assert.AreEqual(a.getTrainingLevel(), 9000);
        }
    }

}
