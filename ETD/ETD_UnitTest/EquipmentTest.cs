using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;

namespace ETD_UnitTest
{
    [TestClass]
    public class EquipmentTest
    {
        [TestMethod]
        public void EquipmentCreation()
        {
            Equipment test = new Equipment(equipments.sittingCart);
            Assert.AreEqual(test.getEquipmentName(), equipments.sittingCart);
        }
    }
}
