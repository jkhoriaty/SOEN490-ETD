using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models.Objects;
using System.Collections.Generic;

namespace ETD_UnitTest
{
    [TestClass]
    public class MapModTest
    {
        [TestMethod]
        public void MapModCreationTest()
        {
            List<MapMod> mpList = MapMod.getMapModList();
            MapMod mp = new MapMod("camp");
            Assert.AreEqual(mp.getMapModType(), MapMods.camp);
            MapMod ms = new MapMod("circle");
            Assert.AreEqual(ms.getMapModType(), MapMods.circle);
            Assert.AreEqual(mpList.Count, 2);          
        }

    }
}
