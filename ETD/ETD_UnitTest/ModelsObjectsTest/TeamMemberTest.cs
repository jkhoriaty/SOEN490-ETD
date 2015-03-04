using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.Models.Objects;
using System.Windows.Controls;

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

        [TestMethod]
        public void TeamMemberGetDepartureTest()
        {
            String name = "Test";
            Trainings trainLevel = Trainings.firstAid;
            DateTime departure = DateTime.Now;
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Assert.AreEqual(a.getDeparture(), departure);
        }

        [TestMethod]
        public void TeamMemberGetNameGridTest()
        {
            String name = "Test";
            Trainings trainLevel = Trainings.firstAid;
            DateTime departure = DateTime.Now;
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Grid nameGrid = new Grid();
            a.setNameGrid(nameGrid);
            Assert.AreEqual(a.getNameGrid(), nameGrid);
        }

        [TestMethod]
        public void TeamMemberSetNameGridTest_Null()
        {
            String name = "Test";
            Trainings trainLevel = Trainings.firstAid;
            DateTime departure = DateTime.Now;
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Grid nameGrid = new Grid();
            Assert.AreEqual(a.getNameGrid(), null);
        }

        [TestMethod]
        public void TeamMemberSetNameGridTest_NotNull()
        {
            String name = "Test";
            Trainings trainLevel = Trainings.firstAid;
            DateTime departure = DateTime.Now;
            TeamMember a = new TeamMember(name, trainLevel, departure);
            Grid nameGrid = new Grid();
            a.setNameGrid(nameGrid);
            Assert.AreNotEqual(a.getNameGrid(), null);
        }
    }
}
