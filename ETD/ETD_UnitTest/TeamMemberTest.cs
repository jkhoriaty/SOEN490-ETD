using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD;

namespace ETD_UnitTest
{
    [TestClass]
    public class TeamMemberTest
    {
        [TestMethod]
        public void TeamMemberCreation()
        {
            String name = "Test";
            int trainLevel = 0;
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Assert.AreEqual(a.getName(), "Test");
            Assert.AreEqual(a.getTrainingLevel(), 0);
        }
    }
}
