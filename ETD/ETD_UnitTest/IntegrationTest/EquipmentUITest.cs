using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETD.Models;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters;
using ETD.Models.Objects;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD_UnitTest.IntegrationTest
{
    [TestClass]
    public class EquipmentUITest
    {
        [TestMethod]
        public void CreateEquipmentPinTest()
        {
            MainWindow window = new MainWindow();
            MapSectionPage mapSectionPage = new MapSectionPage(window);
            Equipment equip = new Equipment("ambulanceCart");
            Team a = new Team("team");
            a.AddEquipment(equip);
            mapSectionPage.Update();
            bool check = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == equip)
                {
                    check = true;
                }

            }
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void DeleteEquipmentPinTest()
        {
            MainWindow window = new MainWindow();
            MapSectionPage mapSectionPage = new MapSectionPage(window);
            Equipment equip = new Equipment("ambulanceCart");
            Team a = new Team("team");
            a.AddEquipment(equip);
            mapSectionPage.Update();
            bool check = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == equip)
                {
                    check = true;
                }

            }
            Assert.IsTrue(check);
            a.RemoveEquipment(equip);
            Equipment.DeleteEquipment(equip);
            mapSectionPage.Update();
            bool exist = false;
            foreach (Pin pin in Pin.getPinList())
            {
                if (pin.getRelatedObject() == equip)
                {
                    check = true;
                }
            }
            Assert.IsFalse(exist);
        }
    }
}
