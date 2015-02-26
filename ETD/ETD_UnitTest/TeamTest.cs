using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.Models.Objects;
using ETD.Models.Services;
using ETD.Models.Grids;
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
            Team c = new Team("C");
            //String equip1 = "TestEquip1";
            //String equip2 = "TestEquip2";
            //String equip3 = "TestEquip3";
            //String equip4 = "TestEquip4";
            Equipment equip1 = new Equipment(Equipments.sittingCart);
            Equipment equip2 = new Equipment(Equipments.ambulanceCart);
            Equipment equip3 = new Equipment(Equipments.epipen);
            Equipment equip4 = new Equipment(Equipments.transportStretcher);
            c.addEquipment(equip1);
            c.addEquipment(equip2);
            c.addEquipment(equip3);
            Assert.IsFalse(c.addEquipment(equip4));
            Assert.AreEqual(c.getEquipmentCount(), 3);

            c.removeEquipment(equip1);
            Assert.AreEqual(c.getEquipmentCount(), 2);

        }

        [TestMethod]
        public void setStatusTest()
        {
            Team b = new Team("B");

            b.setStatus(Statuses.available);
            Assert.AreEqual(b.getStatus(), Statuses.available);
        }

       
    }
}
