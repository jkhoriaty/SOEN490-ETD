using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD;

namespace ETD_UnitTest
{
    [TestClass]
    public class TeamTest
    {
        [TestMethod]
        public void DefaultTeamCreationTest()
        {
            Team a = new Team();
            Assert.AreEqual(a.getName(), "Alpha");
        }

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
            Team a = new Team();
            DateTime departure = new DateTime(2014, 11, 20);
            TeamMember MemberA = new TeamMember("John", 1, departure);
            TeamMember MemberB = new TeamMember("Alex", 0, departure);
            a.addMember(MemberA);
            a.addMember(MemberB);
            Assert.AreEqual(a.getMember(2), null);
            Assert.AreEqual(a.getMember(0), MemberA);
            Assert.AreEqual(a.getMember(1), MemberB);
        }

        [TestMethod]
        public void EquipmentAddTest()
        {
            Team a = new Team();
            Equipment equip1 = new Equipment(equipments.sittingCart);
            Equipment equip2 = new Equipment(equipments.ambulanceCart);
            Equipment equip3 = new Equipment(equipments.epipen);
            Equipment equip4 = new Equipment(equipments.transportStretcher);
            a.addEquipment(equip1);
            a.addEquipment(equip2);
            a.addEquipment(equip3);
            a.addEquipment(equip4);
            Assert.AreEqual(a.getEquipmentCount(), 3);
        }


    }
}
