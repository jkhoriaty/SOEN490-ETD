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
            TeamMember a = new TeamMember(name, trainLevel);
            Assert.AreEqual(a.name, "Test");
            Assert.AreEqual(a.trainingLevel, 9000);

        }
    }

}
