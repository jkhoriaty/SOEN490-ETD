using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD;
using ETD.Models;
using ETD.Models.Objects;
using ETD.Models.ArchitecturalObjects;
using ETD.CustomObjects;
using ETD.CustomObjects.CustomUIObjects;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters;

namespace ETD_UnitTest
{
    [TestClass]
    public class TeamTest
    {
        
        [TestMethod]
        public void TeamCreationTest()
        {
            Team a = new Team("tester");
            Assert.AreEqual(a.getName(), "tester");
            a.setName("something");
            Assert.AreEqual(a.getName(), "something");
        }

        [TestMethod]
        public void TeamAddMemberTest()
        {
            Team a = new Team("A");
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember MemberA = new TeamMember("John", Trainings.firstResponder, departure);
            TeamMember MemberB = new TeamMember("Alex", Trainings.firstAid, departure);
            a.AddMember(MemberA);
            a.AddMember(MemberB);
            Assert.AreEqual(a.getMember(2), null);
            Assert.AreEqual(a.getMember(0), MemberA);
            Assert.AreEqual(a.getMember(1), MemberB);
        }

        [TestMethod]
        public void TeamAddMemberTest_Full()
        {
            Team a = new Team("A");
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember MemberA = new TeamMember("John", Trainings.firstResponder, departure);
            TeamMember MemberB = new TeamMember("Alex", Trainings.firstAid, departure);
            TeamMember MemberC = new TeamMember("Kelso", Trainings.medicine, departure);
            TeamMember MemberD = new TeamMember("Dorian", Trainings.medicine, departure);
            a.AddMember(MemberA);
            a.AddMember(MemberB);
            a.AddMember(MemberC);
            Assert.IsFalse(a.AddMember(MemberD));
        }

        [TestMethod]
        public void EquipmentAddTest()
        {
            Team c = new Team("C");
			
            Equipment equip1 = new Equipment("sittingCart");
            Equipment equip2 = new Equipment("ambulanceCart");
            Equipment equip3 = new Equipment("epipen");
            Equipment equip4 = new Equipment("transportStretcher");
            c.AddEquipment(equip1);
            c.AddEquipment(equip2);
            c.AddEquipment(equip3);
            Assert.AreEqual(c.getEquipmentCount(), 3);

            c.RemoveEquipment(equip1);
            Assert.AreEqual(c.getEquipmentCount(), 2);
        }

        [TestMethod]
        public void EquipmentAddTest_Full()
        {
            Team c = new Team("C");

            Equipment equip1 = new Equipment("sittingCart");
            Equipment equip2 = new Equipment("ambulanceCart");
            Equipment equip3 = new Equipment("epipen");
            Equipment equip4 = new Equipment("transportStretcher");
            c.AddEquipment(equip1);
            c.AddEquipment(equip2);
            c.AddEquipment(equip3);
            Assert.IsFalse(c.AddEquipment(equip4));
        }

        [TestMethod]
        public void setStatusTest()
        {
            Team b = new Team("B");

            b.setStatus("available");
            Assert.AreEqual(b.getStatus(), Statuses.available);
        }

        [TestMethod]
        public void SwapTest_ZeroUp()
        {
            Team a = new Team("Team1");
            Team b = new Team("Team2");
            Team c = Team.getTeamList()[0];
            
            int initialPosition = 0;
            Team.Swap(c, "up");
            int finalPosition = Team.getTeamList().IndexOf(c);

            Assert.AreEqual(initialPosition, finalPosition);
        }

        [TestMethod]
        public void SwapTest_LastDown()
        {
            Team a = new Team("Team1");
            Team b = new Team("Team2");

            int initialPosition = Team.getTeamList().IndexOf(b);
            Team.Swap(b, "down");
            int finalPosition = Team.getTeamList().IndexOf(b);

            Assert.AreEqual(initialPosition, finalPosition);
        }

        [TestMethod]
        public void SwapTest_Up()
        {
            Team a = new Team("Team1");
            Team b = new Team("Team2");

            int initialPosition = Team.getTeamList().IndexOf(b);
            Team.Swap(b, "up");
            int finalPosition = Team.getTeamList().IndexOf(b);

            Assert.IsTrue(finalPosition == initialPosition-1);
        }

        [TestMethod]
        public void SwapTest_Down()
        {
            Team a = new Team("Team1");
            Team b = new Team("Team2");

            int initialPosition = Team.getTeamList().IndexOf(a);
            Team.Swap(a, "down");
            int finalPosition = Team.getTeamList().IndexOf(a);

            Assert.IsTrue(finalPosition == initialPosition + 1);
        }

        [TestMethod]
        public void TeamListContainsTest_False()
        {
            Assert.IsFalse(Team.TeamListContains("MadeUpName5000"));
        }

        [TestMethod]
        public void TeamListContainsTest_True()
        {
            Team a = new Team("MadeUpName5000");
            Assert.IsTrue(Team.TeamListContains("MadeUpName5000"));
        }

        [TestMethod]
        public void GetTeamObjectTest()
        {
            Team a = new Team("MadeUpName9000");
            Team b = Team.getTeamObject("MadeUpName9000");
            Assert.AreEqual(a, b);
        }

        [TestMethod]
        public void DeleteTeamTest()
        {
            Team a = new Team("TeamToDelete");
            Team.DeleteTeam(a);
            Assert.IsFalse(Team.getTeamList().Contains(a));
        }

        [TestMethod]
        public void GetHighestLevelOfTrainingTest()
        {
            Team a = new Team("A");
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember MemberA = new TeamMember("John", Trainings.firstResponder, departure);
            TeamMember MemberB = new TeamMember("Alex", Trainings.firstAid, departure);
            TeamMember MemberC = new TeamMember("Kelso", Trainings.medicine, departure);
            a.AddMember(MemberA);
            a.AddMember(MemberB);
            Assert.AreEqual(a.getHighestLevelOfTraining(), Trainings.firstResponder);
            a.AddMember(MemberC);
            Assert.AreEqual(a.getHighestLevelOfTraining(), Trainings.medicine);
        }

		/*[TestMethod]
		public void CreateTeamPinTest()
		{
			MainWindow window = new MainWindow();
			MapSectionPage mapSectionPage = new MapSectionPage(window);
			Team a = new Team("A");
			mapSectionPage.Update();
			
			bool check = false;
			foreach(Pin pin in Pin.getPinList())
			{
				if(pin.getRelatedObject() == a)
				{
					check = true;
				}
			}
			Assert.IsTrue(check);
		}
		*/
    }
}
