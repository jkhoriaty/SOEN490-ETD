using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.Models.Objects;
using System.Collections.Generic;

namespace ETD_UnitTest
{
    [TestClass]
    public class EquipmentTest
    {
        [TestMethod]
        public void EquipmentCreation()
        {
            Equipment sc = new Equipment("sittingCart");
            Assert.AreEqual(sc.getEquipmentType(), Equipments.sittingCart);
            Equipment ac = new Equipment("ambulanceCart");
            Assert.AreEqual(ac.getEquipmentType(), Equipments.ambulanceCart);

            List<Equipment> eqList = Equipment.getEquipmentList();
            Assert.AreEqual(eqList.Count, 2);
            Assert.AreEqual(eqList[0], "sittingCart");
        }
    }
}
