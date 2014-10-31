using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emergency_Team_Dispatcher;


namespace EmergencyTeamDispatcherTest
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void TeamCreation()
        {
            String name = "Test";
            Team T = new Team(name);
            Assert.AreEqual(T.getName(), "Test");
            T.setName("Bill");
            Assert.AreEqual(T.getName(), "Bill");
        }

        [TestMethod]
        public void defaultTeamCreation()
        {
            Team T = new Team();
            Assert.AreEqual(T.getName(), "Alpha");
        }

        [TestMethod]
        public void addMemberTest()
        {
            Team T = new Team();
            TeamMember testMember = new TeamMember("John",9999,"1500");
            T.addMember(testMember);
            Assert.AreEqual(T.getMember(0), null);
            Assert.AreEqual(T.getMember(9999), testMember);
        }
    }

}
