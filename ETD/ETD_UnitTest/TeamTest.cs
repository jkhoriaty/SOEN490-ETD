using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;

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
            a.addMember(MemberA);
            a.addMember(MemberB);
            Assert.AreEqual(a.getMember(2), null);
            Assert.AreEqual(a.getMember(0), MemberA);
            Assert.AreEqual(a.getMember(1), MemberB);
        }

        [TestMethod]
        public void EquipmentAddTest()
        {
            Team a = new Team("A");
            Equipment equip1 = new Equipment(Equipments.sittingCart);
            Equipment equip2 = new Equipment(Equipments.ambulanceCart);
            Equipment equip3 = new Equipment(Equipments.epipen);
            Equipment equip4 = new Equipment(Equipments.transportStretcher);
            //a.addEquipment(equip1);
            //a.addEquipment(equip2);
            //a.addEquipment(equip3);
            //a.addEquipment(equip4);
            Assert.AreEqual(a.getEquipmentCount(), 3);
        }


    }
}
