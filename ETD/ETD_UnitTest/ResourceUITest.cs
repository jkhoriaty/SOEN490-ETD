using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters.MapSection.PinManagement;
using ETD.ViewsPresenters;
namespace ETD_UnitTest
{
    [TestClass]
    public class ResourceUITest
    {
        [TestMethod]
        public void testLabelName()
        {
            Team b = new Team("A");
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember MemberA = new TeamMember("John", Trainings.firstResponder, departure);
            TeamMember MemberB = new TeamMember("Alex", Trainings.firstAid, departure);
            b.addMember(MemberA);
            b.addMember(MemberB);
            MainWindow mw = new MainWindow();
            MapSectionPage mapSection = new MapSectionPage(mw);
            int teamSize = 200;
            TeamGrid tg = new TeamGrid(b, mapSection, teamSize);

            //PinEditor p = new PinEditor(mapSection);
            //p.CreateTeamPin(a);

            Assert.AreEqual(tg.team.getName(), "A");
        }
    }
}
