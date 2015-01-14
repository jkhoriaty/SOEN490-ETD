using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.Models.Objects;

namespace ETD_UnitTest
{
    [TestClass]
    public class TeamMemberTest
    {
        [TestMethod]
        public void TeamMemberCreation()
        {
            String name = "Test";
           Trainings trainLevel = Trainings.firstAid;
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Assert.AreEqual(a.getName(), "Test");
            Assert.AreEqual(a.getTrainingLevel(), Trainings.firstAid);
        }

        [TestMethod]
        public void getLevelofTrainingTest()
        {

            DateTime departure = new DateTime(2014, 12, 20);
            TeamMember MemberA = new TeamMember("Paul", Trainings.firstResponder, departure);
            Assert.AreEqual(MemberA.getTrainingLevel(), Trainings.firstResponder);

        }
    }
}
