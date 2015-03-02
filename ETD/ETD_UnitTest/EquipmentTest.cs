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
            List<Equipment> eqList = Equipment.getEquipmentList();
            eqList.Clear();
            Equipment sc = new Equipment("sittingCart");
            Assert.AreEqual(sc.getEquipmentType(), Equipments.sittingCart);
            Equipment ac = new Equipment("ambulanceCart");
            Assert.AreEqual(ac.getEquipmentType(), Equipments.ambulanceCart);
            Assert.AreEqual(eqList.Count, 2);
            Equipment.DeleteEquipment(ac);
            Assert.AreEqual(eqList.Count, 1);
            bool assigned = true;
            sc.setAssigned(assigned);
            Assert.AreEqual(sc.IsAssigned(), true);

            
        }
    }
}
